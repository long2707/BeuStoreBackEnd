using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string userId { get; set; }
    
      
        public string refreshToken { get; set; } =string.Empty;
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
