using System.IO;
namespace Events.Models.Blob
{
    public class BlobDTO
    {
        #nullable enable
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Content { get; set; }
    }
}