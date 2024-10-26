using Microsoft.EntityFrameworkCore;
using TechStackStudies.Models;

namespace TechStackStudies.Infrastructure.Configuration;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Technology> builder)
    {
        builder.ToTable("technologies");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .HasDatabaseName("idx_technologies_id");

        builder.Property(x => x.Id)
            .HasColumnType("serial")
            .HasColumnName("technology_id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnType("varchar")
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IsFrameworkOrLib)
            .HasColumnType("boolean")
            .HasColumnName("is_framework_or_lib")
            .IsRequired();

        builder.Property(x => x.Category)
            .HasColumnType("varchar")
            .HasColumnName("category")
            .HasMaxLength(8)
            .IsRequired();

        builder.Property(x => x.SkillLevel)
            .HasColumnType("varchar")
            .HasColumnName("skill_level")
            .HasMaxLength(8)
            .IsRequired();
    }
}