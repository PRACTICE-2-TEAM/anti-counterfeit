using Refit;
using System.Threading.Tasks;

namespace Anticontrafact2.Api
{
    public interface IAntiCounterfeitApi
    {
        /// <summary>
        /// Проверяет товар по указанному штрих-коду.
        /// </summary>
        /// <param name="barcode">номер штрих-кода</param>
        /// <returns>результат проверки</returns>
        [Get("/check_barcode")]
        Task<BarcodeInfo> CheckBarcode(string barcode);

        /// <summary>
        /// Проверяет торговую точку по указанному ИНН.
        /// </summary>
        /// <param name="tin">ИНН</param>
        /// <returns>результат проверки</returns>
        [Get("/check_outlet")]
        Task<OutletInfo> CheckOutlet(string tin);

        /// <summary>
        /// Запрашивает у сервера отправку на указанный адрес
        /// электронной почты кода подтверждения для завершения
        /// регистрации.
        /// </summary>
        /// <param name="email">адрес электронной почты</param>
        /// <returns>результат запроса</returns>
        [Get("/sign_up")]
        Task<SignUpInfo> SignUp(string email);

        /// <summary>
        /// Отправляет на сервер данные для завершения регистрации.
        /// </summary>
        /// <param name="email">адрес электронной почты</param>
        /// <param name="pass">пароль</param>
        /// <param name="code">код подтверждения</param>
        /// <returns>результат регистрации</returns>
        [Get("/registration")]
        Task<RegistrationInfo> Register(string email, string pass, string code);

        /// <summary>
        /// Запрос на авторизацию.
        /// </summary>
        /// <param name="email">адрес электронной почты</param>
        /// <param name="pass"></param>
        /// <returns>токен</returns>
        [Get("/login")]
        Task<LogInInfo> LogIn(string email, string pass);
    }
}
