using Bussiness_Layer.Interface;
using Database_Layer.ProductModel;
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

        public void AddProduct(ProductPostModel productPostModel,int UserId)
        {
            try
            {
                this.productRL.AddProduct(productPostModel, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

