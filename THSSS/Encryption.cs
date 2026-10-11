using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Shooting
{
	// Token: 0x0200020E RID: 526
	internal class Encryption
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x00073A20 File Offset: 0x00071C20
		public static string Encrypt(string pToEncrypt)
		{
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
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00073AF0 File Offset: 0x00071CF0
		public static string Decrypt(string pToDecrypt)
		{
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
		}
	}
}
