using AutoMapper;
using Blog.Common;
using Blog.Model.Dao;
using Blog.Model.Models;
using Blog.Web.Areas.Admin.Models;
using Blog.Web.Common;
using Blog.Web.Infrastructure.Extensions;
using Blog.Web.Models;
using System.Web.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private UserDao _userDao = new UserDao();
        private UserGroupDao _userGrDao = new UserGroupDao();

        // GET: Admin/AdminUser
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index()
        {
            var result = _userDao.GetAll();
            return View(result);
        }

        [HasCredential(RoleID = "ADD_USER")]
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HasCredential(RoleID = "ADD_USER")]
        [HttpPost]
        public ActionResult Create(RegisterModel adminModel)
        {
            if (ModelState.IsValid)
            {
                var admin = new User();
                var encryptedMd5Pas = Encryptor.MD5Hash(adminModel.Password);
                admin.UserName = adminModel.UserName;
                admin.Password = encryptedMd5Pas;
                admin.Name = adminModel.Name;
                admin.Address = adminModel.Address;
                admin.Email = adminModel.Email;
                admin.Phone = adminModel.Phone;
                admin.GroupID = adminModel.GroupID;
                var id = _userDao.Insert(admin);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                    ModelState.AddModelError("", "Thêm thất bại");
            }
            SetViewBag(adminModel.GroupID);
            return View(adminModel);
        }

        [HasCredential(RoleID = "EDIT_USER")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = Mapper.Map<User, UserViewModel>(_userDao.GetById(id));
            SetViewBag(result.GroupID);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_USER")]
        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.UpdateUser(model);
                var res = _userDao.Update(user);
                if (res)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                    ModelState.AddModelError("", "Cập nhật thất bại");
            }
            SetViewBag(model.GroupID);
            return View(model);
        }

        [HasCredential(RoleID = "DELETE_USER")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = _userDao.GetById(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_USER")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var result = _userDao.Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var model = _userDao.GetById(id);
                return View(model);
            }
        }

        [HttpGet]
        public JsonResult UserNameExists(string username)
        {
            var result = _userDao.UserNameExists(username);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EmailExists(string email)
        {
            var result = _userDao.EmailExists(email);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public void SetViewBag(int? selectedId = null)
        {
            ViewBag.GroupID = new SelectList(_userGrDao.GetAll(), "ID", "Name", selectedId);
        }
    }
}