using AutoMapper;
using Blog.Model.Models;
using Blog.Web.Models;

namespace Blog.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();       
            Mapper.CreateMap<Block, BlockViewModel>();
            Mapper.CreateMap<Banner, BannerViewModel>();
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<UserGroup, UserGroupViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
        }
    }
}