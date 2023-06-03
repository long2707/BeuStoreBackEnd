namespace BeuStoreApi.Models.ProductsDTO
{
    public class AttributeDTO
    {
        public string? Name { get; set; }
        public string[]? valueAttribute { get; set; }
    }
    public class UpdateAttributeDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set;} = string.Empty;
        public ICollection<UpdateAttributeValueDTO> updateAttributeValues { get; set; }= new List<UpdateAttributeValueDTO>();
    }
    public class UpdateAttributeValueDTO
    {
        public string Name { get; set; } = string.Empty;
    }
}
