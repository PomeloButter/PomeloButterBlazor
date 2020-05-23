using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PomeloButterBlog.Api.Data;
using PomeloButterBlog.Common.Entites;
using PomeloButterBlog.Common.Models;

namespace PomeloButterBlog.Api.Controllers
{

    [Route("BlogApi")]
    public class BlogApiController : Controller
    {
        // GET
        private readonly BlogContext _context;

        public BlogApiController(BlogContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 获取Catalog集合
        /// </summary>
        /// <returns></returns>
        [Route("GetCatalogList")]
        
        public async Task<IList<CatalogViewModel>> GetCatalogListAsync()
        {
            var catalogViewModels = await _context.Catalogs.Select(x => new CatalogViewModel
            {
                DisplayName = x.DisplayName,
                CategoryName = x.CategoryName,
                Count = _context.Posts.Count(s => s.CategoryId == x.Id)
            }).ToListAsync();
            return catalogViewModels;
        }

        /// <summary>
        /// 获取Post
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Route("GetPostById")]
        public async Task<PostDetailsModel> GetPostByIdAsync(string url)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(s => s.Url == url);
            var category = await _context.Catalogs.FirstOrDefaultAsync(x => x.Id == post.CategoryId);
            var postTags = _context.PostTags.Where(s => s.PostId == post.Id).ToList();
            var tagViewModels = new List<TagViewModel>();
            foreach (var postTag in postTags)
            {
                var tag = _context.Tags.SingleOrDefault(s => s.Id == postTag.TagId);
                var tagViewModel = new TagViewModel();
                tagViewModel.DisplayName = tag.DisplayName;
                tagViewModel.TagName = tag.TagName;
                tagViewModels.Add(tagViewModel);
            }

            var previous = _context.Posts
                .Where(x => x.CreationTime > post.CreationTime)
                .Take(1)
                .FirstOrDefault();

            var next = _context.Posts
                .Where(x => x.CreationTime < post.CreationTime)
                .OrderByDescending(x => x.CreationTime)
                .Take(1)
                .FirstOrDefault();

            return new PostDetailsModel
            {
                Title = post.Title,
                Author = post.Author,
                Url = post.Url,
                Html = post.Html,
                CreationTime = Convert.ToDateTime(post.CreationTime)
                    .ToString("MMMM dd, yyyy HH:mm:ss", new CultureInfo("en-us")),
                Category = new CatalogViewModel
                {
                    DisplayName = category.DisplayName,
                    CategoryName = category.CategoryName
                },

                Tags = tagViewModels,

                Previous = new PosPagedModel
                {
                    Title = previous?.Title,
                    Url = previous?.Url
                },
                Next = new PosPagedModel
                {
                    Title = next?.Title,
                    Url = next?.Url
                }
            };
        }

        /// <summary>
        /// 获取Post集合
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="catalog"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetPostList")]
        public async Task<PagedViewModel<PostViewModel>> GetPostListAsync(string tag, string catalog, int pageIndex = 1,
            int pageSize = 2)
        {
            var list = new List<Post>();
            var count = 0;
            if (!string.IsNullOrEmpty(tag))
            {
                var tagEntity = await _context.Tags.SingleOrDefaultAsync(s => s.DisplayName == tag);
                var postTags = _context.PostTags.ToList().FindAll(s => s.TagId == tagEntity.Id);
                var list1 = list;
                postTags.ForEach(x => { list1.Add(_context.Posts.SingleOrDefault(s => s.Id == x.Id)); });
                list = list1.OrderByDescending(x => x.CreationTime).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .ToList();
                count = postTags.Count;
            }
            else if (!string.IsNullOrEmpty(catalog))
            {
                var catalogEntity = await _context.Catalogs.SingleOrDefaultAsync(s => s.DisplayName == catalog);
                list = _context.Posts.Where(s => s.CategoryId == catalogEntity.Id).ToList();
                count = list.Count;
                list = list.OrderByDescending(x => x.CreationTime).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                    .ToList();
            }
            else
            {
                list = _context.Posts.OrderByDescending(x => x.CreationTime).Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).ToList();
                count = _context.Posts.Count();
            }

            var pageCount = (count + pageSize - 1) / pageSize;
            var postModels = list.Select(x => new PostModel
            {
                Id = x.Id,
                Title = x.Title,
                Url = x.Url,
                CreationTime = Convert.ToDateTime(x.CreationTime)
                    .ToString("MMMM dd, yyyy HH:mm:ss", new CultureInfo("en-us")),
                Year = Convert.ToDateTime(x.CreationTime).Year
            });
            var result = new List<PostViewModel>();
            var group = postModels.GroupBy(x => x.Year).ToList();
            group.ForEach(x =>
            {
                result.Add(new PostViewModel
                {
                    Year = x.Key,
                    Posts = x.ToList()
                });
            });
            return new PagedViewModel<PostViewModel>
            {
                Data = result,
                CurrentPage = pageIndex,
                TotalCount = count,
                TotalPage = pageCount
            };
        }

        /// <summary>
        /// 获取Tag集合
        /// </summary>
        /// <returns></returns>
        [Route("GetTagList")]
        public async Task<IList<TagViewModel>> GetTagListAsync()
        {
            return await _context.Tags.Select(x => new TagViewModel
            {
                TagName = x.TagName,
                DisplayName = x.DisplayName,
                Count = _context.PostTags.Count(s => s.TagId == x.Id)
            }).ToListAsync();
        }
    }
}