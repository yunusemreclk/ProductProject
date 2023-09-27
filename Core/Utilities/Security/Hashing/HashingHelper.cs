using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
   public class HashingHelper
    {
        //Bu class'in amaci kullanicinin verdigi sifreyi Hashleyip Saltlamaktir ya da sifreyi dogrulamaktir.
        public static void CreatePasswordHash( string password, out byte[] passwordHash,out byte[] passwordSalt)
        {
           
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));  
            }

        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                
               var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                            return false;
                    }
                   
                }
                return true;
            }
          
        }
    }
}
