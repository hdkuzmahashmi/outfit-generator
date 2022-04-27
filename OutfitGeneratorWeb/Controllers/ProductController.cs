using Newtonsoft.Json;
using OutfitGeneratorWeb.Classes;
using OutfitGeneratorWeb.Models;
using OutfitGeneratorWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OutfitGeneratorWeb.Controllers
{
    public class ProductController : Controller
    {
        public async Task<ActionResult> GetProductDetail(string productId,string variantId,string apiUrl,string webCategory,int segment)
        {
            ProductDetailViewModel pdViewModel = new ProductDetailViewModel();
            try
            {
                Products objProduct = new Products();
                objProduct = await GetProductDetailByCategory(productId, variantId,apiUrl,webCategory);
                pdViewModel.Product = objProduct;
                pdViewModel.Variant= objProduct.Variants.Where(x=>x.Id== variantId).FirstOrDefault();
                pdViewModel.VariantId= variantId;
                pdViewModel.Outfit= await CreateOutfit(apiUrl,webCategory, objProduct, variantId);
                // Managing Back button code
                ViewBag.ApiUrl = apiUrl;
                ViewBag.WebCategory = webCategory;
                ViewBag.VariantId = variantId;
                ViewBag.Segment = segment;
                
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("~/Views/OutfitGenerator/ProductDetail.cshtml", pdViewModel);
        }
        public async Task<Products> GetProductDetailByCategory(string productId, string variantId,string apiUrl,string webCategory)
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                Products prandom = new Products();
                List<WomenCategory> productList = new List<WomenCategory>();
                Products product = new Products();
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
                        product=pList.Find(x=> x.Id == productId);   
                        // need to write code here for selecting input variant color OR show all first picture of all color on indiviual product view 
                    }
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
        public async Task<List<WomenCategory>> CreateOutfit(string apiUrl, string webCategory, Products selectedProduct, string selectedVariantId)
        {
            List<WomenCategory> womenOutfitList = new List<WomenCategory>();
            WomenCategory womenOutfit = new WomenCategory();
            #region Female Outfit
            if (webCategory == FemaleSubCategoryConstant.SweatShirtsCode_SweatShirtJacket)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.TopsTshirtsUrl_TopWithVneck, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.TopsTshirtsUrl_TShirtWithPrint, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.PantsUrl_Jogger, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_RingSet, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == FemaleSubCategoryConstant.SweatShirtsCode_SweatShirt)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.PantsUrl_Jogger, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.SkirtUrl_WovenMiniSkirt, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_RingSet, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Hat, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == FemaleSubCategoryConstant.BlousesCode_ViscoseBlouse)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.PantsUrl_LeggingsWithPrint, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.SkirtUrl_WovenMiniSkirt, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_RingSet, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Hat, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_SmartphonePendant, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == FemaleSubCategoryConstant.BlousesCode_JerseyBlouse)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.PantsUrl_LeggingsWithPrint, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.PantsUrl_ClothTrousers, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.SkirtUrl_FlaredMidiSkirt, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_RingSet, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Hat, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_SmartphonePendant, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == FemaleSubCategoryConstant.TopsTshirtsCode_TopWithVneck || webCategory == FemaleSubCategoryConstant.TopsTshirtsCode_LongSleeveWithRoundneck ||
                     webCategory == FemaleSubCategoryConstant.TopsTshirtsCode_TShirtWithPrint)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.PantsUrl_LeggingsWithPrint, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.PantsUrl_ClothTrousers, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.SkirtUrl_FlaredMidiSkirt, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_RingSet, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_SetOfSneakers, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_SmartphonePendant, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == FemaleSubCategoryConstant.PantsCode_ClothTrousers || webCategory == FemaleSubCategoryConstant.PantsCode_Jogger
                    || webCategory == FemaleSubCategoryConstant.PantsCode_LeggingsWithPrint)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.BlousesUrl_JerseyBlouse, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.TopsTshirtsUrl_LongSleeveWithRoundneck, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_RingSet, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_SetOfSneakers, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_SmartphonePendant, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == FemaleSubCategoryConstant.SkirtCode_WovenMiniSkirt || webCategory == FemaleSubCategoryConstant.SkirtCode_FlaredMidiSkirt)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.BlousesUrl_ViscoseBlouse, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.TopsTshirtsUrl_LongSleeveWithRoundneck, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_RingSet, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_SetOfSneakers, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.AccessoriesUrl_SmartphonePendant, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == FemaleSubCategoryConstant.AccessoriesCode_Belts || webCategory == FemaleSubCategoryConstant.AccessoriesCode_Hat ||
                    webCategory == FemaleSubCategoryConstant.AccessoriesCode_Sunglasses || webCategory == FemaleSubCategoryConstant.AccessoriesCode_SmartphonePendant ||
                    webCategory == FemaleSubCategoryConstant.AccessoriesCode_Scarf || webCategory == FemaleSubCategoryConstant.AccessoriesCode_SetOfSneakers ||
                    webCategory == FemaleSubCategoryConstant.AccessoriesCode_RingSet)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.BlousesUrl_ViscoseBlouse, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.TopsTshirtsUrl_LongSleeveWithRoundneck, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.SkirtUrl_WovenMiniSkirt, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.PantsUrl_Jogger, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.PantsUrl_LeggingsWithPrint, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.FemaleProductsUrl, FemaleSubCategoryConstant.SweatShirtsUrl_SweatShirtJacket, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            #endregion
            #region Male Outfit
            else if (webCategory == MaleSubCategoryConstant.ShirtsCode_CottonShirt)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.PantsUrl_SlimFitChino, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.DenimUrl_SkinnyDenim, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Cap, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.SweatshirtsPulloverUrl_VestWithVneck, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == MaleSubCategoryConstant.TShirtCode_LongSleevedShirtWithRoundNeck || webCategory == MaleSubCategoryConstant.TShirtCode_PoloShirtWithStandupCollar ||
                     webCategory == MaleSubCategoryConstant.TShirtCode_TShirtWithRoundNeck)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.PantsUrl_HoseWitCuffs, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.DenimUrl_JoggDenim, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Cap, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.SweatshirtsPulloverUrl_HoodedJacket, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == MaleSubCategoryConstant.DenimCode_JoggDenim || webCategory == MaleSubCategoryConstant.DenimCode_SkinnyDenim ||
                     webCategory == MaleSubCategoryConstant.DenimCode_SlimStraightDenim || webCategory == MaleSubCategoryConstant.DenimCode_TaperedDenim)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.TShirtUrl_LongSleevedShirtWithRoundNeck, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.TShirtUrl_PoloShirtWithStandupCollar, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.ShirtsUrl_CottonShirt, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Cap, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.SweatshirtsPulloverUrl_CardiganWithzipper, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Necklace, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == MaleSubCategoryConstant.SweatshirtsPulloverCode_CardiganWithzipper || webCategory == MaleSubCategoryConstant.SweatshirtsPulloverCode_HoodedJacket ||
                     webCategory == MaleSubCategoryConstant.SweatshirtsPulloverCode_VestWithVneck || webCategory == MaleSubCategoryConstant.SweatshirtsPulloverCode_VneckSweater)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.TShirtUrl_LongSleevedShirtWithRoundNeck, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.ShirtsUrl_CottonShirt, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Cap, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Backpack, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == MaleSubCategoryConstant.PantsCode_BasicPants || webCategory == MaleSubCategoryConstant.PantsCode_HoseWitCuffs ||
                     webCategory == MaleSubCategoryConstant.PantsCode_SlimFitChino)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.TShirtUrl_TShirtWithRoundNeck, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.ShirtsUrl_CottonShirt, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.SweatshirtsPulloverUrl_VneckSweater, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Sunglasses, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Cap, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.AccessoriesUrl_Backpack, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            else if (webCategory == MaleSubCategoryConstant.AccessoriesCode_Backpack || webCategory == MaleSubCategoryConstant.AccessoriesCode_Bandana ||
                     webCategory == MaleSubCategoryConstant.AccessoriesCode_Cap || webCategory == MaleSubCategoryConstant.AccessoriesCode_Necklace ||
                     webCategory == MaleSubCategoryConstant.AccessoriesCode_Sunglasses)
            {
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.TShirtUrl_TShirtWithRoundNeck, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.ShirtsUrl_CottonShirt, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.SweatshirtsPulloverUrl_VneckSweater, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.DenimUrl_TaperedDenim, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
                womenOutfit = await GetApiData(ApiUrlMaker.MaleProductsUrl, MaleSubCategoryConstant.PantsUrl_HoseWitCuffs, selectedProduct, selectedVariantId);
                if (womenOutfit != null)
                    womenOutfitList.Add(womenOutfit);
            }
            #endregion
            return womenOutfitList;
        }
        public async Task<WomenCategory> GetApiData(string apiSegment,string apiUrl, Products selectedProduct,string selectedVariantId)
        {
            try
            {
                Items items = new Items();
                List<Products> pList = new List<Products>();
                List<WomenCategory> womenOutfitList = new List<WomenCategory>();
                Products outfit = new Products();
                
                WomenCategory womenOutfit = new WomenCategory();
                var url = apiSegment + apiUrl;
                Uri Uri = new Uri(url, UriKind.Absolute);
                int counter = 0;
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(300);

                var response = await client.GetAsync(Uri).ConfigureAwait(false);
                string result = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    items = JsonConvert.DeserializeObject<Items>(result);
                    if (items != null)
                    {
                        var selectedProVariant = selectedProduct.Variants.Where(x => x.Id == selectedVariantId).FirstOrDefault();

                        #region test Code
                        var randomProduct = items.items.Where(x => x.Variants.Any(y => y.Color_Group == selectedProVariant.Color_Group)).FirstOrDefault();
                        if (randomProduct != null)
                        {
                            Variants rpvari = new Variants();
                            Images images = new Images();

                            var random = new Random();
                            int variindex = random.Next(randomProduct.Variants.Count);
                            rpvari = randomProduct.Variants[variindex];

                            int imgindex = random.Next(rpvari.Images.Count);
                            images = rpvari.Images[imgindex];

                            womenOutfit.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
                            womenOutfit.CategoryName = randomProduct.Maintenance_Group;
                            womenOutfit.Global_Item_Id = randomProduct.Global_Item_Id;
                            womenOutfit.Maintenance_Group = randomProduct.Maintenance_Group;
                            womenOutfit.Id = randomProduct.Id;
                            womenOutfit.Web_Category = randomProduct.Web_Category;
                            womenOutfit.apiUrl = url;
                            womenOutfit.ProductVariantId = rpvari.Id;
                            womenOutfit.Price = rpvari.Original_Price;
                            womenOutfit.ProductDescription = randomProduct.Descriptions.Where(x => x.Language == "EN").Select(x => x.Description).FirstOrDefault();
                        }
                        #endregion
                        else
                        {
                            foreach (var item in items.items)
                            {
                                foreach (var vari in item.Variants)
                                {
                                    if (selectedProVariant.Color_Group == vari.Color_Group)
                                    {
                                        Images images = new Images();
                                        var random = new Random();

                                        int imgindex = random.Next(vari.Images.Count);
                                        images = vari.Images[imgindex];

                                        womenOutfit.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
                                        womenOutfit.CategoryName = item.Maintenance_Group;
                                        womenOutfit.Global_Item_Id = item.Global_Item_Id;
                                        womenOutfit.Maintenance_Group = item.Maintenance_Group;
                                        womenOutfit.Id = item.Id;
                                        womenOutfit.Web_Category = item.Web_Category;
                                        womenOutfit.apiUrl = url;
                                        womenOutfit.ProductVariantId = vari.Id;
                                        womenOutfit.Price = vari.Original_Price;
                                        womenOutfit.ProductDescription = item.Descriptions.Where(x => x.Language == "EN").Select(x => x.Description).FirstOrDefault();

                                        counter = counter + 1;
                                        break;
                                    }
                                    else
                                    {
                                        Variants pvari = new Variants();
                                        Images images = new Images();

                                        var random = new Random();
                                        int variindex = random.Next(item.Variants.Count);
                                        pvari = item.Variants[variindex];

                                        int imgindex = random.Next(pvari.Images.Count);
                                        images = pvari.Images[imgindex];

                                        womenOutfit.ImageUrl = ApiUrlMaker.ProductImageUrl(images.key, "high", "1_1");
                                        womenOutfit.CategoryName = item.Maintenance_Group;
                                        womenOutfit.Global_Item_Id = item.Global_Item_Id;
                                        womenOutfit.Maintenance_Group = item.Maintenance_Group;
                                        womenOutfit.Id = item.Id;
                                        womenOutfit.Web_Category = item.Web_Category;
                                        womenOutfit.apiUrl = url;
                                        womenOutfit.ProductVariantId = pvari.Id;
                                        womenOutfit.Price = vari.Original_Price;
                                        womenOutfit.ProductDescription = item.Descriptions.Where(x => x.Language == "EN").Select(x => x.Description).FirstOrDefault();

                                        counter = counter + 1;
                                        break;

                                    }
                                }
                                if (counter > 0)
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception(response.IsSuccessStatusCode + " " + response.RequestMessage.ToString() + " " + response.StatusCode);
                }

                return womenOutfit;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Not Used Methods
        public async Task<Products> GetProductDetailById(string id, string variantId)
        {

            try
            {
            //var url = "https://api.newyorker.de/csp/products/public/product/" + id + "?country=de";
                      // https://api.newyorker.de/csp/products/public/product/02.02.120.0963?country=de
                Products product = new Products();
                var url = ApiUrlMaker.ProductDetailByCountryUrl(id);
                Uri Uri = new Uri(url, UriKind.Absolute);

                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(300);

                var response = await client.GetAsync(Uri).ConfigureAwait(false);
                string result = string.Empty;
                if (response.IsSuccessStatusCode)//some of Product Id's throws Not found exception
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Products>(result);

                    ViewBag.ProductvariantId = variantId;
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