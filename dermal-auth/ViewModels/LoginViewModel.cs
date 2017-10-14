using dermal.auth.Models;

namespace dermal.auth.ViewModels
{
    public class LoginViewModel : LoginInputModel
    {
        public bool AllowRememberLogin { get; set; }
        public bool EnableLocalLogin { get; set; }
    }
}