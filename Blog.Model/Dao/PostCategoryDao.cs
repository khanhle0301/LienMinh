using Blog.Common;
using Blog.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Model.Dao
{
    public class PostCategoryDao
    {
        private BlogDbContext db = null;

        public PostCategoryDao()
        {
            db = new BlogDbContext();
        }

        public PostCategory GetByChild(int parentId)
        {
            return db.PostCategories.SingleOrDefault(x => x.ID == parentId);
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return db.PostCategories.Where(x => x.Status && x.ParentID == parentId);
        }

        public IEnumerable<PostCategory> GetAllByStatus()
        {
            return db.PostCategories.Where(x => x.Status).OrderBy(x => x.DisplayOrder);
        }

        public List<PostCategory> ListAll()
        {
            return db.PostCategories.ToList();
        }

        public IEnumerable<PostCategory> ListByParent()
        {
            return db.PostCategories.Where(x => x.ParentID == null);
        }

        public PostCategory GetById(int id)
        {
            return db.PostCategories.Find(id);
        }

        public IEnumerable<PostCategory> ListByChild()
        {
            return db.PostCategories.Where(x => x.ParentID != null);
        }

        public bool Delete(int id)
        {
            try
            {
                var cate = db.PostCategories.Find(id);
                db.PostCategories.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Insert(PostCategory entity)
        {
            if (db.PostCategories.Any(x => x.Name == entity.Name))
                return 0;
            db.PostCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public int Update(PostCategory entity)
        {
            if (db.PostCategories.Any(x => x.Name == entity.Name && x.ID != entity.ID))
                return 0;
            var model = db.PostCategories.Find(entity.ID);
            model.Name = entity.Name;
            model.Alias = StringHelper.ToUnsignString(entity.Name);
            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = entity.UpdatedBy;
            model.MetaKeyword = entity.MetaKeyword;
            model.ParentID = entity.ParentID;
            model.MetaDescription = entity.MetaDescription;
            model.Status = entity.Status;
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<PostCategory> GetPostCategroy()
        {
            return db.PostCategories.Where(x => x.Status);
        }

        public IEnumerable<PostCategory> ListAllPaging()
        {
            return db.PostCategories.OrderByDescending(x => x.CreatedDate);
        }

        public PostCategory GetByAlias(string alias)
        {
            return db.PostCategories.SingleOrDefault(x => x.Alias == alias);
        }
    }
}