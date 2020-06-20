using Refit;

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
    }
}
