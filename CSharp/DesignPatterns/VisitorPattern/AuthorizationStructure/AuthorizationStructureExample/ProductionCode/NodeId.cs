namespace AuthorizationStructureExample.ProductionCode;

public record NodeId(string Name, NodeType Type)
{
  public static NodeId Device(string device) => new(device, NodeType.Device);
  public static NodeId User(string user) => new(user, NodeType.User);
  public static NodeId Group(string name) => new(name, NodeType.Group);
}