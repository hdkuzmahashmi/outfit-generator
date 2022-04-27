using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutfitGeneratorWeb.Models
{
    public class Products
    {
        public string Global_Item_Id { get; set; }
        public string Id { get; set; }
        public string Country { get; set; }
        public string Maintenance_Group { get; set; }
        public string Web_Category_Id { get; set; }
        public string Web_Category { get; set; }
        public string Brand { get; set; }
        public int Sales_Unit { get; set; }
        public string Customer_Group { get; set; }
        public List<Variants> Variants { get; set; }
        public List<Descriptions> Descriptions { get; set; }
    }
}