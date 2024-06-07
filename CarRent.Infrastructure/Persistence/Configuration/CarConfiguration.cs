namespace CarRent.Infrastructure.Persistence.Configuration;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable(nameof(Car));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasOne(x => x.Model)
            .WithMany()
            .HasForeignKey(x => x.ModelId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}
