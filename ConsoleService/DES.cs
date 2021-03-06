using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MallHost
{
    /// <summary>
    /// DES 加密
    /// </summary>
    public class DES
    {
        #region 构造方法
        /// <summary>
        /// 静态构造方法
        /// </summary>
        static DES()
        {
            KeyValue = RandomKey();
        }
        #endregion

        #region 私有变量
        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static byte[] desKey = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        #endregion

        #region 公有变量
        /// <summary>
        /// 默认加密密钥
        /// </summary>
        public static string ConstKey = "22331144";

        /// <summary>
        /// 关键字密钥
        /// </summary>
        public static string KeyValue { get; set; }

        #endregion

        /// <summary>
        /// 随机密钥
        /// </summary>
        /// <returns>返回随机密钥</returns>
        public static string RandomKey()
        {
            var  random = new Random();
            var temp = string.Empty;

            for (int i = 0; i < 8; i++)
            {
                temp += random.Next(9).ToString();
            }

            return temp;
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = desKey;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var  dCSP = new DESCryptoServiceProvider();
                var  mStream = new MemoryStream();
                var  cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();

                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = desKey;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// DES加密数据文件
        /// </summary>
        /// <param name="inName">待加密的数据文件</param>
        /// <param name="outName">待输出的数据文件</param>
        /// <param name="encryptKey">The encrypt key.</param>
        /// <returns>
        /// 加密成功返回true，失败返false
        /// </returns>
        public static bool EncryptData(String inName, String outName, string encryptKey)
        {
            try
            {
                byte[] desKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] desIV = desKey;

                // Create the file streams to handle the input and output files.
                var fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
                var fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                // Create variables to help with read and write.
                byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
                long rdlen = 0;              //This is the total number of bytes written.
                long totlen = fin.Length;    //This is the total length of the input file.
                int len;                     //This is the number of bytes to be written at a time.

                var des = new DESCryptoServiceProvider();

                var encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

                // Read from the input file, then encrypt and write to the output file.
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    encStream.Write(bin, 0, len);
                    rdlen = rdlen + len;
                }

                encStream.Close();
                fout.Close();
                fin.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// DES解密数据文件
        /// </summary>
        /// <param name="inName">待解密的数据文件</param>
        /// <param name="outName">待输出的数据文件</param>
        /// <param name="encryptKey">The encrypt key.</param>
        /// <returns>
        /// 加密成功返回true，失败返false
        /// </returns>
        public static bool DecryptData(String inName, String outName, string encryptKey)
        {
            try
            {
                byte[] desKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] desIV = desKey;

                //Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                //Create variables to help with read and write.
                byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
                long rdlen = 0;              //This is the total number of bytes written.
                long totlen = fin.Length;    //This is the total length of the input file.
                int len;                     //This is the number of bytes to be written at a time.

                var des = new DESCryptoServiceProvider();
                var encStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);
               
                //Read from the input file, then encrypt and write to the output file.
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    encStream.Write(bin, 0, len);
                    rdlen = rdlen + len;
                }

                encStream.Close();
                fout.Close();
                fin.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
