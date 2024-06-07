namespace CarRent.Infrastructure.Persistence.Configuration;

public class CarImageConfiguration : IEntityTypeConfiguration<CarImage>
{
    public void Configure(EntityTypeBuilder<CarImage> builder)
    {
        builder.ToTable(nameof(CarImage));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasOne(x => x.Car).WithMany(x => x.Images).HasForeignKey(x => x.CarId);

        builder.Property(x => x.Picture)
            .HasColumnType("image");
    }
}
