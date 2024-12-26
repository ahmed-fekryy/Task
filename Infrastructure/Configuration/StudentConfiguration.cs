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
    
    public  class StudentConfiguration  : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(50); // Increased length for more realistic email addresses

            builder.HasIndex(s => s.Email)
                .IsUnique();

            builder.Property(s => s.Age)
                .IsRequired(); // Age is mandatory

            builder.Property(s => s.Address)
                .HasMaxLength(100); // Assuming a reasonable length for Address
        }
    }
}
