using System.ComponentModel.DataAnnotations;

namespace webbuilder.api.models
{
    public class Image
    {
        [Key]
        public required string ImageId { get; set; }

        public required byte[] ImageBlob { get; set; }

        public required string ImageName { get; set; }

        public required string UserId { get; set; }

        public User User { get; set; } = null!;
    }
}