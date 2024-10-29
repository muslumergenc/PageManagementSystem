using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PageManagementSystem.Application.Interfaces;
using PageManagementSystem.UI.Models.VMs.AccountVMs;

namespace PageManagementSystem.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSenderService _emailSender;
        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IEmailSenderService emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }

                ModelState.AddModelError(string.Empty, "Kulllanıcı Adı veya Şifre Hatalı !");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("Index");
                }

                // Şifre sıfırlama token'ı oluştur
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Şifre sıfırlama bağlantısı
                var resetLink = Url.Action("ResetPassword", "Login", new { token, email = model.Email }, Request.Scheme);

                // Şifre sıfırlama e-postası gönderme
                var subject = "Şifre Sıfırlama Talebi";
                var message = $"Şifrenizi sıfırlamak için <a href='{resetLink}'>buraya tıklayın</a>.";
                try
                {
                    await _emailSender.SendEmailAsync(model.Email, subject, message);
                }
                catch (Exception ex)
                {
                    return View(ex.Message.ToString());
                }


                return RedirectToAction("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz şifre sıfırlama isteği.");
                return RedirectToAction("AccessDenied");
            }

            return View(new ResetPasswordViewModel { Token = token, Email = email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("AccessDenied");
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                foreach (var error in result.Errors)
                {
                    if (error.Code == "InvalidToken")
                    {
                        ModelState.AddModelError(string.Empty, "Bu şifre sıfırlama bağlantısı artık geçersiz.");
                        return RedirectToAction("AccessDenied");
                    }
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View(new LoginViewModel());
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
