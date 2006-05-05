using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace EVEMon.Common
{
    class EncryptionHelper
    {
        private static string m_key = "e8Now%n(7Or;[+ow"; //keep it secret, keep it safe
        private static ASCIIEncoding m_encoding = new ASCIIEncoding();

        public static string Decrypt(string value)
        {
            //set up the encryption
            RijndaelManaged RMCrypto = new RijndaelManaged();
            MemoryStream m_plainStream = new MemoryStream();
            string longName = "o8o(,l7C";//some user input here would be better... username maybe?
            while (longName.Length < 16)
            {
                longName += longName;
            }
            CryptoStream m_cryptStream = new CryptoStream(m_plainStream,
                RMCrypto.CreateDecryptor(m_encoding.GetBytes(m_key), m_encoding.GetBytes(longName.Substring(0, 16))),
                CryptoStreamMode.Write);
            byte[] pBytes = Convert.FromBase64String(value);
            m_cryptStream.Write(pBytes, 0, pBytes.Length);
            m_cryptStream.FlushFinalBlock();
            string decrypted = m_encoding.GetString(m_plainStream.ToArray());
            return decrypted;
        }

        public static string Encrypt(string password)
        {
            //set up the encryption
            RijndaelManaged RMCrypto = new RijndaelManaged();
            MemoryStream m_plainStream = new MemoryStream();
            string longName = "o8o(,l7C";//some user input here would be better... username maybe?
            while (longName.Length < 16)
            {
                longName += longName;
            }
            CryptoStream m_cryptStream = new CryptoStream(m_plainStream,
                RMCrypto.CreateEncryptor(m_encoding.GetBytes(m_key), m_encoding.GetBytes(longName.Substring(0, 16))),
                CryptoStreamMode.Write);
            byte[] pBytes = m_encoding.GetBytes(password);
            m_cryptStream.Write(pBytes, 0, pBytes.Length);
            m_cryptStream.FlushFinalBlock();
            string encrypted = Convert.ToBase64String(m_plainStream.ToArray());
            return encrypted;
        }
    }
}
