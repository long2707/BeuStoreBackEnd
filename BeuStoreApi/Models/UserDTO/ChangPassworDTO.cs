namespace BeuStoreApi.Models.UserDTO
{
    public class ChangPassworDTO
    {
        public string IdUser { get; set; } = string.Empty;
        public string oldPassword { get; set; } = string.Empty;
        public string newPassword { get; set; } = string.Empty;
    }
}
