using Bussiness_Layer.Interface;
using Database_Layer.ProductModel;
using Microsoft.AspNetCore.Http;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_Layer.Services
{
    public class ProductBL : IProductBL
    {
        IProductRL productRL;

        public ProductBL(IProductRL productRL)
        {
            this.productRL = productRL;
        }

        public void AddProduct(ProductPostModel productPostModel, int UserId, string Image)
        {
            try
            {
                this.productRL.AddProduct(productPostModel, UserId,Image);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void AddProduct(ProductPostModel productPostModel, int UserId, IFormFile files)
        //{
        //    try
        //    {
        //        this.productRL.AddProduct(productPostModel, UserId, files);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}

