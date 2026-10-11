using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Shooting
{
	internal class Encryption
	{
		public static string Encrypt(string pToEncrypt)
		{
#if BROWSER
			return DesCbc.EncryptToHex(pToEncrypt, "SeiweLL0", "0SeiweLL");
#else
			string text = "SeiweLL0";
			string text2 = "0SeiweLL";
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
			descryptoServiceProvider.Key = Encoding.ASCII.GetBytes(text);
			descryptoServiceProvider.IV = Encoding.ASCII.GetBytes(text2);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in memoryStream.ToArray())
			{
				stringBuilder.AppendFormat("{0:X2}", b);
			}
			return stringBuilder.ToString();
#endif
		}

		public static string Decrypt(string pToDecrypt)
		{
#if BROWSER
			return DesCbc.DecryptFromHex(pToDecrypt, "SeiweLL0", "0SeiweLL");
#else
			string text = "SeiweLL0";
			string text2 = "0SeiweLL";
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			byte[] array = new byte[pToDecrypt.Length / 2];
			for (int i = 0; i < pToDecrypt.Length / 2; i++)
			{
				int num = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 16);
				array[i] = (byte)num;
			}
			descryptoServiceProvider.Key = Encoding.ASCII.GetBytes(text);
			descryptoServiceProvider.IV = Encoding.ASCII.GetBytes(text2);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(array, 0, array.Length);
			cryptoStream.FlushFinalBlock();
			StringBuilder stringBuilder = new StringBuilder();
			return Encoding.Default.GetString(memoryStream.ToArray());
#endif
		}
	}
}
