using Database_Layer.ProductModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IProductRL
    {
        public void AddProduct(ProductPostModel productPostModel, int UserId,string Image);

       // public void AddProduct(ProductPostModel productPostModel,int UserId, IFormFile files);
    }
}
