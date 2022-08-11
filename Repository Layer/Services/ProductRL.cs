using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using Repository_Layer.Services.Entities;
using System;
using Database_Layer.ProductModel;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Services
{
    public class ProductRL : IProductRL
    {
        private readonly ProductContext productContext;
        private readonly IConfiguration configuration;

        public ProductRL(ProductContext productContext, IConfiguration configuration)
        {
            this.productContext = productContext;
            this.configuration = configuration;
        }

        public void AddProduct(Database_Layer.ProductModel.ProductPostModel productPostModel,int UserId)
        {
            try
            {
                Product product = new Product();
                product.Name = productPostModel.Name;
                product.Price = productPostModel.Price;
                product.Image = productPostModel.Image;
                product.UserId = UserId;
                productContext.Products.Add(product);
                productContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

   
}



