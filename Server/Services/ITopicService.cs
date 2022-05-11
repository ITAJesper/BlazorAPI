using BlazorAPI.Shared.Models;

namespace BlazorAPI.Server.Services;

public interface ITopicService
{
    public Task<IResult> CreateTopicAsync(Topic topicToCreate);

    public Task<IResult> GetByIdAsync(int topicId);

    public Task<IEnumerable<Topic>> GetAllTopicsAsync();
    
    public Task<IResult> DelteTopicAsync(int topicId);
}

