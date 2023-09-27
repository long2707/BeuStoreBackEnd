using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User user { get; set; } = new User();
        public string refreshToken { get; set; } =string.Empty;
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
