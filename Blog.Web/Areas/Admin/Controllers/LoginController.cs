using Blog.Common;
using Blog.Model.Dao;
using Blog.Web.Areas.Admin.Models;
using System.Web.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private UserDao _userDao = new UserDao();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userDao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result > 0)
                {
                    var user = _userDao.GetByUserName(model.UserName);
                    var session = new SessionModel();
                    session.UserName = user.UserName;
                    session.UserID = user.ID;
                    session.Name = user.Name;
                    session.Email = user.Email;
                    session.Phone = user.Phone;
                    session.Address = user.Address;
                    session.GroupID = user.GroupID;
                    var listCredentials = _userDao.GetListCredential(model.UserName);
                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(CommonConstants.ADMIN_SESSION, session);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng.");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.ADMIN_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}