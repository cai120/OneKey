using Microsoft.AspNetCore.Mvc;

namespace OneKey.Web.ViewModels
{
    public class PasswordViewModel : BaseViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DetailsFor { get; set; }
        public string? Website { get; set; }
        public string? Description { get; set; }
    }
}
