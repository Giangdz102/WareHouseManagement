using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagement.Models;
using System.Security.Cryptography;

namespace WarehouseManagement.ViewModel
{

    class LoginViewModel
    {
        QuanLyKhoContext qlk = new QuanLyKhoContext();
        public User GetUserByEmailAndPassword(string username, string password)
        {
            User user = new User();
            try
            {
                using QuanLyKhoContext qlk = new QuanLyKhoContext();
                string PassEncode = MD5Hash(Base64Encode(password));
                user = qlk.Users.FirstOrDefault(c => c.UserName == username && c.Password == PassEncode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

    }
}
