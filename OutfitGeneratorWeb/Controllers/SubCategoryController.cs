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
    public class SubCategoryController : Controller
    {
        public async Task<ActionResult> GetSubCategories(string apiUrl,int segment)
        {
            List<WomenCategory> categoryList = new List<WomenCategory>();
            try
            {
                categoryList = await GetSubCategoryLogic(apiUrl);
                ViewBag.ApiUrl = apiUrl;
                ViewBag.Segment = segment;
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("~/Views/OutfitGenerator/SubCategory.cshtml", categoryList);
        }
        public async Task<List<WomenCategory>> GetSubCategoryLogic(string apiUrl)
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products prandom = new Products();
                List<WomenCategory> categoryList = new List<WomenCategory>();

                var url = apiUrl;
                Uri Uri = new Uri(url, UriKind.Absolute);

                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(300);

                var response = await client.GetAsync(Uri).ConfigureAwait(false);
                string result = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    items = JsonConvert.DeserializeObject<Items>(result);
                    //Get Distinct Web Category
                    if (items != null)
                    {
                        pList = items.items.GroupBy(x => x.Web_Category).Select(x => x.First()).ToList();

                        foreach (var item in pList)
                        {
                            WomenCategory category = new WomenCategory();
                            Variants vari = new Variants();
                            Images images = new Images();

                            var random = new Random();
                            int variindex = random.Next(item.Variants.Count);
                            vari = item.Variants[variindex];

                            int imgindex = random.Next(vari.Images.Count);
                            images = vari.Images[imgindex];

                            category.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
                            category.CategoryName = item.Descriptions.Where(x => x.Language == "EN").Select(x => x.Description).FirstOrDefault();
                            category.Global_Item_Id = item.Global_Item_Id;
                            category.Maintenance_Group = item.Maintenance_Group;
                            category.Id = item.Id;
                            category.Web_Category = item.Web_Category;
                            category.ProductVariantId=vari.Id;
                            category.apiUrl = apiUrl;
                            category.ProductDescription = category.CategoryName;

                            categoryList.Add(category);
                        }

                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return categoryList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> GetProductsBySubCategories(string apiUrl,string webCategory,string variId,int segment)
        {
            List<WomenCategory> objList = new List<WomenCategory>();
            try
            {
                objList = await GetProductsBySubCategoryWithUrl(apiUrl, webCategory,variId);
                ViewBag.ApiUrl = apiUrl;
                ViewBag.WebCategory = webCategory;
                ViewBag.VariantId = variId;
                ViewBag.Segment = segment;

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("~/Views/OutfitGenerator/Products.cshtml", objList);
        }
        public async Task<List<WomenCategory>> GetProductsBySubCategoryWithUrl(string apiUrl,string webCategory, string variId)
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products prandom = new Products();
                List<WomenCategory> productList = new List<WomenCategory>();

                var url = apiUrl;
                Uri Uri = new Uri(url, UriKind.Absolute);

                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(300);

                var response = await client.GetAsync(Uri).ConfigureAwait(false);
                string result = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    items = JsonConvert.DeserializeObject<Items>(result);
                    //Get Distinct Web Category
                    if (items != null)
                    {
                        pList = items.items.Where(x => x.Web_Category == webCategory).ToList();

                        foreach (var item in pList)
                        {
                            WomenCategory category = new WomenCategory();
                            Variants vari = new Variants();
                            Images images = new Images();

                            var random = new Random();

                            int variindex = random.Next(item.Variants.Count);
                            vari = item.Variants[variindex];

                            //No need a variant id input as parameter in this method
                            //Need to change  the logic because of vaiant id (that is coming as a input parameter)
                            //vari = item.Variants.Find(x=>x.Id == variId);

                            int imgindex = random.Next(vari.Images.Count);
                            images = vari.Images[imgindex];

                            category.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
                            category.CategoryName = item.Maintenance_Group;
                            category.Global_Item_Id = item.Global_Item_Id;
                            category.Maintenance_Group = item.Maintenance_Group;
                            category.Id = item.Id;
                            category.Web_Category = item.Web_Category;
                            category.Price = vari.Current_Price;
                            category.ProductVariantId = vari.Id;
                            category.apiUrl = apiUrl;
                            productList.Add(category);
                        }

                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return productList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Not Used Methods
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

        #endregion
    }
}