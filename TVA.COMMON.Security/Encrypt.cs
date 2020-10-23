using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace TVA.COMMON.Security
{
    public static class Encrypt
    {
        private const string DEFAULT_KEY = "TVA";

        public static string HashString(string str)
        {
            SHA256 sha256 = SHA256.Create();

            byte[] data = sha256.ComputeHash(Encoding.Unicode.GetBytes(str));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static string Encode(string toEncrypt, bool useHashing, string keyEncryption)
        {
            keyEncryption = string.IsNullOrEmpty(keyEncryption) ? DEFAULT_KEY : keyEncryption;

            byte[] buffer;
            if (useHashing)
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(keyEncryption));
                provider.Clear();
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(keyEncryption);
            }

            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider();
            provider2.Key = buffer;
            provider2.Mode = CipherMode.ECB;
            provider2.Padding = PaddingMode.PKCS7;

            byte[] buffer2 = Encoding.UTF8.GetBytes(toEncrypt);
            byte[] buffer3 = provider2.CreateEncryptor().TransformFinalBlock(buffer2, 0, (int)buffer2.Length);
            provider2.Clear();

            StringBuilder builder = new StringBuilder();
            int num = 0;

            while (num < ((int)buffer3.Length))
            {
                builder.Append((buffer3[num]).ToString("x2"));
                num += 1;
            }

            return builder.ToString();
        }

        public static string Decode(string cipherString, bool useHashing, string keyEncryption)
        {
            keyEncryption = string.IsNullOrEmpty(keyEncryption) ? DEFAULT_KEY : keyEncryption;

            byte[] buffer;
            if (useHashing)
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(keyEncryption));
                provider.Clear();
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(keyEncryption);
            }

            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider();
            provider2.Key = buffer;
            provider2.Mode = CipherMode.ECB;
            provider2.Padding = PaddingMode.PKCS7;

            byte[] buffer2 = ConvertHexStringToByteArray(cipherString);
            byte[] buffer3 = provider2.CreateDecryptor().TransformFinalBlock(buffer2, 0, (int)buffer2.Length);
            provider2.Clear();

            return Encoding.UTF8.GetString(buffer3);
        }

        private static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if ((hexString.Length % 2) != 0)
            {
                return new byte[0];
            }

            byte[] buffer = new byte[hexString.Length / 2];
            int num = 0;

            string str;
            while (num < ((int)buffer.Length))
            {
                str = hexString.Substring(num * 2, 2);
                buffer[num] = byte.Parse(str, (NumberStyles)0x203, CultureInfo.InvariantCulture);
                num += 1;
            }

            return buffer;
        }
    }
}
