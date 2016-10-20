using System;

namespace Blog.Web.Areas.Admin.Models
{
    [Serializable]
    public class SessionModel
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public int GroupID { set; get; }
    }
}