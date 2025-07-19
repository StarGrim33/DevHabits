using DevHabit.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevHabit.Api.Database.Configurations;

public sealed class HabitTagConfiguration : IEntityTypeConfiguration<HabitTag>
{
    public void Configure(EntityTypeBuilder<HabitTag> builder)
    {
        builder.HasKey(ht => new { ht.HabitId, ht.TagId });

        builder.Property(h => h.HabitId).HasMaxLength(500);
        builder.Property(h => h.TagId).HasMaxLength(500);

        builder.HasOne(ht => ht.Habit)
            .WithMany(ht => ht.HabitTags)
            .HasForeignKey(ht => ht.HabitId);
        
        builder.HasOne(ht => ht.Tag)
            .WithMany()
            .HasForeignKey(ht => ht.TagId);
    }
}
