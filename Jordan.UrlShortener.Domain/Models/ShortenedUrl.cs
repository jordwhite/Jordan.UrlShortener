using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Jordan.UrlShortener.Domain.Models
{
    public class ShortenedUrl
    {
        public string Id { get; set; }
        public string FullUrl { get; set; }
        public string ClientIp { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
