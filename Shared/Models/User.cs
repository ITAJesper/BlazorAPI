using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorAPI.Shared.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string? Name { get; set; }
    
    [JsonIgnore]
    public List<Comment>? Comments {get; set;}
    
   [JsonIgnore]
    public List<Question>? Questions {get; set;} 
}