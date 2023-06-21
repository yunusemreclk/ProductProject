using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

//ProductTest();

static void ProductTest()
{
    ProductManager productManager = new ProductManager(new EfProductDal());

    var result = productManager.GetProductDetails();
    if (result.Success == true)
    {
        foreach (var i in result.Data)
        {
            Console.WriteLine(i.ProductName + "--" + i.CategoryName);
        }
    }
    else
    {
        Console.WriteLine(result.Message);
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

GetAllTest();

static void GetAllTest()
{
    ProductManager productManager = new ProductManager(new EfProductDal());

    var result = productManager.GetAll();
    if (result.Success == true)
    {
        foreach (var i in result.Data)
        {
            Console.WriteLine(i.ProductId + "--" + i.ProductName + "--" + i.UnitPrice + "--" + i.UnitsInStock);
        }
    }
    else
    {
        Console.WriteLine(result.Message);
    }
}