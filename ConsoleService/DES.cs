using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MallHost
{
    /// <summary>
    /// DES ����
    /// </summary>
    public class DES
    {
        #region ���췽��
        /// <summary>
        /// ��̬���췽��
        /// </summary>
        static DES()
        {
            KeyValue = RandomKey();
        }
        #endregion

        #region ˽�б���
        /// <summary>
        /// Ĭ����Կ����
        /// </summary>
        private static byte[] desKey = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        #endregion

        #region ���б���
        /// <summary>
        /// Ĭ�ϼ�����Կ
        /// </summary>
        public static string ConstKey = "22331144";

        /// <summary>
        /// �ؼ�����Կ
        /// </summary>
        public static string KeyValue { get; set; }

        #endregion

        /// <summary>
        /// �����Կ
        /// </summary>
        /// <returns>���������Կ</returns>
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
        /// DES�����ַ���
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns>
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
        /// DES�����ַ���
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns>
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
        /// DES���������ļ�
        /// </summary>
        /// <param name="inName">�����ܵ������ļ�</param>
        /// <param name="outName">������������ļ�</param>
        /// <param name="encryptKey">The encrypt key.</param>
        /// <returns>
        /// ���ܳɹ�����true��ʧ�ܷ�false
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
        /// DES���������ļ�
        /// </summary>
        /// <param name="inName">�����ܵ������ļ�</param>
        /// <param name="outName">������������ļ�</param>
        /// <param name="encryptKey">The encrypt key.</param>
        /// <returns>
        /// ���ܳɹ�����true��ʧ�ܷ�false
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
