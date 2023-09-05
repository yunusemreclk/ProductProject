using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductUpdated = "Ürün güncellendi";
        public static string ProductDeleted = "Ürün silindi";
        public static string ProductsListed = "Ürünler listelendi";

        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductCountOfCategoryError = "Bu kategoride ürün sayısı en fazla 10 olabilir";
        public static string ProductNameAlreadyExists = "Aynı isimli ürün eklenemez";
        public static string CategoryLimitedExceded = "Kategory limiti aşıldı";

        public static string CategoryAdded = "Category eklendi";
        public static string CategoryUpdated = "Category güncellendi";
        public static string CategoryDeleted = "Category silindi";
        public static string CategoryListed = "Category listelendi";

        public static string MaintenenceTime = "Bakım zamanı";

 



    }
}
