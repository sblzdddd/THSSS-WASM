using System;

namespace Shooting
{
	[Serializable]
	public class Event
	{
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

		public object Clone()
		{
			return base.MemberwiseClone();
		}

		public string EventString;

		public float rand;

		public int special;

		public int special2;

		public string condition;

		public string condition2;

		public string result;

		public int contype;

		public int contype2;

		public string opreator;

		public string opreator2;

		public string collector;

		public int change;

		public int changetype;

		public int changevalue;

		public int changename;

		public float res;

		public int times;

		public int time;

		public bool noloop;
	}
}
