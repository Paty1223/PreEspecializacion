using FastDeliveryApi.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastDeliveryApi.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
        .HasMaxLength(100)
        .HasColumnType("text")
        .IsRequired();

        builder.Property(b => b.PhoneNumber)
        .HasMaxLength(9)
        .HasColumnType("text")
        .HasColumnName("PhoneNumberCustomer");

        builder.Property(b => b.Email)
        .HasMaxLength(120)
        .HasColumnType("text")
        .IsRequired();

        builder.Property(b => b.Address)
        .HasColumnType("text")
        .IsRequired()
        .HasMaxLength(120);

        builder.HasData(
            new Customer
            {
                Id = 1,
                Name = "Josselin Zelaya",
                Email = "Josselin@gmail.com",
                Address = "San Miguel",
                PhoneNumber = "1234-5678",
                Status = true
            },
            new Customer
            {
                Id = 2,
                Name = "Alexander Zelaya",
                Email = "Alexander@gmail.com",
                Address = "San Miguel",
                PhoneNumber = "8765-4321",
                Status = true
            }
        );
    }
}