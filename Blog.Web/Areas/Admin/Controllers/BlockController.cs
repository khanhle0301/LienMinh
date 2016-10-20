using AutoMapper;
using Blog.Model.Dao;
using Blog.Model.Models;
using Blog.Web.Common;
using Blog.Web.Infrastructure.Extensions;
using Blog.Web.Models;
using System.Web.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    public class BlockController : BaseController
    {
        private BlockDao _blockDao = new BlockDao();

        [HasCredential(RoleID = "VIEW_BLOCK")]
        public ActionResult Index()
        {
            var result = _blockDao.GetAll();
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_BLOCK")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Mapper.Map<Block, BlockViewModel>(_blockDao.GetById(id));
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_BLOCK")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BlockViewModel model)
        {
            if (ModelState.IsValid)
            {
                var block = new Block();
                block.UpdateBlock(model);
                var res = _blockDao.Update(block);
                if (res)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Block");
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