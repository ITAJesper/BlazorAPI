using BlazorAPI.Shared.Models;

namespace BlazorAPI.Server.Services;

public interface ICommentService
{
    public Task<IResult> CreateCommentAsync(Comment commentToCreate);

    public Task<IResult> GetByIdAsync(int commentId);

    public Task<IEnumerable<Comment>> GetAllCommentasync();
    
    public Task<IResult> UpdateCommentAsync(int commentId, Comment updatedComment);
    
    public Task<IResult> UpdateCommentByIdUpvote(int commentId);

    public Task<IResult> UpdatecommentByIdDownvote(int commentId);

    public Task<IResult> DelteCommentAsync(int commentId);
}