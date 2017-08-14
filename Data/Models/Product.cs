using System.Collections.Generic;

namespace Econometer.Data.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int VatID { get; set; }
        public virtual ICollection<Time> Times { get; set; }
        public List<Data.Models.Vat> Vats = new List<Data.Models.Vat>();
        public virtual Vat Vat { get; set; }
    }
}