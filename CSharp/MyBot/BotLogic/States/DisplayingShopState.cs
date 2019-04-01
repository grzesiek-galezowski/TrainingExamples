using System.Threading.Tasks;

namespace BotLogic.StateValues
{

    public class DisplayingShopState : DefaultState
    {
      private readonly Shop _shop;

      public DisplayingShopState(Shop shop)
      {
        _shop = shop;
      }

      public override async Task OnEnterAsync(IConversationPartner conversationPartner)
      {
        conversationPartner.AppendToResponse(_shop.GetCurrentGamesForSaleAsync());
      }
    }

    public class Shop
    {
      public string GetCurrentGamesForSaleAsync()
      {
        return "Chrono cross, Chrono Trigger";

      }
    }
}