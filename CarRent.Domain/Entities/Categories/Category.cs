namespace CarRent.Domain.Entities.Categories;

public class Category : Aggregate
{
    public string Value { get; private set; } = null!;
    public Guid? ParentId { get; private set; }
    public CategoryType Type { get; private set; }

    public Category? Parent { get; private set; }

    private List<Category> _children = new();
    public IReadOnlyCollection<Category> Children => _children;

    public Category() {}

    public Category(Guid id, string value, Guid? parentId, CategoryType type) : base(id, DateTimeOffset.Now)
    {
        Value = value;
        ParentId = parentId;
        Type = type;

        Validate(this);
    }

    private void Validate(Category category) 
    {
        if (string.IsNullOrEmpty(category.Value)) throw new Exception("კატეგორიის სახელი სავალდებულოა");
    }
}
