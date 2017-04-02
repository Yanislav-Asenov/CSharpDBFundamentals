namespace ProductsShop.Xml.Client.Dtos
{
    using System.Collections.Generic;

    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ProductWithBuyerDto> ProductsSold { get; set; } = new HashSet<ProductWithBuyerDto>();
    }
}
