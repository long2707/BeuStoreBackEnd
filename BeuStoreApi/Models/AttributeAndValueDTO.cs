using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Models
{
    public class AttributeAndValueDTO
    {


        //public class AttributeDTO
        //{
            [FromForm]
            public string? attribute_name { get; set; }
        [FromForm]
        public string[]? valueAttribute { get; set; } = { };
    }
    public class UpdateAttributeDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UpdateAttributeValueDTO> updateAttributeValues { get; set; } = new List<UpdateAttributeValueDTO>();
    }
    public class UpdateAttributeValueDTO
    {
        public string Name { get; set; } = string.Empty;
    }

}

