using Refit;
using System.Threading.Tasks;

namespace Anticontrafact2.Api
{
    public interface IAntiCounterfeitApi
    {
        [Get("/check_barcode")]
        Task<ProductInformation> GetProductInformation([AliasAs("barcode")] string barcode);

        [Get("/check_outlet")]
        Task<OutletInformation> GetOutletInformation([AliasAs("tin")] string tin);

        [Get("/sign_up")]
        Task<SignUpResult> RequestCode([AliasAs("email")] string email);

        [Get("/registration")]
        Task<RegistrationInfo> SignUp([AliasAs("email")] string email, [AliasAs("pass")] string password, [AliasAs("code")] string code);

        [Get("/login")]
        Task<LogInResult> LogIn([AliasAs("email")] string email, [AliasAs("pass")] string password);

        // TODO:
        //  - /complain_outlet
        //  - /complain_product
        //  - /get_outlet_complaints
        //  - /get_product_complaints
    }
}
