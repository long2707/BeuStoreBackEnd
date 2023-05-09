using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string userId { get; set; }
        [ForeignKey(nameof(userId))]
        public Staff staff { get; set; }
        public string refreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
