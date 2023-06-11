
using System.Security.Cryptography;
using System.Text;

namespace Encrypt
{
    class Hash
    {
        public static string EncryptPassword(string mensaje){
            string hash = "black_lions_05789";
            byte[] data = UTF8Encoding.UTF8.GetBytes(mensaje);

            MD5 md5 = MD5.Create();
            TripleDES tripdes = TripleDES.Create();

            tripdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripdes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripdes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data,0,data.Length);

            return Convert.ToBase64String(result);
        }

        public static string DecryptPassword(string message){
            string hash = "black_lions_05789";
            byte[] data = Convert.FromBase64String(message);

            MD5 md5 = MD5.Create();
            TripleDES tripdes = TripleDES.Create();

            tripdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripdes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripdes.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data,0,data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }

    }
}