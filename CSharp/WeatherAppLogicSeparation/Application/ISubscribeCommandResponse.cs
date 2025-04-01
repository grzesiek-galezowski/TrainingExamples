namespace Application;

public interface ISubscribeCommandResponse
{
  Task NoDevicesForQuery(Guid subscriptionId);
  Task SubscriptionAlreadyExists(
    Guid subscriptionId, SubscriptionAlreadyExistsException exception);
  Task SubscriptionCreated(Guid subscriptionId);
  Task ResolutionSubjectNotFound(Guid subscriptionId);
  Task ErrorWhileResolvingDevices(BadResolutionException exception);
  Task UnexpectedError(Exception exception);
}