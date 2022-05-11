using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorAPI.Shared.Models;

public class Comment
{
    [Key]
    public int CommentId { get; set; }
    
    public string? Text { get; set; }
    
    public int UpVote { get; set; }
    
    public int DownVote { get; set; }
    
    public DateTime TimeStamp { get; set; }
    
    public int QuestionId { get; set;}
    public Question? Questions { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set;}
}
