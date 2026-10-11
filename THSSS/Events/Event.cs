using System;

namespace Shooting
{
	// Token: 0x02000156 RID: 342
	[Serializable]
	public class Event
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x000511D4 File Offset: 0x0004F3D4
		public void String2EmitterEvent()
		{
			string eventString = this.EventString;
			string text = eventString.Split(new char[] { '：' })[0];
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = eventString.Split(new char[] { '：' })[1];
			float num = 0f;
			int num2 = 0;
			if (eventString.Contains("且"))
			{
				text6 = "且";
				text2 = text.Split("且".ToCharArray())[1];
				text = text.Split("且".ToCharArray())[0];
			}
			else if (eventString.Contains("或"))
			{
				text6 = "或";
				text2 = text.Split("或".ToCharArray())[1];
				text = text.Split("或".ToCharArray())[0];
			}
			if (text.Contains(">"))
			{
				text3 = text.Split(new char[] { '>' })[0];
				text4 = ">";
				text = text.Split(new char[] { '>' })[1];
			}
			else if (text.Contains("="))
			{
				text3 = text.Split(new char[] { '=' })[0];
				text4 = "=";
				text = text.Split(new char[] { '=' })[1];
			}
			else if (text.Contains("<"))
			{
				text3 = text.Split(new char[] { '<' })[0];
				text4 = "<";
				text = text.Split(new char[] { '<' })[1];
			}
			if (text2.Contains(">"))
			{
				string text8 = text2.Split(new char[] { '>' })[0];
				text5 = ">";
				text2 = text2.Split(new char[] { '>' })[1];
			}
			else if (text2.Contains("="))
			{
				string text9 = text2.Split(new char[] { '=' })[0];
				text5 = "=";
				text2 = text2.Split(new char[] { '=' })[1];
			}
			else if (text2.Contains("<"))
			{
				string text10 = text2.Split(new char[] { '<' })[0];
				text5 = "<";
				text2 = text2.Split(new char[] { '<' })[1];
			}
			if (eventString.Contains("变化到"))
			{
				int num3 = 0;
				string[] array = text7.Split("变".ToCharArray())[1].Split("，".ToCharArray());
				int num4 = (int)Hash.results[text7.Split("变".ToCharArray())[0]];
				string text11 = text7.Split("变".ToCharArray())[0];
				if (array[0].Replace("化到", "").Contains("+"))
				{
					if (array[0].Replace("化到", "").Split(new char[] { '+' })[0] == "自身")
					{
						num2 = 3;
					}
					else if (array[0].Replace("化到", "").Split(new char[] { '+' })[0] == "自机")
					{
						num2 = 4;
					}
					else
					{
						num = float.Parse(array[0].Replace("化到", "").Split(new char[] { '+' })[0]);
					}
				}
				else if (array[0].Replace("化到", "") == "自身")
				{
					num2 = 3;
				}
				else if (array[0].Replace("化到", "") == "自机")
				{
					num2 = 4;
				}
				else
				{
					num = float.Parse(array[0].Replace("化到", ""));
				}
				string text12 = array[1];
				int num5 = int.Parse(array[2].Split("帧".ToCharArray())[0]);
				this.condition = text;
				this.result = text7;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions[text3];
				this.contype2 = (int)Hash.conditions[text3];
				this.opreator = text4;
				this.opreator2 = text5;
				this.collector = text6;
				this.change = num3;
				this.changetype = (int)Hash.type[text12];
				this.changevalue = num4;
				this.changename = (int)Hash.results[text11];
				this.res = num;
				this.special = num2;
				if (array[0].Replace("化到", "").Contains("+"))
				{
					this.rand = float.Parse(array[0].Replace("化到", "").Split(new char[] { '+' })[1]);
				}
				this.times = num5;
				if (array[2].Contains("("))
				{
					this.time = int.Parse(array[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("增加"))
			{
				int num3 = 1;
				string[] array2 = text7.Split("增".ToCharArray())[1].Split("，".ToCharArray());
				array2[0] = array2[0].Replace("加", "");
				int num4 = (int)Hash.results[text7.Split("增".ToCharArray())[0]];
				string text11 = text7.Split("增".ToCharArray())[0];
				if (array2[0].Contains("+"))
				{
					if (array2[0].Split(new char[] { '+' })[0] == "自身")
					{
						num2 = 3;
					}
					else if (array2[0].Split(new char[] { '+' })[0] == "自机")
					{
						num2 = 4;
					}
					else
					{
						num = float.Parse(array2[0].Split(new char[] { '+' })[0]);
					}
				}
				else if (array2[0] == "自身")
				{
					num2 = 3;
				}
				else if (array2[0] == "自机")
				{
					num2 = 4;
				}
				else
				{
					num = float.Parse(array2[0]);
				}
				string text12 = array2[1];
				int num5 = int.Parse(array2[2].Split("帧".ToCharArray())[0]);
				this.condition = text;
				this.result = text7;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions[text3];
				this.contype2 = (int)Hash.conditions[text3];
				this.opreator = text4;
				this.opreator2 = text5;
				this.collector = text6;
				this.change = num3;
				this.changetype = (int)Hash.type[text12];
				this.changevalue = num4;
				this.changename = (int)Hash.results[text11];
				this.res = num;
				this.special = num2;
				if (array2[0].Contains("+"))
				{
					this.rand = float.Parse(array2[0].Split(new char[] { '+' })[1]);
				}
				this.times = num5;
				if (array2[2].Contains("("))
				{
					this.time = int.Parse(array2[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("减少"))
			{
				int num3 = 2;
				string[] array3 = text7.Split("减少".ToCharArray())[2].Split("，".ToCharArray());
				int num4 = (int)Hash.results[text7.Split("减少".ToCharArray())[0]];
				string text11 = text7.Split("减少".ToCharArray())[0];
				if (array3[0].Contains("+"))
				{
					if (array3[0].Split(new char[] { '+' })[0] == "自身")
					{
						num2 = 3;
					}
					else if (array3[0].Split(new char[] { '+' })[0] == "自机")
					{
						num2 = 4;
					}
					else
					{
						num = float.Parse(array3[0].Split(new char[] { '+' })[0]);
					}
				}
				else if (array3[0] == "自身")
				{
					num2 = 3;
				}
				else if (array3[0] == "自机")
				{
					num2 = 4;
				}
				else
				{
					num = float.Parse(array3[0]);
				}
				string text12 = array3[1];
				int num5 = int.Parse(array3[2].Split("帧".ToCharArray())[0]);
				this.condition = text;
				this.result = text7;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions[text3];
				this.contype2 = (int)Hash.conditions[text3];
				this.opreator = text4;
				this.opreator2 = text5;
				this.collector = text6;
				this.change = num3;
				this.changetype = (int)Hash.type[text12];
				this.changevalue = num4;
				this.changename = (int)Hash.results[text11];
				this.res = num;
				this.special = num2;
				if (array3[0].Contains("+"))
				{
					this.rand = float.Parse(array3[0].Split(new char[] { '+' })[1]);
				}
				this.times = num5;
				if (array3[2].Contains("("))
				{
					this.time = int.Parse(array3[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("恢复"))
			{
				this.special = 1;
				this.opreator = text4;
				this.opreator2 = text5;
				this.condition = text;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions[text3];
				this.contype2 = (int)Hash.conditions[text3];
				this.collector = text6;
			}
			else if (eventString.Contains("发射"))
			{
				this.special = 2;
				this.opreator = text4;
				this.opreator2 = text5;
				this.condition = text;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions[text3];
				this.contype2 = (int)Hash.conditions[text3];
				this.collector = text6;
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00051ED4 File Offset: 0x000500D4
		public void String2BulletEvent()
		{
			string eventString = this.EventString;
			string text = eventString.Split(new char[] { '：' })[0];
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = eventString.Split(new char[] { '：' })[1];
			float num = 0f;
			int num2 = 0;
			if (eventString.Contains("且"))
			{
				text6 = "且";
				text2 = text.Split("且".ToCharArray())[1];
				text = text.Split("且".ToCharArray())[0];
			}
			else if (eventString.Contains("或"))
			{
				text6 = "或";
				text2 = text.Split("或".ToCharArray())[1];
				text = text.Split("或".ToCharArray())[0];
			}
			if (text.Contains(">"))
			{
				text3 = text.Split(new char[] { '>' })[0];
				text4 = ">";
				text = text.Split(new char[] { '>' })[1];
			}
			else if (text.Contains("="))
			{
				text3 = text.Split(new char[] { '=' })[0];
				text4 = "=";
				text = text.Split(new char[] { '=' })[1];
			}
			else if (text.Contains("<"))
			{
				text3 = text.Split(new char[] { '<' })[0];
				text4 = "<";
				text = text.Split(new char[] { '<' })[1];
			}
			if (text2.Contains(">"))
			{
				string text8 = text2.Split(new char[] { '>' })[0];
				text5 = ">";
				text2 = text2.Split(new char[] { '>' })[1];
			}
			else if (text2.Contains("="))
			{
				string text9 = text2.Split(new char[] { '=' })[0];
				text5 = "=";
				text2 = text2.Split(new char[] { '=' })[1];
			}
			else if (text2.Contains("<"))
			{
				string text10 = text2.Split(new char[] { '<' })[0];
				text5 = "<";
				text2 = text2.Split(new char[] { '<' })[1];
			}
			if (eventString.Contains("变化到"))
			{
				int num3 = 0;
				string[] array = text7.Split("变".ToCharArray())[1].Split("，".ToCharArray());
				int num4 = (int)Hash.results2[text7.Split("变".ToCharArray())[0]];
				string text11 = text7.Split("变".ToCharArray())[0];
				if (array[0].Replace("化到", "").Contains("+"))
				{
					if (array[0].Replace("化到", "").Split(new char[] { '+' })[0] == "自身")
					{
						num2 = 3;
					}
					else if (array[0].Replace("化到", "").Split(new char[] { '+' })[0] == "自机")
					{
						num2 = 4;
					}
					else
					{
						num = float.Parse(array[0].Replace("化到", "").Split(new char[] { '+' })[0]);
					}
				}
				else if (array[0].Replace("化到", "") == "自身")
				{
					num2 = 3;
				}
				else if (array[0].Replace("化到", "") == "自机")
				{
					num2 = 4;
				}
				else
				{
					num = float.Parse(array[0].Replace("化到", ""));
				}
				string text12 = array[1];
				int num5 = int.Parse(array[2].Split("帧".ToCharArray())[0]);
				this.condition = text;
				this.result = text7;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions2[text3];
				this.contype2 = (int)Hash.conditions2[text3];
				this.opreator = text4;
				this.opreator2 = text5;
				this.collector = text6;
				this.change = num3;
				this.changetype = (int)Hash.type[text12];
				this.changevalue = num4;
				this.changename = (int)Hash.results2[text11];
				this.res = num;
				this.special = num2;
				if (array[0].Replace("化到", "").Contains("+"))
				{
					this.rand = float.Parse(array[0].Replace("化到", "").Split(new char[] { '+' })[1]);
				}
				this.times = num5;
				if (array[2].Contains("("))
				{
					this.time = int.Parse(array[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("增加"))
			{
				int num3 = 1;
				string[] array2 = text7.Split("增".ToCharArray())[1].Split("，".ToCharArray());
				array2[0] = array2[0].Replace("加", "");
				int num4 = (int)Hash.results2[text7.Split("增".ToCharArray())[0]];
				string text11 = text7.Split("增".ToCharArray())[0];
				if (array2[0].Contains("+"))
				{
					if (array2[0].Split(new char[] { '+' })[0] == "自身")
					{
						num2 = 3;
					}
					else if (array2[0].Split(new char[] { '+' })[0] == "自机")
					{
						num2 = 4;
					}
					else
					{
						num = float.Parse(array2[0].Split(new char[] { '+' })[0]);
					}
				}
				else if (array2[0] == "自身")
				{
					num2 = 3;
				}
				else if (array2[0] == "自机")
				{
					num2 = 4;
				}
				else
				{
					num = float.Parse(array2[0]);
				}
				string text12 = array2[1];
				int num5 = int.Parse(array2[2].Split("帧".ToCharArray())[0]);
				this.condition = text;
				this.result = text7;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions2[text3];
				this.contype2 = (int)Hash.conditions2[text3];
				this.opreator = text4;
				this.opreator2 = text5;
				this.collector = text6;
				this.change = num3;
				this.changetype = (int)Hash.type[text12];
				this.changevalue = num4;
				this.changename = (int)Hash.results2[text11];
				this.res = num;
				this.special = num2;
				if (array2[0].Contains("+"))
				{
					this.rand = float.Parse(array2[0].Split(new char[] { '+' })[1]);
				}
				this.times = num5;
				if (array2[2].Contains("("))
				{
					this.time = int.Parse(array2[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("减少"))
			{
				int num3 = 2;
				string[] array3 = text7.Split("减少".ToCharArray())[2].Split("，".ToCharArray());
				int num4 = (int)Hash.results2[text7.Split("减少".ToCharArray())[0]];
				string text11 = text7.Split("减少".ToCharArray())[0];
				if (array3[0].Contains("+"))
				{
					if (array3[0].Split(new char[] { '+' })[0] == "自身")
					{
						num2 = 3;
					}
					else if (array3[0].Split(new char[] { '+' })[0] == "自机")
					{
						num2 = 4;
					}
					else
					{
						num = float.Parse(array3[0].Split(new char[] { '+' })[0]);
					}
				}
				else if (array3[0] == "自身")
				{
					num2 = 3;
				}
				else if (array3[0] == "自机")
				{
					num2 = 4;
				}
				else
				{
					num = float.Parse(array3[0]);
				}
				string text12 = array3[1];
				int num5 = int.Parse(array3[2].Split("帧".ToCharArray())[0]);
				this.condition = text;
				this.result = text7;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions2[text3];
				this.contype2 = (int)Hash.conditions2[text3];
				this.opreator = text4;
				this.opreator2 = text5;
				this.collector = text6;
				this.change = num3;
				this.changetype = (int)Hash.type[text12];
				this.changevalue = num4;
				this.changename = (int)Hash.results2[text11];
				this.res = num;
				this.special = num2;
				if (array3[0].Contains("+"))
				{
					this.rand = float.Parse(array3[0].Split(new char[] { '+' })[1]);
				}
				this.times = num5;
				if (array3[2].Contains("("))
				{
					this.time = int.Parse(array3[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("恢复"))
			{
				this.special = 1;
				this.opreator = text4;
				this.opreator2 = text5;
				this.condition = text;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions2[text3];
				this.contype2 = (int)Hash.conditions2[text3];
				this.collector = text6;
			}
			else if (eventString.Contains("发射"))
			{
				this.special = 2;
				this.opreator = text4;
				this.opreator2 = text5;
				this.condition = text;
				this.condition2 = text2;
				this.contype = (int)Hash.conditions2[text3];
				this.contype2 = (int)Hash.conditions2[text3];
				this.collector = text6;
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00052BD4 File Offset: 0x00050DD4
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x04000325 RID: 805
		public string EventString;

		// Token: 0x04000326 RID: 806
		public float rand;

		// Token: 0x04000327 RID: 807
		public int special;

		// Token: 0x04000328 RID: 808
		public int special2;

		// Token: 0x04000329 RID: 809
		public string condition;

		// Token: 0x0400032A RID: 810
		public string condition2;

		// Token: 0x0400032B RID: 811
		public string result;

		// Token: 0x0400032C RID: 812
		public int contype;

		// Token: 0x0400032D RID: 813
		public int contype2;

		// Token: 0x0400032E RID: 814
		public string opreator;

		// Token: 0x0400032F RID: 815
		public string opreator2;

		// Token: 0x04000330 RID: 816
		public string collector;

		// Token: 0x04000331 RID: 817
		public int change;

		// Token: 0x04000332 RID: 818
		public int changetype;

		// Token: 0x04000333 RID: 819
		public int changevalue;

		// Token: 0x04000334 RID: 820
		public int changename;

		// Token: 0x04000335 RID: 821
		public float res;

		// Token: 0x04000336 RID: 822
		public int times;

		// Token: 0x04000337 RID: 823
		public int time;

		// Token: 0x04000338 RID: 824
		public bool noloop;
	}
}
