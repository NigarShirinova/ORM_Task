namespace EF.Entities;

internal class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public BaseEntity()
    {
         IsDeleted = false;
    }
}

