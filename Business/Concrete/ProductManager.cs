using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
   public class ProductManager : IProductService
    {
       private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            if(product.ProductName.Length<2)
            {
               return new ErrorResult(Messages.ProductNameInvalid);
            }
          _productDal.Add(product);
            return new Result(true,"Ürün Eklendi");
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new Result(true, "Ürün Eklendi");
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==20)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenenceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }
        public IDataResult<Product> GetById(int id)
        {
            return new DataResult<Product>(_productDal.Get(x=>x.ProductId==id), true, "Ürünler listelendi");
        }
        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            return new DataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId == id),true,"Ürünler Listelendi");
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
         return new DataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min&&p.UnitPrice<=max), true, "Ürünler Listelendi");
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new DataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),true);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new Result(true, "Ürün Eklendi");
        }
    }
}
