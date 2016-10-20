using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class HomeEsportViewModel
    {
        public IEnumerable<PostCategoryViewModel> ChildCategory { set; get; }

        public BannerViewModel BannerEsport { set; get; }

        public IEnumerable<PostViewModel> Esports { set; get; }

        public IEnumerable<PostViewModel> Camnang { set; get; }
    }
}