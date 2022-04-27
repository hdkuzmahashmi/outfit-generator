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
    public class CategoryController : Controller
    {
        public async Task<ActionResult> GetWomenCategories()
        {
            List<WomenCategory> categoryList = new List<WomenCategory>();
            try
            {
                WomenCategory category=new WomenCategory();

                category = await GetCategoryData(ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Sweatshirts,"Sweat Shirts");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Blouses,"Blouses");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.TopsTshirts,"T-Shirts");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.skirts,"Skirts");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Pants,"Pants");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Accessories,"Accessories");
                if (category != null)
                    categoryList.Add(category);
                ViewBag.Segment = 1;
                
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("~/Views/OutfitGenerator/Category.cshtml", categoryList);
        }
        public async Task<ActionResult> GetManCategories()
        {
            List<WomenCategory> categoryList = new List<WomenCategory>();
            try
            {
                WomenCategory category = new WomenCategory();

                category = await GetCategoryData(ApiUrlMaker.MaleProductsUrl + MaleCategoryConstant.TShirts,"T-Shirts");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.MaleProductsUrl + MaleCategoryConstant.Denim,"Denim");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.MaleProductsUrl + MaleCategoryConstant.SweatshirtsPullover,"Sweat Shirts/ Pullover");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.MaleProductsUrl + MaleCategoryConstant.Pants,"Pants");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.MaleProductsUrl + MaleCategoryConstant.Shirts,"Shirts");
                if (category != null)
                    categoryList.Add(category);
                category = await GetCategoryData(ApiUrlMaker.MaleProductsUrl + MaleCategoryConstant.Accessories,"Acessories");
                if (category != null)
                    categoryList.Add(category);


                ViewBag.Segment = 2;
                
            }
            catch (Exception ex)
            {

                TempData["error"] = ex.Message;
            }
            return View("~/Views/OutfitGenerator/Category.cshtml", categoryList);
        }
        public async Task<WomenCategory> GetCategoryData(string apiUrl,string categoryName)
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products product = new Products();
                WomenCategory category = new WomenCategory();
                Variants vari = new Variants();
                Images images = new Images();

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
                        var random = new Random();
                        int index = random.Next(pList.Count);
                        product = pList[index];

                        int variindex = random.Next(product.Variants.Count);
                        vari = product.Variants[variindex];

                        int imgindex = random.Next(vari.Images.Count);
                        images = vari.Images[imgindex];

                        category.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
                        category.CategoryName = categoryName;
                        category.Global_Item_Id = product.Global_Item_Id;
                        category.Maintenance_Group = product.Maintenance_Group;
                        category.Id = product.Id;
                        category.Web_Category = product.Web_Category;
                        category.apiUrl = apiUrl;
                        category.ProductDescription = product.Descriptions.Where(x => x.Language == "EN").Select(x => x.Description).FirstOrDefault();
                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return category;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Old Code
        //public async Task<ActionResult> GetWomenCategories()
        //{
        //    try
        //    {
        //        List<WomenCategory> categoryList = new List<WomenCategory>();

        //        Products sweatShirt = new Products();
        //        sweatShirt = await GetWomenSweatshirts();
        //        Products blouses = new Products();
        //        blouses = await GetWomenBlouses();
        //        Products topstshirts = new Products();
        //        topstshirts = await GetWomenTopsTshirts();
        //        Products pants = new Products();
        //        pants = await GetWomenPants();
        //        Products skirts = new Products();
        //        skirts = await GetWomenskirts();
        //        Products accessories = new Products();
        //        accessories = await GetWomenAccessories();

        //        if (sweatShirt != null)
        //        {
        //            WomenCategory category = new WomenCategory();
        //            Variants vari = new Variants();
        //            Images images = new Images();

        //            var random = new Random();

        //            int variindex = random.Next(sweatShirt.Variants.Count);
        //            vari = sweatShirt.Variants[variindex];

        //            int imgindex = random.Next(vari.Images.Count);
        //            images = vari.Images[imgindex];

        //            category.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
        //            category.CategoryName = "Sweat Shirts";
        //            category.Global_Item_Id = sweatShirt.Global_Item_Id;
        //            category.Maintenance_Group = sweatShirt.Maintenance_Group;
        //            category.Id = sweatShirt.Id;
        //            category.Web_Category = sweatShirt.Web_Category;
        //            category.apiUrl = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Sweatshirts;
        //            categoryList.Add(category);
        //        }

        //        if (blouses != null)
        //        {
        //            WomenCategory category = new WomenCategory();
        //            Variants vari = new Variants();
        //            Images images = new Images();
        //            var random = new Random();

        //            int variindex = random.Next(blouses.Variants.Count);
        //            vari = blouses.Variants[variindex];

        //            int imgindex = random.Next(vari.Images.Count);
        //            images = vari.Images[imgindex];

        //            category.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
        //            category.CategoryName = "Blouses";
        //            category.Global_Item_Id = blouses.Global_Item_Id;
        //            category.Maintenance_Group = blouses.Maintenance_Group;
        //            category.Id = blouses.Id;
        //            category.Web_Category = blouses.Web_Category;
        //            category.apiUrl = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Blouses;
        //            categoryList.Add(category);
        //        }
        //        if (topstshirts != null)
        //        {
        //            WomenCategory category = new WomenCategory();
        //            Variants vari = new Variants();
        //            Images images = new Images();
        //            var random = new Random();

        //            int variindex = random.Next(topstshirts.Variants.Count);
        //            vari = topstshirts.Variants[variindex];

        //            int imgindex = random.Next(vari.Images.Count);
        //            images = vari.Images[imgindex];


        //            category.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
        //            category.CategoryName = "Topst Shirts";
        //            category.Global_Item_Id = topstshirts.Global_Item_Id;
        //            category.Maintenance_Group = topstshirts.Maintenance_Group;
        //            category.Id = topstshirts.Id;
        //            category.Web_Category = topstshirts.Web_Category;
        //            category.apiUrl = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.TopsTshirts;
        //            categoryList.Add(category);
        //        }
        //        if (pants != null)
        //        {
        //            WomenCategory category = new WomenCategory();
        //            Variants vari = new Variants();
        //            Images images = new Images();
        //            var random = new Random();

        //            int variindex = random.Next(pants.Variants.Count);
        //            vari = pants.Variants[variindex];

        //            int imgindex = random.Next(vari.Images.Count);
        //            images = vari.Images[imgindex];


        //            category.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
        //            category.CategoryName = "Pants";
        //            category.Global_Item_Id = pants.Global_Item_Id;
        //            category.Maintenance_Group = pants.Maintenance_Group;
        //            category.Id = pants.Id;
        //            category.Web_Category = pants.Web_Category;
        //            category.apiUrl = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Pants;
        //            categoryList.Add(category);
        //        }
        //        if (skirts != null)
        //        {
        //            WomenCategory category = new WomenCategory();
        //            Variants vari = new Variants();
        //            Images images = new Images();
        //            var random = new Random();

        //            int variindex = random.Next(skirts.Variants.Count);
        //            vari = skirts.Variants[variindex];

        //            int imgindex = random.Next(vari.Images.Count);
        //            images = vari.Images[imgindex];



        //            category.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
        //            category.CategoryName = "Skirts";
        //            category.Global_Item_Id = skirts.Global_Item_Id;
        //            category.Maintenance_Group = skirts.Maintenance_Group;
        //            category.Id = skirts.Id;
        //            category.Web_Category = skirts.Web_Category;
        //            category.apiUrl = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.skirts;
        //            categoryList.Add(category);
        //        }
        //        if (accessories != null)
        //        {
        //            WomenCategory category = new WomenCategory();
        //            Variants vari = new Variants();
        //            Images images = new Images();
        //            var random = new Random();

        //            int variindex = random.Next(accessories.Variants.Count);
        //            vari = accessories.Variants[variindex];

        //            int imgindex = random.Next(vari.Images.Count);
        //            images = vari.Images[imgindex];

        //            category.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
        //            category.CategoryName = "Accessories";
        //            category.Global_Item_Id = accessories.Global_Item_Id;
        //            category.Maintenance_Group = accessories.Maintenance_Group;
        //            category.Id = accessories.Id;
        //            category.Web_Category = accessories.Web_Category;
        //            category.apiUrl = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Accessories;
        //            categoryList.Add(category);
        //        }


        //        return View("~/Views/OutfitGenerator/Category.cshtml", categoryList);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public async Task<Products> GetWomenAccessories()
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products prandom = new Products();
                

                var url = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Accessories;
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
                        var random = new Random();
                        int index = random.Next(pList.Count);
                        prandom = pList[index];
                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return prandom;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Products> GetWomenskirts()
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products prandom = new Products();


                var url = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.skirts;
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
                        var random = new Random();
                        int index = random.Next(pList.Count);
                        prandom = pList[index];
                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return prandom;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Products> GetWomenPants()
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products prandom = new Products();


                var url = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Pants;
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
                        var random = new Random();
                        int index = random.Next(pList.Count);
                        prandom = pList[index];
                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return prandom;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Products> GetWomenSweatshirts()
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products prandom = new Products();


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
                    //Get Distinct Web Category
                    if (items != null)
                    {
                        pList = items.items.GroupBy(x => x.Web_Category).Select(x => x.First()).ToList();
                        var random = new Random();
                        int index = random.Next(pList.Count);
                        prandom = pList[index];
                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return prandom;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Products> GetWomenBlouses()
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products prandom = new Products();


                var url = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.Blouses;
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
                        var random = new Random();
                        int index = random.Next(pList.Count);
                        prandom = pList[index];
                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return prandom;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Products> GetWomenTopsTshirts()
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products prandom = new Products();


                var url = ApiUrlMaker.FemaleProductsUrl + FemaleCategoryConstant.TopsTshirts;
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
                        var random = new Random();
                        int index = random.Next(pList.Count);
                        prandom = pList[index];
                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return prandom;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> GetDamenCategory()
        {
            Items items = new Items();
            List<Products> categoryDamen = new List<Products>();
            try
            {

                //Accessoires
                //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]=Accessoires,WCA01156,WCA01159,WCA01155,WCA01152,WCA01158,WCA01153,WCA01157,WCA01154";
                //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]=Blouses,WCA00122,WCA00121";
                //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE&filters[web_category]="+FemalCategoryConstant.Sweatshirts;
                //var url = "https://api.newyorker.de/csp/products/public/query?filters[country]=de&filters[gender]=FEMALE"; //Get data by maintanceGroup

                
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

                    //Get Distinct Web Category
                    if (items != null)
                    {
                         categoryDamen = items.items.GroupBy(x => x.Web_Category).Select(x => x.First()).ToList();
                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View("~/Views/OutfitGenerator/Product.cshtml", categoryDamen);
        }
        public async Task<Products> GetProduct(string id)
        {

            try
            {
                Products product = new Products();
                var url = "https://api.newyorker.de/csp/products/public/product/" + id + "?country=de";
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
        #endregion 
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
    }
}