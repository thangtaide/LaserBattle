using UnityEngine;
using LTA.UI;
using LTA.DesignPattern;
namespace LTA.Market
{
    public class ObserverCoinKey
    {
        public const string UpdateCoinInfo = "UpdateCoinInfo";
    }

    [RequireComponent(typeof(TextMoney))]
    [DisallowMultipleComponent]
    public class CoinBarController : MonoBehaviour
    {
        TextMoney txtMoney;

        private void Awake()
        {
            txtMoney = GetComponent<TextMoney>();
            Observer.Instance.AddObserver(ObserverCoinKey.UpdateCoinInfo, UpdateCoin);
        }

        protected float Coin
        {
            get
            {
                return MarketVal.coin;
            }
        }

        void UpdateCoin(object data)
        {
            Debug.Log(Coin);
            txtMoney.UpdateMoney(Coin);
        }

        private void OnDestroy()
        {
            Observer.Instance.RemoveObserver(ObserverCoinKey.UpdateCoinInfo, UpdateCoin);
        }
    }
}

