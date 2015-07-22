namespace SubscriptionApi.Dto
{
  public class StopSubscriptionResponseDto
  {
    public bool Failure { get; set; }
    public string[] Errors { get; set; }
  }
}