using CatalogApi.Interfaces.Manager;
using CatalogApi.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace CatalogApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        IProductManager _productManager;
        public CatalogController( IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 20)]//it use ffor server handaling stop frequent request 
        public IActionResult GetProduct()
        {
            try
            {
                var products = _productManager.GetAll();
                return CustomResult("data loded successflly ", products);//CustomResult By default 200
                // return CustomResult (products,HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message,HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.Created)]
        public IActionResult CreateProduct([FromBody]Product product)
        {
            try
            {
                product.Id=ObjectId.GenerateNewId().ToString();
                bool isSaved=_productManager.Add(product);
                if(isSaved)
                {
                    return CustomResult("Product has been save successfully",product);
                }
                return CustomResult("Product save faild",product,HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]

        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Id))
                {
                    return CustomResult("Data not found",HttpStatusCode.BadRequest);
                }
                
                bool isUpdate = _productManager.Update(product.Id,product);
                if (isUpdate)
                {
                    return CustomResult("Product has been Update successfully", product);
                }
                return CustomResult("Product modified faild", product, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeleteProduct(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return CustomResult("Data not found", HttpStatusCode.BadRequest);
                }

                bool isDelete = _productManager.Delete(id);
                if (isDelete)
                {
                    return CustomResult("Product has been Delete successfully",HttpStatusCode.OK);
                }
                return CustomResult("Product delete faild", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
 