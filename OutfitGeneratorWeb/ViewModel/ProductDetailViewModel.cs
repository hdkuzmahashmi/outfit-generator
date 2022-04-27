using OutfitGeneratorWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutfitGeneratorWeb.ViewModel
{
    public class ProductDetailViewModel
    {
        public Products Product { get; set; }
        public Variants Variant { get; set; }
        public string VariantId { get; set; }
        public List<WomenCategory> Outfit { get; set; }
    }
}