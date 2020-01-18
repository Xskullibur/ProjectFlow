using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace ProjectFlow.FileManagement
{
    public class Decryption
    {
        string IV = HostingEnvironment.MapPath("~/FileManagement/_KEYS/IV.bin");
        string KEY = HostingEnvironment.MapPath("~/FileManagement/_KEYS/KEY.bin");

        public void DecryptFileWithKey(string path, string key)
        {
            byte[] fileToByte = File.ReadAllBytes(path);
            using (var AES = new AesCryptoServiceProvider())
            {
                AES.IV = File.ReadAllBytes(IV);
                AES.Key = Encoding.UTF8.GetBytes(key); ;
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

        public void DecryptFile(string path)
        {
            byte[] fileToByte = File.ReadAllBytes(path);
            using (var AES = new AesCryptoServiceProvider())
            {
                AES.IV = File.ReadAllBytes(IV);
                AES.Key = File.ReadAllBytes(KEY);
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