namespace CourtesyImplementation._3_Visitor
{
  public interface NodeVisitor
  {
    void VisitElephant(Elephant elephant);
    void VisitBox(Box elephant);
  }
}