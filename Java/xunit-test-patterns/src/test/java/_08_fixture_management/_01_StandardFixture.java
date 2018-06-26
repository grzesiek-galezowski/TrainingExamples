package _08_fixture_management;

import lombok.val;
import org.testng.annotations.Test;

public class _01_StandardFixture {

    public static Invoice createNewInvoice() {
        val invoice = new Invoice();
        val address = createAddress();
        invoice.setInvoiceNumber("InvTest001");
        invoice.setBillToAddress(address);
        attachInvoiceLineAsCharge(
            invoice,
            new Money("4999.95", "USD"));
        invoice.setStatus(InvoiceStatus.NEW);
        return invoice;
    }

    public static void attachInvoiceLineAsCharge(
        Invoice invoice,
        Money money) {

        val invToInvLine
            = new InvoiceToInvoiceLine();
        val invoiceLine =
            getInvoiceLineAsCharge(money);
        invToInvLine.setInvoiceLine(invoiceLine);
        invToInvLine.setInvoice(invoice);
    }

    private static InvoiceLine getInvoiceLineAsCharge(Money money) {
        //todo implement
        return null;
    }

    public static InvoiceAddress createAddress(
        String city,
        String state,
        String zip) {
        val address = new InvoiceAddress();
        address.setAddressLine1("1011 Bit Lane");
        address.setCity(city);
        address.setState(state);
        address.setZip(zip);
        address.setStatus(AddressStatus.ACTIVE);
        return address;
    }

    public static InvoiceAddress createAddress() {
        InvoiceAddress address = createAddress("Chicago",
            "IL",
            "60647");
        return address;
    }


    @Test
    public void shouldPassEachLineToTheReport() {
        //GIVEN
        val invoice = createNewInvoice();

        //WHEN
        //...

        //THEN
        //...
    }

    @Test
    public void shouldProcessingInvoice() {
        //GIVEN
        val invoice = createNewInvoice();

        //WHEN
        //...

        //THEN
        //...
    }
}
