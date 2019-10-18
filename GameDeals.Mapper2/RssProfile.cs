using AutoMapper;
using System.Linq;
using Post = GameDeals.Core.Model.Post;
using Feed = GameDeals.Core.Model.Feed;
using Category = GameDeals.Core.Model.Category;
using PostEntity = GameDeals.Data.Contracts.Model.Post;
using FeedEntity = GameDeals.Data.Contracts.Model.Feed;
using CategoryEntity = GameDeals.Data.Contracts.Model.Category;

namespace GameDeals.Mapper
{
    public class RssProfile : Profile
    {
        public RssProfile()
        {
            CreateMap<Post, PostEntity>()
                .ForMember(f => f.Seen, act => act.Ignore())
                .ForMember(f => f.Feed, act => act.Ignore());

            CreateMap<PostEntity, Post>()
                .ForMember(f => f.IsNew, act => act.MapFrom(p => !p.Seen));

            CreateMap<Feed, FeedEntity>()
                .ForMember(f => f.Posts, act => act.Ignore())
                .ForMember(f => f.Category, act => act.Ignore());

            CreateMap<FeedEntity, Feed>();

            CreateMap<Category, CategoryEntity>()
                .ForMember(f => f.Feeds, act => act.Ignore());

            CreateMap<CategoryEntity, Category>()
                .ForMember(
                    c => c.NewPosts,
                    act => act.MapFrom(c => c.Feeds.Sum(f => f.Posts.Count(p => !p.Seen))));
        }
    }
}
