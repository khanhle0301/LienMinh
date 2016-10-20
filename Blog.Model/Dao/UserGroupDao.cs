using Blog.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Model.Dao
{
    public class UserGroupDao
    {
        private BlogDbContext db = null;

        public UserGroupDao()
        {
            db = new BlogDbContext();
        }

        public IEnumerable<Role> ListAllRole()
        {
            return db.Roles;
        }

        public IEnumerable<Credential> GetCredentialByUserGroupID(int id)
        {
            return db.Credentials.Where(x => x.UserGroupID == id);
        }

        public IEnumerable<UserGroup> GetAll()
        {
            return db.UserGroups.Where(x => x.ID != 1);
        }

        public IEnumerable<UserGroup> ListAll()
        {
            return db.UserGroups;
        }

        public UserGroup GetById(int id)
        {
            return db.UserGroups.Find(id);
        }

        public int Insert(UserGroup entity)
        {
            if (db.UserGroups.Any(x => x.Name == entity.Name))
                return 0;
            db.UserGroups.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public int Update(UserGroup entity)
        {
            if (db.UserGroups.Any(x => x.Name == entity.Name && x.ID != entity.ID))
                return 0;
            var UserGroup = db.UserGroups.Find(entity.ID);
            UserGroup.Name = entity.Name;
            db.SaveChanges();
            return entity.ID;
        }

        public bool Delete(int id)
        {
            try
            {
                var UserGroup = db.UserGroups.Find(id);
                db.UserGroups.Remove(UserGroup);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}