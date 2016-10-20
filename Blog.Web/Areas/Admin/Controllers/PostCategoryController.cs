using AutoMapper;
using Blog.Common;
using Blog.Model.Dao;
using Blog.Model.Models;
using Blog.Web.Areas.Admin.Models;
using Blog.Web.Common;
using Blog.Web.Infrastructure.Extensions;
using Blog.Web.Models;
using System;
using System.Web.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class PostCategoryController : BaseController
    {
        private PostCategoryDao _postCategoryDao = new PostCategoryDao();

        [HasCredential(RoleID = "VIEW_POSTCATEGORY")]
        public ActionResult Index()
        {
            var model = _postCategoryDao.ListByChild();
            return View(model);
        }

        [HasCredential(RoleID = "ADD_POSTCATEGORY")]
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HasCredential(RoleID = "ADD_POSTCATEGORY")]
        [HttpPost]
        public ActionResult Create(PostCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Alias = StringHelper.ToUnsignString(model.Name);
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                var session = (SessionModel)Session[CommonConstants.ADMIN_SESSION];
                model.CreatedBy = session.Name;
                model.UpdatedBy = session.Name;
                var postCategory = new PostCategory();
                postCategory.UpdatePostCategory(model);
                var result = _postCategoryDao.Insert(postCategory);

                if (result == 0)
                {
                    ModelState.AddModelError("", "Tên danh mục đã tồn tại");
                }
                else if (result > 0)
                {
                    SetAlert("Thêm danh thành công", "success");
                    return RedirectToAction("Index", "PostCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            SetViewBag();
            return View(model);
        }

        public void SetViewBag(int? selectedId = null)
        {
            ViewBag.ParentID = new SelectList(_postCategoryDao.ListByParent(), "ID", "Name", selectedId);
        }

        [HasCredential(RoleID = "EDIT_POSTCATEGORY")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = Mapper.Map<PostCategory, PostCategoryViewModel>(_postCategoryDao.GetById(id));
            SetViewBag(result.ParentID);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_POSTCATEGORY")]
        [HttpPost]
        public ActionResult Edit(PostCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Alias = StringHelper.ToUnsignString(model.Name);
                model.UpdatedDate = DateTime.Now;
                var session = (SessionModel)Session[CommonConstants.ADMIN_SESSION];
                model.UpdatedBy = session.Name;
                var postCategory = new PostCategory();
                postCategory.UpdatePostCategory(model);
                var result = _postCategoryDao.Update(postCategory);
                if (result == 0)
                {
                    ModelState.AddModelError("", "Tên danh mục đã tồn tại");
                }
                else if (result > 0)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "PostCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            SetViewBag(model.ParentID);
            return View(model);
        }

        [HasCredential(RoleID = "DELETE_POSTCATEGORY")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = _postCategoryDao.GetById(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_POSTCATEGORY")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var result = _postCategoryDao.Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "PostCategory");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var postCate = _postCategoryDao.GetById(id);
                return View(postCate);
            }
        }
    }
}