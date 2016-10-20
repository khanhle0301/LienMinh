using AutoMapper;
using Blog.Common;
using Blog.Model.Dao;
using Blog.Model.Models;
using Blog.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private PostDao _postDao = new PostDao();
        private PostCategoryDao _postCategoryDao = new PostCategoryDao();
        private BannerDao _bannerDao = new BannerDao();
        private BlockDao _blockDao = new BlockDao();

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByStatus());
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Logo()
        {
            ViewBag.Logo = ConfigHelper.GetByKey("Logo");
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Social()
        {
            ViewBag.Face = ConfigHelper.GetByKey("Face");
            ViewBag.You = ConfigHelper.GetByKey("You");
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult BannerTop()
        {
            var topBanner = Mapper.Map<Banner, BannerViewModel>(_bannerDao.GetBannerByType("banner_top"));
            return PartialView(topBanner);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult NewsTop()
        {
            HomeNewsTopViewModel homeNewsVm = new HomeNewsTopViewModel();
            var blockLeft = Mapper.Map<Block, BlockViewModel>(_blockDao.GetBlockByType("block_left"));
            var blockRightTop = Mapper.Map<Block, BlockViewModel>(_blockDao.GetBlockByType("block_right_top"));
            var blockRightBottomLeft = Mapper.Map<Block, BlockViewModel>(_blockDao.GetBlockByType("block_right_bottom_left"));
            var blockRightBottomRight = Mapper.Map<Block, BlockViewModel>(_blockDao.GetBlockByType("block_right_bottom_right"));
            var eventNews = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetEventNews(6));

            homeNewsVm.BlockLeft = blockLeft;
            homeNewsVm.BlockRightTop = blockRightTop;
            homeNewsVm.BlockRightBottomLeft = blockRightBottomLeft;
            homeNewsVm.BlockRightBottomRight = blockRightBottomRight;
            homeNewsVm.EventNews = eventNews;
            ViewBag.Image = ConfigHelper.GetByKey("NewsEvent");
            return PartialView(homeNewsVm);
        }

        [ChildActionOnly]
        public ActionResult NewsPost()
        {
            HomeLastedNewsViewModel homNewsVm = new HomeLastedNewsViewModel();
            var lastedNews = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetLastedNews(20));
            var bannerNew = Mapper.Map<Banner, BannerViewModel>(_bannerDao.GetBannerByType("banner_tintuc_home"));
            homNewsVm.LastedNews = lastedNews;
            homNewsVm.BannerNews = bannerNew;
            return PartialView(homNewsVm);
        }

        [ChildActionOnly]
        public ActionResult NewsGames()
        {
            HomeNewsGamesViewModel homNewsGameVm = new HomeNewsGamesViewModel();
            var newsGame = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(1, 6));

            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(1));

            var newsHot = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetNewsHotOfWeek(5));

            homNewsGameVm.ChildCategory = childCategories;
            homNewsGameVm.NewsGame = newsGame;
            homNewsGameVm.NewsHot = newsHot;
            return PartialView(homNewsGameVm);
        }

        [ChildActionOnly]
        public ActionResult NewsEsports()
        {
            HomeEsportViewModel homeEsportVm = new HomeEsportViewModel();
            var esports = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(2, 5));
            var cammang = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(3, 4));
            var bannerEsport = Mapper.Map<Banner, BannerViewModel>(_bannerDao.GetBannerByType("banner_tinesport_home"));

            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(2));

            homeEsportVm.ChildCategory = childCategories;
            homeEsportVm.BannerEsport = bannerEsport;
            homeEsportVm.Camnang = cammang;
            homeEsportVm.Esports = esports;
            return PartialView(homeEsportVm);
        }

        [ChildActionOnly]
        public ActionResult Video()
        {
            HomeVideoViewModel homeVideoVm = new HomeVideoViewModel();

            var congdong = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(4, 5));
            var video = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(5, 6));
            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(5));

            homeVideoVm.ChildCategory = childCategories;
            homeVideoVm.Congdong = congdong;
            homeVideoVm.Videos = video;
            return PartialView(homeVideoVm);
        }

        [ChildActionOnly]
        public ActionResult Cosplay()
        {
            HomeCosplayViewModel homeCosplayVm = new HomeCosplayViewModel();
            var cosplay = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(6, 3));
            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(6));
            homeCosplayVm.ChildCategory = childCategories;
            homeCosplayVm.Cosplays = cosplay;
            return PartialView(homeCosplayVm);
        }
    }
}