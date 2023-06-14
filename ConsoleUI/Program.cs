using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

ProductTest();

static void ProductTest()
{
    ProductManager productManager = new ProductManager(new EfProductDal());
    foreach (var i in productManager.GetProductDetails())
    {
        Console.WriteLine(i.ProductName + "--" + i.CategoryName);
    }
}
//CategoryTest();

static void CategoryTest()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
    foreach (var item in categoryManager.GetAll())
    {
        Console.WriteLine(item.CategoryName);
    }
}
