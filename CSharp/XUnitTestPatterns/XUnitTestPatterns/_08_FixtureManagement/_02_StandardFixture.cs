using NUnit.Framework;

namespace XUnitTestPatterns._08_FixtureManagement
{
  public class _02_StandardFixture
  {
    [Test]
    public void ShouldPassEachLineToTheReport()
    {
      //GIVEN
      var invoice = InvoiceMother.CreateRepresentativeInvoice();

      //WHEN
      //...

      //THEN
      //...
    }

    [Test]
    public void ShouldFormatInvoiceTitleAccordingToIso12354()
    {
      //GIVEN
      var invoice = InvoiceMother.CreateRepresentativeInvoice();

      //WHEN
      //...

      //THEN
      //...
    }
  }
}