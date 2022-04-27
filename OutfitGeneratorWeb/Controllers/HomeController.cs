using Newtonsoft.Json;
using OutfitGeneratorWeb.Classes;
using OutfitGeneratorWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OutfitGeneratorWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/OutfitGenerator/LandingView.cshtml");
        }

        //public async Task<ActionResult> Index()
        //{
        //    //return View("~/Views/OutfitGenerator/Main.cshtml");

        //    Items items = new Items();
        //    List<Products> categoryDamen = new List<Products>();
        //    try
        //    {

        //        //Accessoires
        //        //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]=Accessoires,WCA01156,WCA01159,WCA01155,WCA01152,WCA01158,WCA01153,WCA01157,WCA01154";
        //        //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]=Blouses,WCA00122,WCA00121";
        //        //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]="+FemalCategoryConstant.Sweatshirts;


        //        var url = ApiUrlMaker.FemaleProductsUrl + FemalCategoryConstant.Sweatshirts;
        //        Uri Uri = new Uri(url, UriKind.Absolute);

        //        HttpClient client = new HttpClient();
        //        client.Timeout = TimeSpan.FromSeconds(300);

        //        var response = await client.GetAsync(Uri).ConfigureAwait(false);
        //        string result = string.Empty;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            result = response.Content.ReadAsStringAsync().Result;
        //            items = JsonConvert.DeserializeObject<Items>(result);

        //            //Get Distinct Web Category
        //            if (items != null)
        //            {
        //                categoryDamen = items.items.GroupBy(x => x.Web_Category).Select(x => x.First()).ToList();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return View("~/Views/OutfitGenerator/Product.cshtml", categoryDamen);
        //}

        public async Task<ActionResult> GetItem()
        {
            Items items = new Items();

            try
            {

                //Accessoires
                //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]=Accessoires,WCA01156,WCA01159,WCA01155,WCA01152,WCA01158,WCA01153,WCA01157,WCA01154";
                //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]=Blouses,WCA00122,WCA00121";
                //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]="+FemalCategoryConstant.Sweatshirts;
                

                var url = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Sweatshirts;
                Uri Uri = new Uri(url, UriKind.Absolute);

                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(300);

                var response = await client.GetAsync(Uri).ConfigureAwait(false);
                string result = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    items = JsonConvert.DeserializeObject<Items>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("~/Views/OutfitGenerator/Product.cshtml",items);
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<Products> GetProduct(string id)
        {

            try
            {
                Products product = new Products(); 
                var url = "https://api.newyorker.de/csp/products/public/product/"+id+"?country=de";
                Uri Uri = new Uri(url, UriKind.Absolute);

                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(300);

                var response = await client.GetAsync(Uri).ConfigureAwait(false);
                string result = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                     product = JsonConvert.DeserializeObject<Products>(result);
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return product;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}