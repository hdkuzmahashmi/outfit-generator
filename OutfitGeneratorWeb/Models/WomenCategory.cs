using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutfitGeneratorWeb.Models
{
    public class WomenCategory
    {
        public string Global_Item_Id { get; set; }
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public string Maintenance_Group { get; set; }
        public string Web_Category { get; set; }
        public string Customer_Group { get; set; }
        public string apiUrl { get; set; }
        public double Price { get; set; }
        public string ProductVariantId { get; set; }
        public string ProductDescription { get; set; }
    }
}