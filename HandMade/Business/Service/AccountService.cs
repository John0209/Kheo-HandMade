using Application.ErrorHandlers;
using Application.Utils;
using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Dtos.ResponseDto;
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

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto, LoginType type)
    {
        var customer = await _unitOfWork.CustomerRepository.LoginAsync(dto, type) ??
                       throw new BadRequestException(
                           "Login failed, please check your login information again or activate your account via the activation code sent to your email");

        return CustomerMapper.CustomerToLoginResponseDto(customer);
    }

    public async Task RegisterCustomer(RegisterRequestDto dto)
    {
        var emailExisted = _unitOfWork.CustomerRepository.CheckEmailExist(dto.Email);
        if (emailExisted)
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
            SendConfirmationCode(customer);
        else
            throw new BadRequestException("Register Failed");
    }

    private void SendConfirmationCode(Customer account)
    {
        var code = account.TokenCode;
        var title = "Successful account registration";
        var content = "Welcome " + account.FullName + "<br>" + "<br>" +
                      "Your account has been successfully registered at HandMade" + "<br>" + "<br>" +
                      "To complete your registration, Please enter your activation code:" + "<br>"
                      + code + "<br>" + "<br>" +
                      "Thanks you!" + "<br>" + "<br>" + "HandMade Team";
        EmailUtils.SendEmail(account.Email!, title, content);
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
}