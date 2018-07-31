using DatingApp.Common.Interfaces.IRepos;
using DatingApp.Common.Interfaces.IServices;
using DatingApp.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Common.Services
{
    public class AuthService : IAuthService
    {

        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        private readonly IAuthRepository _repos;

        public AuthService(IAuthRepository repos)
        {
            _repos = repos;


        }

        public User Login(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool RegisterUser(User user, string password)
        {
            var recordsInserted = 0;
            if (!string.IsNullOrWhiteSpace(user?.UserName))
            {
                user.Password = Encrypt(password);

                recordsInserted = _repos.SaveUser(user);
            }

            return recordsInserted > 1;
        }



        public bool UserExist(string username)
        {
            var user = _repos.GetUser(x => x.UserName == username).FirstOrDefault();
            return user != null;
        }


        private void CreateHashKey(string password)
        {
            throw new NotImplementedException();
        }


        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }


        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

    }
}
