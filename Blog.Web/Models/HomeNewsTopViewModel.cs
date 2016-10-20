using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class HomeNewsTopViewModel
    {
        public BlockViewModel BlockLeft { set; get; }
        public BlockViewModel BlockRightTop { set; get; }
        public BlockViewModel BlockRightBottomLeft { set; get; }
        public BlockViewModel BlockRightBottomRight { set; get; }
        public IEnumerable<PostViewModel> EventNews { set; get; }
    }
}