using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KependudukanAPI.Libs
{
    public class lConvert
    {
        public JArray convertDynamicToJArray(List<dynamic> list)
        {
            var jsonObject = new JObject();
            dynamic data = jsonObject;
            data.Lists = new JArray() as dynamic;
            dynamic detail = new JObject();
            foreach (dynamic dr in list)
            {
                detail = new JObject();
                foreach (var pair in dr)
                {
                    detail.Add(pair.Key, pair.Value);
                }
                data.Lists.Add(detail);
            }
            return data.Lists;
        }

        public string decrypt(string str)
        {
            lDbConn ldb = new lDbConn();
            var key = ldb.ConfigKey("DecryptionKey");

            //var key = "idxpartners";
            //var key = "iD3c1$10N";
            string decrypted = DecryptString(str, key);
            return decrypted;
        }

        public string DecryptString(string cipherText, string EncryptionKey)
        {
            //string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Dispose();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
