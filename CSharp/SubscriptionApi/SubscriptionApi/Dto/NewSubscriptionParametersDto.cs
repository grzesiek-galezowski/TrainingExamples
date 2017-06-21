namespace SubscriptionApi.Dto
{
  public class NewSubscriptionParametersDto
  {
    public string UserName { get; set; }
    public string SubscriptionId { get; set; }
    public AssetRequestDto[] Requests { get; set; }
  }
}