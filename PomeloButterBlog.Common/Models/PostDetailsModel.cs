using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PomeloButterBlog.Common.Models
{
    public class PostDetailsModel
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Html { get; set; }
        [DataMember]
        public string Markdown { get; set; }
        [DataMember]
        public string CreationTime { get; set; }
        [DataMember]
        public CatalogViewModel Category { get; set; }
        [DataMember]
        public IList<TagViewModel> Tags { get; set; }
        [DataMember]
        public PosPagedModel Previous { get; set; }
        [DataMember]
        public PosPagedModel Next { get; set; }
    }
    public class PosPagedModel
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Url { get; set; }
    }
}