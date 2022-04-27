using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutfitGeneratorWeb.Classes
{
    public static class ApiUrlMaker
    {
        public static readonly string FemaleProductsUrl = @"https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]=";

        public static readonly string MaleProductsUrl = @"https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=MALE&filters[web_category]=";

        public static readonly string SpecificProductsUrl = @"https://api.newyorker.de/csp/products/public/product/";

        public static readonly string ImageUrl = @"https://api.newyorker.de/csp/images/image/public/";

        public static readonly string ProductDetailUrl = @"https://api.newyorker.de/csp/products/public/product/";// + id + "?country=de";


        public static string ProductImageUrl(string imageId,string res,string frame)
        {
            string url = ImageUrl+imageId+"?res="+res+"&frame="+frame+"";
            return url;
        }

        public static string ProductDetailByCountryUrl(string id)
        {
            string url = ProductDetailUrl + id + "?country=de";
            return url;
        }
    }
}