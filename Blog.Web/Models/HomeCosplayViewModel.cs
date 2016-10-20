using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class HomeCosplayViewModel
    {
        public IEnumerable<PostCategoryViewModel> ChildCategory { set; get; }

        public IEnumerable<PostViewModel> Cosplays { set; get; }
    }
}