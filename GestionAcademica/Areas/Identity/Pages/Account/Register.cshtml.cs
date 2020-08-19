using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GestionAcademica.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace GestionAcademica.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            public int DocumentType { get; set; }

            [Required]
            public string Document { get; set; }

            [Required]
            [Display(Name = "Nombre")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Apellidos")]
            public string Surname { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Fecha de Nacimiento")]
            public string Birthdate { get; set; }

            public string Gender { get; set; }

            [Required(AllowEmptyStrings = true)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Address { get; set; }

            [Required(AllowEmptyStrings = true)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Phone { get; set; }

            public string Telephone { get; set; }
            [Required(ErrorMessage = "El capo usuario es requerido")]
            [Display(Name = "Usuario")]
            public string UserName { get; set; }

            [Required(ErrorMessage="El password es requerido")]
            [StringLength(100, ErrorMessage = "El campo {0} debe tener minimo {2} y maximo {1} caracteres de longitud", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Los passwords no coinciden.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    DocumentType = Input.DocumentType,
                    Document = Input.Document,
                    Name = Input.Name,
                    Surname = Input.Surname,
                    UserName = Input.UserName,
                    Email = Input.Email,
                    Birthdate = Convert.ToDateTime(Input.Birthdate),
                    Gender = Input.Gender,
                    Address = Input.Address,
                    Phone = Input.Phone,
                    Telephone = Input.Telephone
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Cuenta de usuario creada con password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirma tu email",
                        $"Porfavor confirma tue cuenta<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Has click aqui!</a>.");

                    // check if role user exist
                    var roleUser = await _roleManager.RoleExistsAsync("User");
                    // add user role to user
                    if (!roleUser)
                    {
                        var role = new IdentityRole("User");
                        var res = await _roleManager.CreateAsync(role);

                        if (res.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            _logger.LogInformation("Cuenta de usuario creada");
                            return LocalRedirect(returnUrl);
                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("Cuenta de usuario creada");
                        return LocalRedirect(returnUrl);
                    }
                    
                    

                    /*
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }*/
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
