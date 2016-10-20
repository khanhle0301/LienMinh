using Blog.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Model.Dao
{
    public class BannerDao
    {
        private BlogDbContext db = null;

        public BannerDao()
        {
            db = new BlogDbContext();
        }

        public IEnumerable<Banner> GetBannerCategory()
        {
            return db.Banners.Where(x => x.Type == "banner_category");
        }

        public Banner GetBannerByType(string type)
        {
            return db.Banners.SingleOrDefault(x => x.Type == type);
        }

        public IEnumerable<Banner> GetAll()
        {
            return db.Banners;
        }

        public Banner GetById(int id)
        {
            return db.Banners.Find(id);
        }

        public bool Update(Banner entity)
        {
            try
            {
                var model = db.Banners.Find(entity.ID);
                model.Name = entity.Name;
                model.Description = entity.Description;
                model.Image = entity.Image;
                model.Url = entity.Url;
                model.Type = entity.Type;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}