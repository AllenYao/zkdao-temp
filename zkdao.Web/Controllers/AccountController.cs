using System.Web.Mvc;
using System.Web.Security;
using zkdao.Web.Models;
using zkdao.Web.UserServiceReference;

namespace zkdao.Web.Controllers {

    public class AccountController : ControllerBase {

        public ActionResult LogOn() {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(UserLogOn model, string returnUrl) {
            if (ModelState.IsValid) {
                if (Membership.ValidateUser(model.Email, model.LogOnPassword)) {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\")) {
                        return Redirect(returnUrl);
                    } else {
                        return RedirectToAction("Index", "Home");
                    }
                } else {
                    ModelState.AddModelError(string.Empty, "用户名或密码错误。");
                }
            }
            return View(model);
        }

        public ActionResult LogOff() {
            FormsAuthentication.SignOut();
            CustomerID = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegister model) {
            if (ModelState.IsValid) {
                MembershipCreateStatus createStatus = MembershipCreateStatus.ProviderError;
                Membership.CreateUser(model.Email, model.Password, model.Email, null, null, true, null, out createStatus);
                if (createStatus == MembershipCreateStatus.Success) {
                    FormsAuthentication.SetAuthCookie(model.Email, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                } else if (createStatus == MembershipCreateStatus.InvalidEmail) {
                    TempData["WhiteTitle"] = "请去注册邮箱获取验证邮件，用以激活帐号。";
                    TempData["WhiteContent"] = "您的注册邮箱：<a href='mailto:" + model.Email + "'>" + model.Email + "</a>，如果长时间未收到激活邮件，请 <a href='/Account/ReSendApproved'>点击这里</a> 重新发送邮件。";
                    return RedirectToAction("White", "Home");
                } else {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }
            return View(model);
        }

        public ActionResult Approved(string approvedID) {
            bool ok;
            if (string.IsNullOrEmpty(approvedID)) {
                ok = false;
            }
            using (UserServiceClient svc = new UserServiceClient()) {
                ok = svc.UserApproved(User.Identity.Name, approvedID);
            }
            if (ok) {
                TempData["WhiteTitle"] = "验证成功！感谢注册！";
            } else {
                TempData["WhiteTitle"] = "验证失败。请 <a href='/Account/ReSendApproved'>点击这里</a> 重新申请验证邮件。";
                TempData["WhiteContent"] = "您的注册邮箱：<a href='mailto:" + User.Identity.Name + "'></a>";
                TempData["WhiteColor"] = "c_red";
            }
            return RedirectToAction("White", "Home");
        }

        [Authorize]
        public ActionResult ReSendApproved() {
            using (UserServiceClient svc = new UserServiceClient()) {
                svc.UserRequestApproved(User.Identity.Name);
            }
            TempData["WhiteTitle"] = "请去注册邮箱获取验证邮件，用以激活帐号。";
            TempData["WhiteContent"] = "您的注册邮箱：<a href='mailto:" + User.Identity.Name + "'>" + User.Identity.Name + "</a>，如果长时间未收到激活邮件，请 <a href='/Account/ReSendApproved'>点击这里</a> 重新发送邮件。";
            return RedirectToAction("White", "Home");
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus) {
            switch (createStatus) {
                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名已被他人使用。";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Email地址已被他人使用。";

                case MembershipCreateStatus.InvalidPassword:
                    return "密码错误。";

                case MembershipCreateStatus.InvalidEmail:
                    return "Email地址错误。";

                case MembershipCreateStatus.InvalidAnswer:
                    return "安全验证的答案错误。";

                case MembershipCreateStatus.InvalidQuestion:
                    return "安全验证的问题是无效的。";

                case MembershipCreateStatus.InvalidUserName:
                    return "用户名错误。";

                case MembershipCreateStatus.ProviderError:
                    return "系统错误，请在页脚联系管理员。";

                case MembershipCreateStatus.UserRejected:
                    return "新建用户请求被驳回，详细原因请在页脚联系管理员。";

                default:
                    return "未知错误，详细原因请在页脚联系管理员。";
            }
        }

        [Authorize]
        public ActionResult ChangePassword() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(UserChangePassword model) {
            if (ModelState.IsValid) {
                bool succeeded = false;
                MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                if (currentUser == null) {
                    ModelState.AddModelError("", "用户验证失败。");
                    return View(model);
                }
                succeeded = currentUser.ChangePassword(model.Password, model.NewPassword);
                if (!succeeded) {
                    ModelState.AddModelError("", "更改密码失败。");
                } else {
                    TempData["WhiteTitle"] = "已成功更改密码。";
                    return RedirectToAction("White", "Home");
                }
            }
            return View(model);
        }
    }
}