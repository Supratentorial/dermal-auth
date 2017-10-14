using dermal.auth.Models;

namespace dermal.auth.ViewModels
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; }
    }
}