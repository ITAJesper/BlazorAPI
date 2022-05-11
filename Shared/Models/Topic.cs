using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorAPI.Shared.Models;

public class Topic
{
    [Key]
    public int TopicId { get; set; }

    public string? Title { get; set; }
    
    [JsonIgnore]
    public List<Question>? Questions { get; set; }
}