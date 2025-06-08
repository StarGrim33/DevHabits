using DevHabit.Api.Database;
using DevHabit.Api.DTOs.HabitTags;
using DevHabit.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Controllers;

[ApiController]
[Route("habits/{habitId}/tags")]
public sealed class HabitTagsController(ApplicationDbContext applicationDbContext) : ControllerBase
{

    [HttpPut]
    public async Task<ActionResult> UpsertHabitTags(string habitId, UpsertHabitTagsDto habitTagsDto)
    {
        Habit? habit = await applicationDbContext
            .Habits
            .Include(h => h.Tags)
            .FirstOrDefaultAsync(h => h.Id == habitId);

        if (habit is null)
        {
            return NotFound();
        }

        var currentTagIds = habit.Tags.Select(t => t.Id).ToHashSet();

        if (currentTagIds.SetEquals(habitTagsDto.TagsId))
        {
            return NoContent();
        }

        List<Tag> existingTagIds = await applicationDbContext
            .Tags
            .Where(t => habitTagsDto.TagsId.Contains(t.Id))
            .ToListAsync();

        if (existingTagIds.Count != habitTagsDto.TagsId.Count)
        {
            return BadRequest("One or more tag IDs is invalid");
        }

        habit.HabitTags.RemoveAll(ht => !habitTagsDto.TagsId.Contains(ht.HabitId));

        string[] tagIdsToAdd = habitTagsDto.TagsId.Except(currentTagIds).ToArray();

        habit.HabitTags.AddRange(tagIdsToAdd.Select(tagId => new HabitTag
        {
            HabitId = habitId,
            TagId = tagId,
            CreatedAtUtc = DateTime.UtcNow
        }));

        await applicationDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{tagId}")]
    public async Task<ActionResult> DeleteHabitTag(string habitId, string tagId)
    {
        HabitTag? habitTag = await applicationDbContext.HabitTags
            .SingleOrDefaultAsync(ht => ht.HabitId == habitId && ht.TagId == tagId);

        if (habitTag is null)
        {
            return NotFound();
        }

        applicationDbContext.HabitTags.Remove(habitTag);

        await applicationDbContext.SaveChangesAsync();

        return NoContent();
    }
}
