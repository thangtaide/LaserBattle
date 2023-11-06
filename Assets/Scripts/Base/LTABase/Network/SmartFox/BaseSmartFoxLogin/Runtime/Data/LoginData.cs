
using Sfs2X.Protocol.Serialization;
namespace lta.data
{ 
    public class LoginData : SerializableSFSType
    {
        public string username;
        public string password;
        public string email;
        public int type;
        public string otp;
        public string deviceId;
        public bool reconnect;

        public LoginData()
        {
        }

        public LoginData(string username, string password,int type,  bool reconnect)
        {
            this.username = username;
            this.password = password;
            this.type = type;
            this.reconnect = reconnect;
        }
        

        public LoginData(string username, string password, string email, int type, string otp, string deviceId, bool reconnect)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.type = type;
            this.otp = otp;
            this.deviceId = deviceId;
            this.reconnect = reconnect;
        }
    }
}
