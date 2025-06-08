namespace DevHabit.Api.DTOs.HabitTags;

public sealed record UpsertHabitTagsDto
{
    public required List<string> TagsId { get; init; } = [];
}
