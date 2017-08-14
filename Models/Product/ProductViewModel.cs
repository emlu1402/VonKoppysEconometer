using System.Collections.Generic;

namespace Econometer.Models.Product
{
    public class ProductViewModel
    {
           public List<Data.Models.Product> Products = new List<Data.Models.Product>();
           public List<Data.Models.Vat> Vats = new List<Data.Models.Vat>();
    }
}