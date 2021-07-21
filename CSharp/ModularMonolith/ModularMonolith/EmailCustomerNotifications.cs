using System.Net.Mail;
using System.Threading.Tasks;
using WarehouseModule;

namespace ModularMonolith
{
  public class EmailCustomerNotifications : ICustomerNotifications
  {
    public void NotifyCustomerOfOrderState(OrderDto orderDto)
    {
      var smtpClient = new SmtpClient("127.0.0.1");

      smtpClient.SendAsync(new MailMessage(
        new MailAddress("shop.com"),
        new MailAddress(orderDto.RecipientEmail))
      {
        Body = "Your order is " + orderDto.OrderState
      }, new object());
    }
  }
}