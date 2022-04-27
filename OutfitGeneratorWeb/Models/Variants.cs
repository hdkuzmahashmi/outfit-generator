using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutfitGeneratorWeb.Models
{
    public class Variants
    {
        public string Id { get; set; }
        public string Product_Id { get; set; }
        public DateTime Publish_Date { get; set; }
        public bool New_In { get; set; }
        public bool Coming_soon { get; set; }
        public bool Sale { get; set; }
        public string Color_Name { get; set; }
        public string Pantone_Color_Name { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public string Color_Group { get; set; }
        public string Basic_Color { get; set; }
        public string Currency { get; set; }
        public double Original_Price { get; set; }
        public double Current_Price { get; set; }
        public bool Red_Price_Change { get; set; }
        public List<Sizes> Sizes { get; set; }
        public  List<Images> Images { get; set; }   
    }
}