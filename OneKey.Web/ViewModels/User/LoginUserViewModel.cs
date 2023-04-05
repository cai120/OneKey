using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneKey.Web.ViewModels.User;
public class LoginUserViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    public bool Failed { get; set; } = false;
    public string? ErrorMessage { get; set; }

}