using Sfs2X.Protocol.Serialization;
namespace lta.data
{
    public class UserData : SerializableSFSType
    {
        public string userId;
        public string userName;
        public int totalMoney;
        public int totalDiamond;
        public int vip;
        public string deviceId;
        public bool isOnline;
        public int reconnect;
    }
}
