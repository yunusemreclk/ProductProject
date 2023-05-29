using Business.Concrete;
using DataAccess.Concrete.InMemory;


ProductManager productManager =new ProductManager(new InMemoryProductDal());
foreach (var i in productManager.GetAll())
{
    Console.WriteLine(i.ProductName);
}
