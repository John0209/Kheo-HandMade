using ClassLibrary1.Dtos.RequestDto.Account;
using ClassLibrary1.Interface.IRepositories;
using DataAccess.Data;
using DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.Repositories;

public class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    public SellerRepository(AppDbContext context) : base(context)
    {
    }
}