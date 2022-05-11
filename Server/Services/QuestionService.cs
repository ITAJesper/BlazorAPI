using BlazorAPI.Server.Data;
using BlazorAPI.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;


namespace BlazorAPI.Server.Services;

public class QuestionService : IQuestionService
{
    private DataContext db { get; set; }

    public QuestionService(DataContext db)
    {
        this.db = db;
    }
    
    public async Task<IEnumerable<Question>> GetAllQuestionAsync()
    {
        List<Question> allQuestion = await db.Questions
            .Include(q => q.User)
            .Include(q => q.Topic)
            .ToListAsync();
        
        return allQuestion;
    }
    
    public async Task<IResult> GetByIdAsync(int questionId)
    {
        Question? question = await db.Questions.FindAsync(questionId);

        if (question != null)
        {
            return Results.Ok(question);
        }
        else
        {
            return Results.NotFound();
        }
    }

    public async Task<IResult> CreateQuestionAsync(Question questionToCreate)
    {
        User user = await db.Users
            .Where(u => u.UserId == questionToCreate.UserId)
            .FirstOrDefaultAsync();
        if (user == null)
            db.Users.Add(user);

        Topic topic = await db.Topics
            .Where(t => t.TopicId == questionToCreate.TopicId)
            .FirstOrDefaultAsync();
        if (topic == null)
            db.Topics.Add(topic);
        
        questionToCreate.User = user;
        questionToCreate.Topic = topic;
        
        await db.Questions.AddAsync(questionToCreate);
        await db.SaveChangesAsync();
        
        return Results.Created($"/posts/{questionToCreate.QuestionId}", questionToCreate);
    }

    public async Task<IResult> UpdateQuestionAsync(int questionId, Question updatedquestion)
    {
        var questionToUpdate = await db.Questions.FindAsync(questionId);

        if (questionToUpdate == null)
        {
            return Results.NotFound();
        }

        questionToUpdate.Title = updatedquestion.Title;
        questionToUpdate.Text = updatedquestion.Text;
        questionToUpdate.User = updatedquestion.User;
        questionToUpdate.Topic = updatedquestion.Topic;
        

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    public async Task<IResult> UpdateQuestionByIdUpvote(int questionId)
    {
        var upvoteQuestion = await db.Questions.FindAsync(questionId);
        
        if (upvoteQuestion == null)
        {
            return Results.NotFound();
        }

        upvoteQuestion.UpVote++;
        

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    public async Task<IResult> UpdateQuestionByIdDownvote(int questionId)
    {
        var downvoteQuestion = await db.Questions.FindAsync(questionId);
        
        if (downvoteQuestion == null)
        {
            return Results.NotFound();
        }

        downvoteQuestion.DownVote++;
        

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    public async Task<IResult> DelteQuestionAsync(int questionId)
    {
        Question? questionToDelete = await db.Questions.FindAsync(questionId);

        if (questionToDelete != null)
        {
            db.Questions.Remove(questionToDelete);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
        else
        {
            return Results.NotFound();
        }
    }
}