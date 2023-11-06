
using LTA.DesignPattern;
namespace LTA.Market
{
    public class MarketUtils
    {
        public static void UpdateCoin(float coin)
        {
            MarketVal.coin += coin;
            Observer.Instance.Notify(ObserverCoinKey.UpdateCoinInfo, coin);
        }
    }
}
