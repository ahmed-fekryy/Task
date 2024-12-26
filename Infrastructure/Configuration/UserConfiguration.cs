using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Define the table name
            builder.ToTable("Users");

            // Define the primary key
            builder.HasKey(u => u.Id);

            // Configure the Username property
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            // Ensure the Username is unique
            builder.HasIndex(u => u.Username)
                .IsUnique();
        }
    }

}
