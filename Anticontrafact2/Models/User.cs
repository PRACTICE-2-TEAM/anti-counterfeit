using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;

namespace Anticontrafact2.Models
{
    public class User
    {
        private static User instance;
        private User()
        {
            IsLogin = false;
            Email = null;
        }
        private string email;
        private bool isLogin;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                if(value == null)
                {
                    isLogin = false;
                }
                else
                {
                    isLogin = true;
                }
            }
        }
        public bool IsLogin
        {
            get { return isLogin; }
            set
            {
                isLogin = value;
                if (value == false)
                    email = null;
            }
        }

        public string Password { get; set; }

        // токен
        public string Token { get; set; }

        public static User GetUser()
        {
            if (instance == null)
                instance = new User();
            return instance;
        }
    }
}
