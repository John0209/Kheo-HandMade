using DataAccess.Enum;

namespace DataAccess.Entites;

public class Role : BaseEntity
{
    public RoleType RoleType { get; set; }

    //Relation
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}