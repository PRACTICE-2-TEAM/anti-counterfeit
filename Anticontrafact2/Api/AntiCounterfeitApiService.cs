using Refit;
using System.Net;

namespace Anticontrafact2.Api
{
    public class AntiCounterfeitApiService
    {
        private static AntiCounterfeitApiService instance;

        public static AntiCounterfeitApiService getInstance()
        {
            if (instance == null)
            {
                instance = new AntiCounterfeitApiService();
            }
            return instance;
        }

        private AntiCounterfeitApiService()
        {
            Api = RestService.For<IAntiCounterfeitApi>("http://godnext-001-site1.btempurl.com/api");
        }

        public IAntiCounterfeitApi Api { get; }

        public bool IsAvailable()
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    using (webClient.OpenRead("http://godnext-001-site1.btempurl.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
