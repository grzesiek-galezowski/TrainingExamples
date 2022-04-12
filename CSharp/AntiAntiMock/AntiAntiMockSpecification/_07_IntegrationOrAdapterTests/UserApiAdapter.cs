namespace MockNoMockSpecification._07_IntegrationOrAdapterTests;

internal class UserApiAdapter
{
  public UserApiAdapter(IUserApiSupport support, string uri)
  {
    UserApi = new HttpBasedUserApi(support, uri);
  }

  public IHttpBasedUserApi UserApi { get; }
}