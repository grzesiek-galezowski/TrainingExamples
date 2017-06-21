namespace CourtesyImplementation._4_Visitor
{
  public interface NodeVisitor
  {
    void VisitElephant(Elephant elephant);
    void VisitBox(Box elephant);
  }
}