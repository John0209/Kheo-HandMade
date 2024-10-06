using Application.ErrorHandlers;
using Application.Utils;
using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Dtos.ResponseDto;
using ClassLibrary1.Dtos.ResponseDto.Customer;
using ClassLibrary1.Dtos.ResponseDto.Seller;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IServices;
using ClassLibrary1.Mapper;
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

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var user = await _unitOfWork.UserRepository.LoginAsync(dto) ??
                   throw new BadRequestException("Login failed, Email or password is wrong");

        var role = user.Role!.RoleType;

        switch (role)
        {
            case RoleType.Customer:
                if (user.Customer?.Status == UserStatus.Inactive)
                {
                    throw new BadRequestException("Account has not been activated");
                }

                break;
        }

        return UserMapper.UserToLoginResponse(user);
    }

    public async Task RegisterCustomer(RegisterRequestDto dto)
    {
        var (info, isEmailExist) = _unitOfWork.UserRepository.CheckEmailExist(dto.Email);
        if (isEmailExist)
            throw new ConflictException("Email has been exist in the database");
        var code = StringUtils.GenerateRandomNumber(6);

        var user = new User()
        {
            FullName = StringUtils.FormatName(dto.Name),
            Email = dto.Email,
            PassWord = dto.Password,
            RoleId = (int)RoleType.Customer
        };
        var customer = new Customer
        {
            BirthDate = dto.BirthDate,
            PhoneNumber = dto.PhoneNumber,
            Status = UserStatus.Inactive,
            TokenCode = int.Parse(code),
            User = user
        };

        await _unitOfWork.CustomerRepository.AddAsync(customer);
        var result = await _unitOfWork.SaveChangeAsync();

        if (result > 0)
            EmailUtils.SendVerifyCodeToEmail(user);
        else
            throw new BadRequestException("Register Failed");
    }

    public async Task VerifyEmailCode(VerifyRequestDto dto)
    {
        var user = _unitOfWork.UserRepository.VerifyEmailCode(dto.Email, dto.Code);
        if (user == null)
            throw new BadRequestException("Email or verify code is wrong");

        user.Customer!.Status = UserStatus.Actived;
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task RecoverPassword(string email)
    {
        var (user, isEmailExist) = _unitOfWork.UserRepository.CheckEmailExist(email);
        if (!isEmailExist) throw new BadRequestException("Email has not existed in DB");

        if (user == null) throw new ConflictException("User information is null");

        user.PassWord = StringUtils.GenerateRandomNumberString(6);

        _unitOfWork.UserRepository.Update(user);
        if (await _unitOfWork.SaveChangeAsync() > 0)
        {
            EmailUtils.SendNewPasswordToEmail(user);
            return;
        }

        throw new NotFoundException("Update user password is failed");
    }

    public async Task ChangePassword(PassChangeRequest request)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId) ??
                   throw new NotFoundException("Not found user information");

        user.PassWord = request.Password;

        _unitOfWork.UserRepository.Update(user);
        if (await _unitOfWork.SaveChangeAsync() < 0) throw new NotFoundException("Update user password to DB failed");
    }
}