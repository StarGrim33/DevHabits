using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;

internal static class HabitMappings
{
    public static Habit ToEntity(this CreateHabitDto dto)
    {
        Habit habit = new()
        {
            Id = $"h_{Guid.CreateVersion7()}",
            Name = dto.Name,
            Description = dto.Description,
            Type = dto.Type,
            Frequency = new Frequency()
            {
                Type = dto.Frequency.Type,
                TimesPerPeriod = dto.Frequency.TimesPerPeriod
            },
            Target = new Target
            {
                Unit = dto.Target.Unit,
                Value = dto.Target.Value
            },
            EndDate = dto.EndDate,
            Milestone = dto.Milestone is not null
                ?  new Milestone
                {
                    Target = dto.Milestone.Target,
                    Current = 0
                } : null,
            CreatedAtUtc = DateTime.UtcNow
        };

        return habit;
    }

    public static HabitDto ToDto(this Habit h)
    {
        return new HabitDto
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

    public static void UpdateFromDto(this Habit habit, UpdateHabitDto dto)
    {
        habit.Name = dto.Name;
        habit.Description = dto.Description;
        habit.Type = dto.Type;
        habit.EndDate = dto.EndDate;

        habit.Frequency = new Frequency
        {
            Type = dto.Frequency.Type,
            TimesPerPeriod = dto.Frequency.TimesPerPeriod,
        };

        habit.Target = new Target
        {
            Unit = dto.Target.Unit,
            Value = dto.Target.Value
        };

        if (dto.Milestone is not null)
        {
            habit.Milestone = new Milestone
            {
                Target = dto.Milestone.Target
            };
        }

        habit.UpdatedAtUtc = DateTime.UtcNow;
    }
}
