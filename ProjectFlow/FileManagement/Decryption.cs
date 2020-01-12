using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProjectFlow.FileManagement
{
    public class Decryption
    {
        string IV = "WTVqdWZnTHpxTnl1";
        public void DecryptFileWithKey(string path, string key)
        {
            byte[] fileToByte = File.ReadAllBytes(path);
            using (var AES = new AesCryptoServiceProvider())
            {
                AES.IV = Encoding.UTF8.GetBytes(IV);
                AES.Key = Encoding.UTF8.GetBytes(key);
                AES.Mode = CipherMode.CBC;
                AES.Padding = PaddingMode.PKCS7;

                using (var memStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(memStream, AES.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(fileToByte, 0, fileToByte.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(path, memStream.ToArray());
                }
            }
        }
    }
}