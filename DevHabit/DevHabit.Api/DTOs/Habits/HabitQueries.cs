using System.Linq.Expressions;
using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;

internal static class HabitQueries
{
    public static Expression<Func<Habit, HabitDto>> ProjectToDto()
    {
        return h => new HabitDto
        {
            Id = h.Id,
            Name = h.Name,
            Description = h.Description,
            Type = h.Type,
            Frequency = new FrequencyDto
            {
                Type = h.Frequency.Type,
                TimesPerPeriod = h.Frequency.TimesPerPeriod
            },
            Target = new TargetDto
            {
                Unit = h.Target.Unit,
                Value = h.Target.Value
            },
            Status = h.Status,
            IsArchived = h.IsArchived,
            EndDate = h.EndDate,
            Milestone = h.Milestone == null
                            ? null
                            : new MilestoneDto
                            {
                                Target = h.Milestone.Target,
                                Current = h.Milestone.Current
                            },
            CreatedAtUtc = h.CreatedAtUtc,
            UpdatedAtUtc = h.UpdatedAtUtc,
            LastCompletedAtUtc = h.LastCompletedAtUtc
        };
    }
}
