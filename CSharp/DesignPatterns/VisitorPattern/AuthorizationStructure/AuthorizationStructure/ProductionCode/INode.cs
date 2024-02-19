namespace AuthorizationStructure.ProductionCode;

public interface INode
{
    void Dump(IDumpTarget target);
    void AddChild(INode node); //BUG: candidate for visitor
}