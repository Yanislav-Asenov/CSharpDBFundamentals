using System.Collections.Generic;

namespace ProductsShop.Client.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ProductWithBuyerDto> ProductsSold { get; set; } = new HashSet<ProductWithBuyerDto>();
    }
}
