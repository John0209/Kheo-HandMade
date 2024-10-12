namespace DataAccess.Entites;

public class Admin : BaseEntity
{
    //Relation
    public int UserId { get; set; }
    public virtual User? User { get; set; }
}