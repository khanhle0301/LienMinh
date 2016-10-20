using Blog.Common;
using Blog.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Blog.Model.Dao
{
    public class PostDao
    {
        private BlogDbContext db = null;

        public PostDao()
        {
            db = new BlogDbContext();
        }

        public IEnumerable<Post> GetAllBySearch(string keyword, int page, int pageSize, out int totalRow)
        {
            var query = db.Posts.Where(x => x.Status && x.Name.Contains(keyword));
            totalRow = query.Count();
            return query.OrderByDescending(x => x.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Post> GetNewsHot(int top)
        {
            return db.Posts.Where(x => x.HotNewsFlag == true && x.Status).OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Post> GetAllVideoPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            IEnumerable<Post> query = Enumerable.Empty<Post>();
            var category = db.Posts.Find(categoryId);
            var childCategories = db.PostCategories.Where(x => x.ParentID == category.ID);
            foreach (var item in childCategories)
            {
                query = query.Concat(db.Posts.Where(x => x.Status && x.CategoryID == item.ID));
            }

            var queryParent = query.Concat(db.Posts.Where(x => x.Status && x.CategoryID == categoryId));
            totalRow = queryParent.Count();
            return queryParent.OrderByDescending(x => x.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Post> GetAllByParentCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            IEnumerable<Post> query = Enumerable.Empty<Post>();
            var category = db.Posts.Find(categoryId);
            var childCategories = db.PostCategories.Where(x => x.ParentID == category.ID);
            foreach (var item in childCategories)
            {
                query = query.Concat(db.Posts.Where(x => x.Status && x.CategoryID == item.ID));
            }

            var queryParent = query.Concat(db.Posts.Where(x => x.Status && x.CategoryID == categoryId));

            var queryPaging = queryParent.OrderByDescending(x => x.UpdatedDate).Skip(10);
            totalRow = queryPaging.Count();
            return queryPaging.OrderByDescending(x => x.UpdatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Post> GetNewsHotOfWeek(int top)
        {
            return db.Posts.Where(x => x.HotFlag == true && x.Status).OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Post> GetAllByParentCategory(int parentCategoryId, int top)
        {
            IEnumerable<Post> query = Enumerable.Empty<Post>();
            var category = db.Posts.Find(parentCategoryId);
            var childCategories = db.PostCategories.Where(x => x.ParentID == category.ID);
            foreach (var item in childCategories)
            {
                query = query.Concat(db.Posts.Where(x => x.Status && x.CategoryID == item.ID));
            }

            var queryParent = query.Concat(db.Posts.Where(x => x.Status && x.CategoryID == parentCategoryId));

            return queryParent.OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Post> GetLastedNews(int top)
        {
            return db.Posts.Include("PostCategory").Where(x => x.Status).OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Post> GetEventNews(int top)
        {
            return db.Posts.Include("PostCategory").Where(x => x.Status && x.EventFlag == true).OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertPostTag(int postId, string tagId)
        {
            PostTag postTag = new PostTag();
            postTag.PostID = postId;
            postTag.TagID = tagId;
            db.PostTags.Add(postTag);
            db.SaveChanges();
        }

        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public int Insert(Post post)
        {
            try
            {
                db.Posts.Add(post);
                db.SaveChanges();
                if (!string.IsNullOrEmpty(post.Tags))
                {
                    string[] tags = post.Tags.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.ToUnsignString(tag);
                        var existedTag = this.CheckTag(tagId);
                        //insert to to tag table
                        if (!existedTag)
                        {
                            this.InsertTag(tagId, tag);
                        }
                        //insert to product tag
                        this.InsertPostTag(post.ID, tagId);
                    }
                }
                return post.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool Update(Post post)
        {
            try
            {
                var model = db.Posts.Find(post.ID);
                model.Name = post.Name;
                model.Alias = StringHelper.ToUnsignString(post.Name);
                model.CategoryID = post.CategoryID;
                model.Image = post.Image;
                model.Description = post.Description;
                model.Content = post.Content;
                model.HotFlag = post.HotFlag;
                model.HotNewsFlag = post.HotNewsFlag;
                model.EventFlag = post.EventFlag;
                model.Tags = post.Tags;
                model.Status = post.Status;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = post.UpdatedBy;
                model.MetaKeyword = post.MetaKeyword;
                model.MetaDescription = post.MetaDescription;
                db.SaveChanges();
                //Xử lý tag
                this.RemoveAllContentTag(post.ID);
                if (!string.IsNullOrEmpty(post.Tags))
                {
                    string[] tags = post.Tags.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.ToUnsignString(tag);
                        var existedTag = this.CheckTag(tagId);
                        //insert to to tag table
                        if (!existedTag)
                        {
                            this.InsertTag(tagId, tag);
                        }
                        //insert to product tag
                        this.InsertPostTag(post.ID, tagId);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RemoveAllContentTag(int id)
        {
            db.PostTags.RemoveRange(db.PostTags.Where(x => x.PostID == id));
            db.SaveChanges();
        }

        public Post GetById(int id)
        {
            return db.Posts.Include("PostCategory").SingleOrDefault(x => x.ID == id);
        }

        public void Delete(int id)
        {
            var cate = db.Posts.Find(id);
            db.Posts.Remove(cate);
            db.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate);
        }

        public void IncreaseView(int id)
        {
            var post = db.Posts.Find(id);
            if (post.ViewCount.HasValue)
                post.ViewCount += 1;
            else
                post.ViewCount = 1;
            db.SaveChanges();
        }

        public IEnumerable<Post> GetReatedPosts(int id, int top)
        {
            var post = db.Posts.Find(id);
            return db.Posts.Include("PostCategory").Where(x => x.Status && x.ID != id && x.CategoryID == post.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Post> GetReatedTakePosts(int id, int top)
        {
            var post = db.Posts.Find(id);
            return db.Posts.Include("PostCategory").Where(x => x.Status && x.ID != id && x.CategoryID == post.CategoryID).OrderByDescending(x => x.CreatedDate).Skip(2).Take(top);
        }

        public Post GetAllById(int id)
        {
            return db.Posts.Include("PostCategory").SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<Tag> GetListTagByPostId(int id)
        {
            return db.PostTags.Include("Tag").Where(x => x.PostID == id).Select(y => y.Tag); ;
        }

        public IEnumerable<Post> GetNew(int top)
        {
            return db.Posts.Include("PostCategory").Where(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            var model = db.Posts.Include("PostCategory").Where(x => x.Status && x.CategoryID == categoryId).OrderByDescending(x => x.CreatedDate);
            totalRow = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Post> GetAllByTagPaging(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from a in db.Posts
                        join b in db.PostTags
                        on a.ID equals b.PostID
                        join c in db.PostCategories
                        on a.CategoryID equals c.ID
                        where b.TagID == tagId && a.Status
                        orderby a.CreatedDate descending
                        select a;
            var model = query.Include("PostCategory");
            totalRow = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            var model = db.Posts.Include("PostCategory").Where(x => x.Status).OrderByDescending(x => x.CreatedDate);
            totalRow = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}