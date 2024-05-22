using Application.ErrorHandlers;
using Application.Utils;
using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Interface;
using ClassLibrary1.Interface.IServices;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Service;

public class AccountService:IAccountService
{
    private IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterCustomer(RegisterRequestDto dto)
    {
        var emailExisted = _unitOfWork.CustomerRepository.CheckEmailExist(dto.Email);
        if (emailExisted)
            throw new ConflictException("Email has been exist in the database");

        var customer = new Customer
        {
            FullName = StringUtils.FormatName(dto.Name),
            BirthDate = dto.BirthDate,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            PassWord = dto.Password,
            Status = UserStatus.Inactive
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
        var code = StringUtils.GenerateRandomNumber(6);
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
        if(customer==null)
         throw new BadRequestException("Email or code is wrong");
        
        customer.Status = UserStatus.Actived;
        _unitOfWork.CustomerRepository.Update(customer);
        await _unitOfWork.SaveChangeAsync();
    }
}