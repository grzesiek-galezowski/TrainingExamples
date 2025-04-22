using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Add;

public class AddPostRequest(PostDto postDto) : IAddPostRequest
{
  public void AssertContentIsOfRequiredLength()
  {
    const int minLength = 10; // Example minimum length
    const int maxLength = 500; // Example maximum length
    if (string.IsNullOrWhiteSpace(postDto.Content) ||
        postDto.Content.Length < minLength ||
        postDto.Content.Length > maxLength)
    {
      throw new ArgumentException($"Content must be between {minLength} and {maxLength} characters.");
    }
  }
  public void AssertContentContainsNoInappropriateWords()
  {
    var inappropriateWords = new[] { "badword1", "badword2" }; // Example inappropriate words
    if (inappropriateWords.Any(word => postDto.Content.Contains(word, StringComparison.OrdinalIgnoreCase)))
    {
      throw new ArgumentException("Content contains inappropriate words.");
    }
  }
    
  public async Task AddToAsync(IExistingPosts existingPosts, IAddingInProgress addingInProgress)
  {
    var id = await existingPosts.AddAsync(postDto);
    addingInProgress.SavedSuccessfully(postDto, id);
  }
    
  public async Task NotifyAsync(IFollowers followers, IAddingInProgress addingInProgress)
  {

  }
}