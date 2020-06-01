using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PomeloButterBlog.Common.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PomeloButterBlog.App.HttpClientApi
{
    public partial class BlogApi
    {
        public async Task<PostDetailsModel> GetPostByIdAsync(string url)
        {
            var result = await WorkingAsync(HttpMethod.Get, "/BlogApi/GetPostById?url="+ url);

            var models = JsonConvert.DeserializeObject<PostDetailsModel>(result);
            return models;
        }

        public async Task<PagedViewModel<PostViewModel>> GetPostListAsync(string tag, string catalog, int pageIndex = 1, int pageSize = 8)
        {
            var result = await WorkingAsync(HttpMethod.Get, $"/BlogApi/GetPostList?tag={tag}&catalog={catalog}&pageIndex={pageIndex}&pageSize={pageSize}");

            var models = JsonConvert.DeserializeObject<PagedViewModel<PostViewModel>>(result);
            return models;
        }
    }
}
