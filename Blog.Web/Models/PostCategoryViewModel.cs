using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class PostCategoryViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Nhập tên danh mục")]
        [Display(Name = "Tên danh mục")]
        public string Name { set; get; }

        public string Alias { set; get; }

        [Display(Name = "Mô tả ngắn")]
        public string Description { set; get; }

        [Required(ErrorMessage = "Chọn danh mục cha")]
        [Display(Name = "Danh mục cha")]
        public int? ParentID { set; get; }

        [Required(ErrorMessage = "Nhập thứ tự")]
        [Display(Name = "Thứ tự")]
        public int? DisplayOrder { set; get; }

        public virtual IEnumerable<PostViewModel> Posts { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        [Display(Name = "Từ khóa")]
        public string MetaKeyword { set; get; }

        [Display(Name = "Description")]
        public string MetaDescription { set; get; }

        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
    }
}