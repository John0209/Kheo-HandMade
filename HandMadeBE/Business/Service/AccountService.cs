using Application.ErrorHandlers;
using Application.Utils;
using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Dtos.ResponseDto;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IServices;
using ClassLibrary1.Mapper;
using ClassLibrary1.Utils;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Service;

public class AccountService : IAccountService
{
    private IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto, LoginType type)
    {
        var customer = await _unitOfWork.CustomerRepository.LoginAsync(dto, type) ??
                       throw new BadRequestException(
                           "Login failed, please check your login information again or activate your account via the activation code sent to your email");

        return CustomerMapper.CustomerToLoginResponseDto(customer);
    }

    public async Task RegisterCustomer(RegisterRequestDto dto)
    {
        var (user, isEmailExist) = _unitOfWork.CustomerRepository.CheckEmailExist(dto.Email);
        if (isEmailExist)
            throw new ConflictException("Email has been exist in the database");
        var code = StringUtils.GenerateRandomNumber(6);

        var customer = new Customer
        {
            FullName = StringUtils.FormatName(dto.Name),
            BirthDate = dto.BirthDate,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            PassWord = dto.Password,
            Status = UserStatus.Inactive,
            TokenCode = int.Parse(code),
            Address = "Not"
        };

        await _unitOfWork.CustomerRepository.AddAsync(customer);
        var result = await _unitOfWork.SaveChangeAsync();

        if (result > 0)
            EmailUtils.SendVerifyCodeToEmail(customer);
        else
            throw new BadRequestException("Register Failed");
    }

    public async Task VerifyEmailCode(VerifyRequestDto dto)
    {
        var customer = _unitOfWork.CustomerRepository.VerifyEmailCode(dto.Email, dto.Code);
        if (customer == null)
            throw new BadRequestException("Email or code is wrong");

        customer.Status = UserStatus.Actived;
        _unitOfWork.CustomerRepository.Update(customer);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task ChangPassword(PassChangeRequestDto request)
    {
        var user = await _unitOfWork.CustomerRepository.GetByIdAsync(request.UserId) ??
                   throw new NotFoundException("Not found user information");

        user.PassWord = request.Password;

        _unitOfWork.CustomerRepository.Update(user);
        if (await _unitOfWork.SaveChangeAsync() < 0) throw new NotFoundException("Update user password to DB failed");
    }

    public async Task RecoverPassword(string email)
    {
        var (user, isEmailExist) = _unitOfWork.CustomerRepository.CheckEmailExist(email);
        if (!isEmailExist) throw new BadRequestException("Email has not existed in DB");

        if (user == null) throw new ConflictException("User information is null");

        user.PassWord = StringUtils.GenerateRandomNumberString(6);

        _unitOfWork.CustomerRepository.Update(user);
        if (await _unitOfWork.SaveChangeAsync() > 0)
        {
            EmailUtils.SendNewPasswordToEmail(user);
            return;
        }

        throw new NotFoundException("Update user password is failed");
    }
}