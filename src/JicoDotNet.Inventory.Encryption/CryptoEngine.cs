using System;
using System.Security.Cryptography;
using System.Text;

namespace JicoDotNet.Inventory.Encryption
{
    public class CryptoEngine : ICryptoEngine
    {
        private readonly string _DefaultKey;
        private readonly string _Key;

        /// <summary>
        /// Create an Object for Encryption 
        /// User can pass the Key
        /// </summary>
        /// <param name="Key">16 lengths of ASCII Characters</param>
        public CryptoEngine(string Key = null)
        {
            _DefaultKey = _key;

            if (string.IsNullOrEmpty(Key))
            {
                _Key = _DefaultKey;
            }
            else if (Key.Length != 16)
            {
                throw new Exception("Key Should be 16 digits chars");
            }
            else
            {
                _Key = Key;
            }
        }

        /// <summary>
        /// It will Encrypted
        /// </summary>
        /// <param name="input">a string</param>
        /// <returns>Encrypted String</returns>
        public string Encrypt(string input)
        {
            try
            {
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider
                {
                    Key = UTF8Encoding.UTF8.GetBytes(_Key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// It will Decrypted
        /// </summary>
        /// <param name="input">Encrypted String</param>
        /// <returns>a String</returns>
        public string Decrypt(string input)
        {
            try
            {
                byte[] inputArray = Convert.FromBase64String(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider
                {
                    Key = UTF8Encoding.UTF8.GetBytes(_Key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private readonly string _key = "item@9051blob-in";
    }
}