using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PomeloButterBlog.Common.Models
{

    public class PagedViewModel<T>
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public int TotalPage { get; set; }
        [DataMember]
        public int CurrentPage { get; set; }
        [DataMember]
        public IList<T> Data { get; set; }
    }
}