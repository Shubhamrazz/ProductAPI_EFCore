using Bussiness_Layer.Interface;
using Database_Layer.ProductModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ProductAPI_EFCore.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext productContext;
        private readonly IProductBL productBL;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(ProductContext productContext, IProductBL productBL, IWebHostEnvironment webHostEnvironment)
        {
            this.productContext = productContext;
            this.productBL = productBL;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductPostModel productModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);


                this.productBL.AddProduct(productModel, UserId);

                return this.Ok(new { success = true, message = "ProductCreated Successfully" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("[action]")]
        public IActionResult UploadedFiles(List<IFormFile> files)
        {
            if (files.Count == 0)
                return BadRequest();
            string directoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "UploadedFiles");//we can get root path of solun from web host envir
            foreach (var file in files)
            {
                string filePath = Path.Combine(directoryPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return Ok("Uploaded Successful");
        }


        //[HttpPost("AddProduct")]
        //public IActionResult AddProduct(ProductPostModel productModel)
        //{

        //    try
        //    {
        //        string FileName = UploadedFiles(productModel);
        //        var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
        //        int UserId = int.Parse(userId.Value);


        //        this.productBL.AddProduct(productModel, UserId);

        //        return this.Ok(new { success = true, message = "ProductCreated Successfully" });

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public string UploadedFiles(ProductPostModel productModel, List<IFormFile> files)
        //{
        //    string FileName = null;
        //    if (productModel.Image != null)
        //    {
        //        string directoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "UploadedFiles");//we can get root path of solun from web host envir
        //        foreach (var Image in files)
        //        {
        //            string filePath = Path.Combine(directoryPath, Image.FileName);
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                productModel.Image.CopyTo(stream);
        //            }
        //        }
        //    }
        //    return FileName;
        //}
    }
}
