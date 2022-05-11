using BlazorAPI.Server.Data;
using BlazorAPI.Server.Services;
using BlazorAPI.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TopicService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<CommentService>();


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


var allowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSomeStuff, builder => {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(allowSomeStuff);

app.MapRazorPages();
app.MapFallbackToFile("index.html");

// endpoints for topic

app.MapGet("/topic/all", async (TopicService service) =>
{
    return await service.GetAllTopicsAsync();
});

app.MapPost("/topic/create", async ([FromBody] Topic topicToCreate,[FromServices] TopicService service) =>
{
    return await service.CreateTopicAsync(topicToCreate);
});


app.MapDelete("/topic/delete/{topicId}", async (int topicId, TopicService service) =>
    {
        return await service.DelteTopicAsync(topicId);
    }
);

// endpoints for user

app.MapGet("/user/all", async (UserService service) =>
{
    return await service.GetAllUserAsync();
});

app.MapPost("/user/create", async ([FromBody] User userToCreate,[FromServices] UserService service) =>
{
    return await service.CreateUserAsync(userToCreate);
});

app.MapDelete("/user/delete/{userId}", async (int userId, UserService service) =>
    {
        return await service.DelteUserAsync(userId);
    }
);

// endpoints for Question

app.MapGet("/question/all", async (QuestionService service) =>
{
    return await service.GetAllQuestionAsync();
});

app.MapGet("/question/{questionId}", async (int questionId, QuestionService service) =>
{
    return await service.GetByIdAsync(questionId);
});

app.MapPut("/question/update/{questionId}", async (int questionId, Question upvotedQuestion, QuestionService serviec) =>
    {
        return await serviec.UpdateQuestionByIdUpvote(questionId);
    }
);

app.MapPost("/question/create", async ([FromBody]Question questionToCreate, QuestionService service) =>
{
    return await service.CreateQuestionAsync(questionToCreate);
});
    
app.MapDelete("/question/delete/{questionId}", async (int questionId, QuestionService service) => 
    {
        return await service.DelteQuestionAsync(questionId);
    }
);

app.MapPut("/question/upvote/{questionId}", async (int questionId, QuestionService service) =>
{
    return await service.UpdateQuestionByIdUpvote(questionId);
});

app.MapPut("/question/downvote/{questionId}", async (int questionId, QuestionService service) =>
{
    return await service.UpdateQuestionByIdDownvote(questionId);
});

// endpoints for Comment

app.MapGet("/comment/all", async (CommentService service) =>
{
    return await service.GetAllCommentasync();
});

app.MapGet("/comment/{commentId}", async (int commentId, CommentService service) =>
{
    return await service.GetByIdAsync(commentId);
});

app.MapPut("/comment/update/{commentId}", async (int commentId, Comment updatedComment, CommentService serviec) =>
    {
        return await serviec.UpdateCommentAsync(commentId, updatedComment);
    }
);

app.MapPost("/comment/create", async ([FromBody] Comment commentToCreate, CommentService service) =>
{
    return await service.CreateCommentAsync(commentToCreate);
});
    
app.MapDelete("/comment/delete/{commentId}", async (int commentId, CommentService service) =>
    {
        return await service.DelteCommentAsync(commentId);
    }
);

app.MapPut("/comment/upvote/{commentId}", async (int commentId, CommentService service) =>
{
    return await service.UpdateCommentByIdUpvote(commentId);
});

app.MapPut("/comment/downvote/{commentId}", async (int commentId, CommentService service) =>
{
    return await service.UpdatecommentByIdDownvote(commentId);
});


app.Run();
