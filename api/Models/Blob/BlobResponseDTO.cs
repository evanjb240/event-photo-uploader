namespace Events.Models.Blob
{
    public class BlobResponseDTO
    {
        #nullable enable
        public string? Status { get; set; }
        public bool Error { get; set; }
        public BlobDTO Blob { get; set; }

        public BlobResponseDTO()
        {
            Blob = new BlobDTO();
        }
    }
}