using BlazorAPI.Server.Data;
using BlazorAPI.Shared.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace BlazorAPI.Server.Services;

public class CommentService : ICommentService
{
    private DataContext db { get; }

    public CommentService(DataContext db)
    {
        this.db = db;
    }
    
    public async Task<IEnumerable<Comment>> GetAllCommentasync()
    {
        List<Comment> allComment = await db.Comments
            .Include(c => c.Questions)
            .Include(c => c.User)
            .ToListAsync();
        
        return allComment;
    }
    

    public Task<IResult> GetByIdAsync(int commentId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<IResult> CreateCommentAsync(Comment commentToCreate)
    {
        User user = await db.Users
            .Where(u => u.UserId == commentToCreate.UserId)
            .FirstOrDefaultAsync();
        if (user == null)
            db.Users.Add(user);

        Question question = await db.Questions
            .Where(q => q.QuestionId == commentToCreate.QuestionId)
            .FirstOrDefaultAsync();

        commentToCreate.User = user;
        commentToCreate.Questions = question;
        
       await db.Comments.AddAsync(commentToCreate);
       await db.SaveChangesAsync();

        await db.SaveChangesAsync();
        return Results.Created($"/posts/{commentToCreate.CommentId}", commentToCreate);
    }

    
    public async Task<IResult> UpdateCommentAsync(int commentId, Comment updatedComment)
    {
        var commentToUpdate = await db.Comments.FindAsync(commentId);

        if (commentToUpdate == null)
        {
            return Results.NotFound();
        }

        commentToUpdate.Text = updatedComment.Text;
        commentToUpdate.User = updatedComment.User;
        

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    public async Task<IResult> UpdateCommentByIdUpvote(int commentId)
    {
        var upvoteComment = await db.Comments.FindAsync(commentId);
        
        if (upvoteComment == null)
        {
            return Results.NotFound();
        }

        upvoteComment.UpVote++;
        

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    public async Task<IResult> UpdatecommentByIdDownvote(int commentId)
    {
        var downvoteComment = await db.Comments.FindAsync(commentId);
        
        if (downvoteComment == null)
        {
            return Results.NotFound();
        }

        downvoteComment.DownVote++;
        

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    public async Task<IResult> DelteCommentAsync(int commentId)
    {
        Comment? commentToDelete = await db.Comments.FindAsync(commentId);

        if (commentToDelete != null)
        {
            db.Comments.Remove(commentToDelete);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
        else
        {
            return Results.NotFound();
        }
    }
}