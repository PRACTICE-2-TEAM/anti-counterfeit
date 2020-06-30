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
        Task<RegistrationInfo> SignUp([AliasAs("email")] string email,
                                      [AliasAs("pass")] string password,
                                      [AliasAs("code")] string code);

        [Get("/login")]
        Task<LogInResult> LogIn([AliasAs("email")] string email,
                                [AliasAs("pass")] string password);

        [Get("/showrequest")]
        Task<ComplaintInputData> GetComplaintData([AliasAs("token")] string token,
                                             [AliasAs("id")] string id);

        [Post("/complain_product")]
        Task<ComplaintResult> Complain([Body] ComplaintOutputData data);

        [Get("/usergetcomplains")]
        Task<ComplaintIdentifier[]> GetComplaintIdentifiers([AliasAs("token")] string token,
                                                            [AliasAs("count")] int count,
                                                            [AliasAs("page")] int page);
    }
}
