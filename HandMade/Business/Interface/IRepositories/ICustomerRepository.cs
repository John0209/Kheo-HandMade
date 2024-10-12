using ClassLibrary1.Dtos.RequestDto.Account;
using DataAccess.Entites;
using DataAccess.Enum;

namespace ClassLibrary1.Interface.IRepositories;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    public Task<Customer?> GetCustomerByUserId(int id);
}