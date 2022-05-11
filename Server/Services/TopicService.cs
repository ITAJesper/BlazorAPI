using BlazorAPI.Server.Data;
using BlazorAPI.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAPI.Server.Services;

public class TopicService : ITopicService
{
    private DataContext db { get; }

    public TopicService(DataContext db)
    {
        this.db = db;
    }
    
    public async Task<IResult> CreateTopicAsync(Topic topicToCreate)
    {
        db.Topics.Add(topicToCreate);
        await db.SaveChangesAsync();
        return Results.Created($"/topic/{topicToCreate.TopicId}", topicToCreate);
    }

    public Task<IResult> GetByIdAsync(int topicId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
    {
        List<Topic> allTopics = await db.Topics
            .ToListAsync();
        
        return allTopics;
    }

    public async  Task<IResult> DelteTopicAsync(int topicId)
    {
        Topic? topicToDelete = await db.Topics.FindAsync(topicId);

        if (topicToDelete != null)
        {
            db.Topics.Remove(topicToDelete);
            await db.SaveChangesAsync();
            return Results.NoContent();
            
        }
        else
        {
            return Results.NotFound();
        }
    }
}