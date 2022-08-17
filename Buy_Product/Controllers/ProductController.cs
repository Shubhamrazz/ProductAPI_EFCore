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
using Repository_Layer.Services.Entities;
using Newtonsoft.Json;

namespace Buy_Product.Controllers
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
        public IActionResult AddProduct(string Name, int Price, IFormFile files)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                string Serialization;
                ProductPostModel productModel = new ProductPostModel();
                productModel.Name = Name;
                productModel.Price = Price;
                var Image = files.FileName;
                Serialization= JsonConvert.SerializeObject(productModel.Name,(Formatting)productModel.Price);
                this.productBL.AddProduct(productModel, UserId, Image);

                //UploadFiles in Database

                string directoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "UploadedFiles");//we can get root path of solun from web host envir
                var originalFileName = " ";


                string filePath = Path.Combine(directoryPath, files.FileName);
                originalFileName = Path.GetFileName(files.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    files.CopyTo(stream);
                }


                //this.productBL.AddProduct(productModel, UserId, files);

                return this.Ok(new { success = true, message = "ProductCreated Successfully with Image", FileName = files.FileName ,Serialization});

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost("AddProduct")]
        //public IActionResult AddProduct(ProductPostModel productModel)
        //{
        //    try
        //    {

        //        var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
        //        int UserId = int.Parse(userId.Value);

        //        this.productBL.AddProduct(productModel, UserId);
        //        return this.Ok(new { success = true, message = "ProductCreated Successfully with Image" });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;


        //    }
        //}


        //[HttpPost("AddProduct")]
        //public IActionResult AddProduct(ProductPostModel productModel)
        //{
        //    try
        //    {
        //        var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
        //        int UserId = int.Parse(userId.Value);

        //        //UploadFiles in Database

        //        string directoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "UploadedFiles");//we can get root path of solun from web host envir
        //        var originalFileName = " ";


        //        string filePath = Path.Combine(directoryPath, productModel.File.FileName);
        //        originalFileName = Path.GetFileName(productModel.File.FileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //           productModel.File.CopyTo(stream);
        //        }


        //        //this.productBL.AddProduct(productModel, UserId, files);

        //        return this.Ok(new { success = true, message = "ProductCreated Successfully with Image", FileName = productModel.File.FileName });

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPost("UploadImage/{ProductId}")]
        //public IActionResult UploadedFiles(IFormFile files, int ProductId)
        //{

        //    var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
        //    int UserId = int.Parse(userId.Value);
        //    string directoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "UploadedFiles");//we can get root path of solun from web host envir
        //    var originalFileName = " ";


        //    string filePath = Path.Combine(directoryPath, files.FileName);
        //    originalFileName = Path.GetFileName(files.FileName);
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        files.CopyTo(stream);
        //    }
        //    //if (files.Count == 0)
        //    //    return BadRequest();
        //    //string directoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "UploadedFiles");//we can get root path of solun from web host envir
        //    //var originalFileName=" ";
        //    //foreach (var file in files)
        //    //{
        //    //    string filePath = Path.Combine(directoryPath, file.FileName);
        //    //     originalFileName = Path.GetFileName(file.FileName);
        //    //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    //    {
        //    //        file.CopyTo(stream);
        //    //    }
        //    //}
        //    //Product obj = new Product();
        //    var obj = productContext.Products.Where(x => x.ProductId == ProductId && x.UserId == UserId).FirstOrDefault();

        //    obj.Image = originalFileName;
        //    productContext.Products.Update(obj);
        //    productContext.SaveChanges();
        //    return Ok(new { success = true, message = "FileUpload Successfully", FileName = originalFileName });
        //}

    }
}
