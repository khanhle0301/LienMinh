using Blog.Model.Models;
using System.Linq;

namespace Blog.Model.Dao
{
    public class TagDao
    {
        private BlogDbContext db = null;

        public TagDao()
        {
            db = new BlogDbContext();
        }

        public Tag GetById(string id)
        {
            return db.Tags.SingleOrDefault(x => x.ID == id);
        }
    }
}