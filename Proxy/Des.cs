using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProxyClient
{
    /// <summary>
    /// DES����
    /// </summary>
    internal sealed class Des
    {
        /// <summary>
        /// Ĭ����Կ����
        /// 
        /// </summary>
        private static byte[] desKey;

        /// <summary>
        /// Initializes the <see cref="Des"/> class.
        /// </summary>
        static Des()
        {
            desKey = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            ConstKey = "22331144";
        }

        /// <summary>
        /// ��Կ
        /// </summary>
        public static string ConstKey { get; private set; }

        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ8λ</param>
        /// <returns>
        /// ���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��
        /// </returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = desKey;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var dcsp = new DESCryptoServiceProvider();
                var mstream = new MemoryStream();
                CryptoStream cstream = new CryptoStream(mstream, dcsp.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cstream.Write(inputByteArray, 0, inputByteArray.Length);
                cstream.FlushFinalBlock();
                return Convert.ToBase64String(mstream.ToArray());
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
        /// <returns>
        /// ���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��
        /// </returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbkey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbiv = desKey;
                byte[] inputbytearray = Convert.FromBase64String(decryptString);
                var dcsp = new DESCryptoServiceProvider();
                var mstream = new MemoryStream();
                CryptoStream cstream = new CryptoStream(mstream, dcsp.CreateDecryptor(rgbkey, rgbiv), CryptoStreamMode.Write);
                cstream.Write(inputbytearray, 0, inputbytearray.Length);
                cstream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mstream.ToArray());
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
        public static bool EncryptData(string inName, string outName, string encryptKey)
        {
            try
            {
                byte[] desKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] desIV = desKey;

                // Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                // This is intermediate storage for the encryption.
                byte[] bin = new byte[100];

                // This is the total number of bytes written.
                long rdlen = 0;

                // his is the total length of the input file.
                long totlen = fin.Length;

                // This is the number of bytes to be written at a time.
                int len;

                var des = new DESCryptoServiceProvider();

                CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

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
        public static bool DecryptData(string inName, string outName, string encryptKey)
        {
            try
            {
                byte[] desKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] desIV = desKey;

                // Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                // Create variables to help with read and write.
                byte[] bin = new byte[100];

                // This is the total number of bytes written.
                long rdlen = 0;

                // This is the total length of the input file.
                long totlen = fin.Length;

                // This is the number of bytes to be written at a time.
                int len;

                var des = new DESCryptoServiceProvider();

                CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

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
    }
}
