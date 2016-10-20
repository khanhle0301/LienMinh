using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class PostViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Nhập tiều đề")]
        [Display(Name = "Tiêu đề")]
        public string Name { set; get; }

        public string Alias { set; get; }

        [Display(Name = "Danh mục")]
        public int CategoryID { set; get; }

        [Required(ErrorMessage = "Chọn ảnh")]
        [Display(Name = "Hình ảnh")]
        public string Image { set; get; }

        [Required(ErrorMessage = "Nhập mô tả ngắn")]
        [Display(Name = "Mô tả ngắn")]
        public string Description { set; get; }

        [Required(ErrorMessage = "Nhập nội dung")]
        [Display(Name = "Nội dung")]
        public string Content { set; get; }

        [Display(Name = "Tin hot trong tuần")]
        public bool? HotFlag { set; get; }

        [Display(Name = "Tin nỗi bật")]
        public bool? HotNewsFlag { set; get; }

        [Display(Name = "Tin sự kiện")]
        public bool? EventFlag { set; get; }

        public int? ViewCount { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        [Display(Name = "Từ khóa")]
        public string MetaKeyword { set; get; }

        [Display(Name = "Description")]
        public string MetaDescription { set; get; }

        public string Tags { set; get; }

        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }

        public virtual PostCategoryViewModel PostCategory { set; get; }
    }
}