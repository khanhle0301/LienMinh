using Blog.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Model.Dao
{
    public class BlockDao
    {
        private BlogDbContext db = null;

        public BlockDao()
        {
            db = new BlogDbContext();
        }

        public Block GetBlockByType(string type)
        {
            return db.Blocks.SingleOrDefault(x => x.Type == type);
        }

        public IEnumerable<Block> GetAll()
        {
            return db.Blocks;
        }

        public Block GetById(int id)
        {
            return db.Blocks.Find(id);
        }

        public bool Update(Block entity)
        {
            try
            {
                var model = db.Blocks.Find(entity.ID);
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