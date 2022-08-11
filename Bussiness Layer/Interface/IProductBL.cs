using Database_Layer.ProductModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_Layer.Interface
{
    public interface IProductBL
    {
        public void AddProduct(ProductPostModel productPostModel,int UserId);
    }
}
