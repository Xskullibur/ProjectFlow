using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.Hosting;

namespace ProjectFlow.FileManagement
{
    public class Encryption
    {
        string IV = HostingEnvironment.MapPath("~/FileManagement/_KEYS/IV.bin");
        string KEY = HostingEnvironment.MapPath("~/FileManagement/_KEYS/KEY.bin");

        public void EncryptFileWithKey(string path, string key)
        {
            byte[] fileToByte = File.ReadAllBytes(path);
            using (var AES = new AesCryptoServiceProvider())
            {
                AES.IV = File.ReadAllBytes(IV);
                AES.Key = Encoding.UTF8.GetBytes(key);
                AES.Mode = CipherMode.CBC;
                AES.Padding = PaddingMode.PKCS7;

                using (var memStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(memStream, AES.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(fileToByte, 0, fileToByte.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(path, memStream.ToArray());
                }
            }
        }

        public void EncryptFile(string path)
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
                    CryptoStream cryptoStream = new CryptoStream(memStream, AES.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(fileToByte, 0, fileToByte.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(path, memStream.ToArray());
                }
            }
        }

        public string GenerateKey(int iKeySize)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            aesEncryption.GenerateIV();
            string ivStr = Convert.ToBase64String(aesEncryption.IV);
            aesEncryption.GenerateKey();
            string keyStr = Convert.ToBase64String(aesEncryption.Key);
            string completeKey = ivStr + "," + keyStr;

            return Convert.ToBase64String(ASCIIEncoding.UTF8.GetBytes(completeKey));
        }
    }
}