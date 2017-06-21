namespace SubscriptionApi.Dto
{
  public class StartSubscriptionResponseDto
  {
    public bool Failure { get; set; }
    public string[] Errors { get; set; }
  }
}