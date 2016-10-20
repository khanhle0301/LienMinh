using AutoMapper;
using Blog.Model.Dao;
using Blog.Model.Models;
using Blog.Web.Common;
using Blog.Web.Infrastructure.Extensions;
using Blog.Web.Models;
using System.Web.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class BannerController : BaseController
    {
        private BannerDao _bannerDao = new BannerDao();

        [HasCredential(RoleID = "VIEW_BANNER")]
        public ActionResult Index()
        {
            var result = _bannerDao.GetAll();
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_BANNER")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Mapper.Map<Banner, BannerViewModel>(_bannerDao.GetById(id));
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_BANNER")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BannerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var banner = new Banner();
                banner.UpdateBanner(model);
                var res = _bannerDao.Update(banner);
                if (res)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Banner");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            return View(model);
        }
    }
}