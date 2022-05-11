using BlazorAPI.Shared.Models;

namespace BlazorAPI.Server.Services;

public interface IQuestionService
{
    public Task<IResult> CreateQuestionAsync(Question question);

    public Task<IResult> GetByIdAsync(int questionId);

    public Task<IEnumerable<Question>> GetAllQuestionAsync();
    
    public Task<IResult> UpdateQuestionAsync(int questionId, Question updatedquestion);

    public Task<IResult> UpdateQuestionByIdUpvote(int questionId);

    public Task<IResult> UpdateQuestionByIdDownvote(int questionId);

    public Task<IResult> DelteQuestionAsync(int questionId);
}