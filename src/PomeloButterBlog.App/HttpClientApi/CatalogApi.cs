using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PomeloButterBlog.Common.Models;

namespace PomeloButterBlog.App.HttpClientApi
{
    public partial class BlogApi
    {
        public async Task<IList<CatalogViewModel>> GetCatalogListAsync()
        {
            var result = await WorkingAsync(HttpMethod.Get, "/BlogApi/GetCatalogList/");
            IList<CatalogViewModel> models = JsonConvert.DeserializeObject<IList<CatalogViewModel>>(result);
            return models;
        }
    }
}