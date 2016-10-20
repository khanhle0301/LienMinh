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
    public class PostController : BaseController
    {
        private PostDao _posDao = new PostDao();
        private PostCategoryDao _posCategoryDao = new PostCategoryDao();

        // GET: Admin/Post
        [HasCredential(RoleID = "VIEW_POST")]
        public ActionResult Index()
        {
            var model = _posDao.GetAll();
            return View(model);
        }

        [HasCredential(RoleID = "ADD_POST")]
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HasCredential(RoleID = "ADD_POST")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Alias = StringHelper.ToUnsignString(model.Name);
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                var session = (SessionModel)Session[CommonConstants.ADMIN_SESSION];
                model.CreatedBy = session.Name;
                model.UpdatedBy = session.Name;
                var post = new Post();
                post.UpdatePost(model);
                var res = _posDao.Insert(post);
                if (res > 0)
                {
                    SetAlert("Thêm danh thành công", "success");
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            SetViewBag();
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_POST")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var result = Mapper.Map<Post, PostViewModel>(_posDao.GetById(id));
            SetViewBag(result.CategoryID);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_POST")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    model.Alias = StringHelper.ToUnsignString(model.Name);
                    model.UpdatedDate = DateTime.Now;
                    var session = (SessionModel)Session[CommonConstants.ADMIN_SESSION];
                    model.UpdatedBy = session.Name;
                    var post = new Post();
                    post.UpdatePost(model);
                    var res = _posDao.Update(post);
                    if (res)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Post");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật thất bại");
                    }
                }
            }
            SetViewBag(model.ID);
            return View(model);
        }

        public void SetViewBag(int? selectedId = null)
        {
            ViewBag.CategoryID = new SelectList(_posCategoryDao.ListByChild(), "ID", "Name", selectedId);
        }

        [HasCredential(RoleID = "DELETE_POST")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _posDao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}