using NUnit.Framework;

namespace XUnitTestPatterns._07_FixtureManagement
{
  public class _02_StandardFixture
  {
    private static Invoice CreateNewInvoice()
    {
      var invoice = new Invoice();
      var address = CreateAddress();
      invoice.SetInvoiceNumber("InvTest001");
      invoice.SetBillToAddress(address);
      AttachInvoiceLineAsCharge(
        invoice,
        new Money("4999.95", "USD"));
      invoice.SetStatus(InvoiceStatus.New);
      return invoice;
    }

    private static void AttachInvoiceLineAsCharge(Invoice invoice, Money charge)
    {
      var invToInvLine
        = new InvoiceToInvoiceLine();
      var invoiceLine =
        GetInvoiceLineAsCharge(charge);
      invToInvLine.SetInvoiceLine(invoiceLine);
      invToInvLine.SetInvoice(invoice);
    }

    private static InvoiceLine GetInvoiceLineAsCharge(Money money)
    {
      //todo implement
      return null;
    }

    private static InvoiceAddress CreateAddress(
      string city,
      string state,
      string zip)
    {
      var address = new InvoiceAddress();
      address.SetAddressLine1("1011 Bit Lane");
      address.SetCity(city);
      address.SetState(state);
      address.SetZip(zip);
      address.SetStatus(AddressStatus.Active);
      return address;
    }

    private static InvoiceAddress CreateAddress()
    {
      var address = CreateAddress(
        "Chicago",
        "IL",
        "60647");
      return address;
    }


    [Test]
    public void ShouldPassEachLineToTheReport()
    {
      //GIVEN
      var invoice = CreateNewInvoice();

      //WHEN
      //...

      //THEN
      //...
    }

    [Test]
    public void ShouldFormatInvoiceTitleAccordingToIso12354()
    {
      //GIVEN
      var invoice = CreateNewInvoice();

      //WHEN
      //...

      //THEN
      //...
    }
  }
}