using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorAPI.Shared.Models;

public class Question
{
    [Key]
    public int QuestionId { get; set; }

    public string? Title { get; set; }
    
    public string? Text { get; set; }
    
    public int UpVote { get; set; }
    
    public int DownVote { get; set; }
    
    public DateTime TimeStamp { get; set; }
    
    
    public int TopicId { get; set;}
    public Topic? Topic { get; set; }
    
    public int UserId {get; set;} 
    public User? User { get; set;}
    
    [JsonIgnore]
    public List<Comment>? Comments { get; set; }
    
}