namespace CarRent.Domain.Entities.Base;

public class Aggregate : Entity
{
    public DateTimeOffset CreateDate { get; protected set; }
    public DateTimeOffset? LastUpdateDate { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTimeOffset? DeleteDate { get; protected set; }

    protected Aggregate() { }

    protected Aggregate(Guid id, DateTimeOffset createDate)
    {
        if (id == default)
        {
            throw new Exception($"ობიექტის იდენტიფიკატორი არავალიდურია {GetType().Name}");
        }
        Id = id;

        CreateDate = createDate;
    }

    protected void Update(DateTimeOffset updateDate)
    {
        if (IsDeleted)
        {
            throw new Exception($"წაშლილი ობიექტის განახლება შეუძლებელია {GetType().Name}");
        }

        LastUpdateDate = updateDate;
    }

    public void Delete(DateTimeOffset deleteDate)
    {
        if (IsDeleted)
        {
            throw new Exception($"წაშლილი ობიექტის ხელმეორედ წაშლა შეუძლებელია {GetType().Name}");
        }

        DeleteDate = deleteDate;
        IsDeleted = true;
    }
}
