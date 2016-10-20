using AutoMapper;
using Blog.Common;
using Blog.Model.Dao;
using Blog.Model.Models;
using Blog.Web.Infrastructure.Core;
using Blog.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class PostController : Controller
    {
        private PostDao _postDao = new PostDao();
        private PostCategoryDao _postCategoryDao = new PostCategoryDao();
        private BannerDao _bannerDao = new BannerDao();
        private BlockDao _blockDao = new BlockDao();
        private TagDao _tagDao = new TagDao();

        public ActionResult TinGame(int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var newsGame = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(1, 10));
            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(1));
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategoryPaging(1, page, pageSize, out totalRow));
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            ViewBag.ChildCategory = childCategories;
            ViewBag.NewsGame = newsGame;

            return View(paginationSet);
        }

        public ActionResult TinESports(int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var newsGame = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(2, 10));
            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(2));
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategoryPaging(2, page, pageSize, out totalRow));
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.ChildCategory = childCategories;
            ViewBag.NewsGame = newsGame;
            return View(paginationSet);
        }

        public ActionResult Camnang(int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var newsGame = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(3, 10));
            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(3));
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategoryPaging(3, page, pageSize, out totalRow));
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.ChildCategory = childCategories;
            ViewBag.NewsGame = newsGame;
            return View(paginationSet);
        }

        public ActionResult Congdong(int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var newsGame = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategory(4, 10));
            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(4));
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByParentCategoryPaging(4, page, pageSize, out totalRow));
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.ChildCategory = childCategories;
            ViewBag.NewsGame = newsGame;
            return View(paginationSet);
        }

        public ActionResult Video(int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSizeVideo"));
            int totalRow = 0;
            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(5));
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllVideoPaging(5, page, pageSize, out totalRow));
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.ChildCategory = childCategories;
            return View(paginationSet);
        }

        public ActionResult Hinhanh(int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSizeVideo"));
            int totalRow = 0;
            var childCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryDao.GetAllByParentId(6));
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllVideoPaging(6, page, pageSize, out totalRow));
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.ChildCategory = childCategories;
            return View(paginationSet);
        }

        public ActionResult Category(string alias, int page = 1)
        {
            var category = _postCategoryDao.GetByAlias(alias);
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSizeCategory"));
            int totalRow = 0;
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetAllByCategoryPaging(category.ID, page, pageSize, out totalRow));
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };         
            ViewBag.Category = category;         
            return View(paginationSet);
        }

        public ActionResult Detail(int id)
        {
            var post = _postDao.GetById(id);
            var viewModel = Mapper.Map<Post, PostViewModel>(post);
            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_postDao.GetListTagByPostId(id));

            var relatedPost = _postDao.GetReatedPosts(id, 6);
            ViewBag.RelatedPosts = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(relatedPost);
            _postDao.IncreaseView(id);          
            return View(viewModel);
        }

        public ActionResult ListByTag(string tagId, int page = 1)
        {
            ViewBag.Tag = _tagDao.GetById(tagId);
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSizeTag"));
            int totalRow = 0;
            var postModel = _postDao.GetAllByTagPaging(tagId, page, pageSize, out totalRow);
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }

        [ChildActionOnly]
        public ActionResult NewsHot()
        {
            var newsHot = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postDao.GetNewsHot(5));
            return PartialView(newsHot);
        }

        public ActionResult Banner()
        {
            var topBanner = Mapper.Map<IEnumerable<Banner>, IEnumerable<BannerViewModel>>(_bannerDao.GetBannerCategory());
            return PartialView(topBanner);
        }

        public ActionResult Search(string keyword, int page = 1)
        {
            ViewBag.Keyword = keyword;
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSizeSearch"));
            int totalRow = 0;
            var postModel = _postDao.GetAllBySearch(keyword, page, pageSize, out totalRow);
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }
    }
}