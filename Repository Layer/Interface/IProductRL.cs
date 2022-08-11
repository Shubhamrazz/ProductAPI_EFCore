using Database_Layer.ProductModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IProductRL
    {
        public void AddProduct(ProductPostModel productPostModel,int UserId);
    }
}
