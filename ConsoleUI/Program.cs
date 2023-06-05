using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;


ProductManager productManager =new ProductManager(new EfProductDal());
foreach (var i in productManager.GetAll())
{
    Console.WriteLine(i.ProductName);
}
