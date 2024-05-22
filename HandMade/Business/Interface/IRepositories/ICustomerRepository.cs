using DataAccess.Entites;

namespace ClassLibrary1.Interface.IRepositories;

public interface ICustomerRepository:IBaseRepository<Customer>
{
    bool CheckEmailExist(string email);
    Customer? VerifyEmailCode(string email, int code);
}