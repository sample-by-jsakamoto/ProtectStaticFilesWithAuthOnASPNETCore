using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ProtectStaticFilesWithAuth.Pages
{
    public class IndexModel : PageModel
    {
        private readonly string SignInScheme;

        public IndexModel(IOptions<AuthenticationOptions> options)
        {
            this.SignInScheme = options.Value.DefaultScheme;
        }

        public async Task<IActionResult> OnPost(string command)
        {
            switch (command)
            {
                case "Sign in":
                    var principal = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, "Test User")
                    }, SignInScheme));
                    await this.HttpContext.SignInAsync(principal);
                    return Redirect("/");

                case "Sign out":
                    await this.HttpContext.SignOutAsync();
                    return Redirect("/");

                default: throw new Exception($"Unknown command: \"{command}\"");
            }
        }
    }
}
