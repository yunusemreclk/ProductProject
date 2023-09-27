using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        private readonly ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(ProductValidator))]
        [SecuredOperation("product.Add,admin")]
        public IResult Add(Product product)
        {

            //Yöntem 1
            //var context =new  ValidationContext<Product>(product);
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}

            //Yöntem 2
            //ValidationTool.Validate(new ProductValidator(), product);
            IResult result=BusinessRules.Run(
                CheckIfProductCountOfCategoryCorrect(product.CategoryId), 
                CheckIfProductNameExists(product.ProductName), 
                CheckIfCategoryLimitExceded());

            if (result!=null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }
       

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductAdded);
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
            return new SuccessResult(Messages.ProductUpdated);
        }
        
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count();
            if (result>=15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            { 
                return new ErrorResult(Messages.ProductNameAlreadyExists);
               
            } 
           return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitedExceded);
            }
            return new SuccessResult();
        }
        
    }
}
