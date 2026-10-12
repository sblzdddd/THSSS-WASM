using System;

namespace Shooting
{
	[Serializable]
	public class Event
	{
		public void String2EmitterEvent()
		{
			var eventString = EventString;
			var text = eventString.Split(new char[] { '：' })[0];
			var text2 = "";
			var text3 = "";
			var text4 = "";
			var text5 = "";
			var text6 = "";
			var text7 = eventString.Split(new char[] { '：' })[1];
			var num = 0f;
			var num2 = 0;
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
				var text8 = text2.Split(new char[] { '>' })[0];
				text5 = ">";
				text2 = text2.Split(new char[] { '>' })[1];
			}
			else if (text2.Contains("="))
			{
				var text9 = text2.Split(new char[] { '=' })[0];
				text5 = "=";
				text2 = text2.Split(new char[] { '=' })[1];
			}
			else if (text2.Contains("<"))
			{
				var text10 = text2.Split(new char[] { '<' })[0];
				text5 = "<";
				text2 = text2.Split(new char[] { '<' })[1];
			}
			if (eventString.Contains("变化到"))
			{
				var num3 = 0;
				var array = text7.Split("变".ToCharArray())[1].Split("，".ToCharArray());
				var num4 = (int)Hash.results[text7.Split("变".ToCharArray())[0]];
				var text11 = text7.Split("变".ToCharArray())[0];
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
				var text12 = array[1];
				var num5 = int.Parse(array[2].Split("帧".ToCharArray())[0]);
				condition = text;
				result = text7;
				condition2 = text2;
				contype = (int)Hash.conditions[text3];
				contype2 = (int)Hash.conditions[text3];
				opreator = text4;
				opreator2 = text5;
				collector = text6;
				change = num3;
				changetype = (int)Hash.type[text12];
				changevalue = num4;
				changename = (int)Hash.results[text11];
				res = num;
				special = num2;
				if (array[0].Replace("化到", "").Contains("+"))
				{
					rand = float.Parse(array[0].Replace("化到", "").Split(new char[] { '+' })[1]);
				}
				times = num5;
				if (array[2].Contains("("))
				{
					time = int.Parse(array[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("增加"))
			{
				var num3 = 1;
				var array2 = text7.Split("增".ToCharArray())[1].Split("，".ToCharArray());
				array2[0] = array2[0].Replace("加", "");
				var num4 = (int)Hash.results[text7.Split("增".ToCharArray())[0]];
				var text11 = text7.Split("增".ToCharArray())[0];
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
				var text12 = array2[1];
				var num5 = int.Parse(array2[2].Split("帧".ToCharArray())[0]);
				condition = text;
				result = text7;
				condition2 = text2;
				contype = (int)Hash.conditions[text3];
				contype2 = (int)Hash.conditions[text3];
				opreator = text4;
				opreator2 = text5;
				collector = text6;
				change = num3;
				changetype = (int)Hash.type[text12];
				changevalue = num4;
				changename = (int)Hash.results[text11];
				res = num;
				special = num2;
				if (array2[0].Contains("+"))
				{
					rand = float.Parse(array2[0].Split(new char[] { '+' })[1]);
				}
				times = num5;
				if (array2[2].Contains("("))
				{
					time = int.Parse(array2[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("减少"))
			{
				var num3 = 2;
				var array3 = text7.Split("减少".ToCharArray())[2].Split("，".ToCharArray());
				var num4 = (int)Hash.results[text7.Split("减少".ToCharArray())[0]];
				var text11 = text7.Split("减少".ToCharArray())[0];
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
				var text12 = array3[1];
				var num5 = int.Parse(array3[2].Split("帧".ToCharArray())[0]);
				condition = text;
				result = text7;
				condition2 = text2;
				contype = (int)Hash.conditions[text3];
				contype2 = (int)Hash.conditions[text3];
				opreator = text4;
				opreator2 = text5;
				collector = text6;
				change = num3;
				changetype = (int)Hash.type[text12];
				changevalue = num4;
				changename = (int)Hash.results[text11];
				res = num;
				special = num2;
				if (array3[0].Contains("+"))
				{
					rand = float.Parse(array3[0].Split(new char[] { '+' })[1]);
				}
				times = num5;
				if (array3[2].Contains("("))
				{
					time = int.Parse(array3[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("恢复"))
			{
				special = 1;
				opreator = text4;
				opreator2 = text5;
				condition = text;
				condition2 = text2;
				contype = (int)Hash.conditions[text3];
				contype2 = (int)Hash.conditions[text3];
				collector = text6;
			}
			else if (eventString.Contains("发射"))
			{
				special = 2;
				opreator = text4;
				opreator2 = text5;
				condition = text;
				condition2 = text2;
				contype = (int)Hash.conditions[text3];
				contype2 = (int)Hash.conditions[text3];
				collector = text6;
			}
		}

		public void String2BulletEvent()
		{
			var eventString = EventString;
			var text = eventString.Split(new char[] { '：' })[0];
			var text2 = "";
			var text3 = "";
			var text4 = "";
			var text5 = "";
			var text6 = "";
			var text7 = eventString.Split(new char[] { '：' })[1];
			var num = 0f;
			var num2 = 0;
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
				var text8 = text2.Split(new char[] { '>' })[0];
				text5 = ">";
				text2 = text2.Split(new char[] { '>' })[1];
			}
			else if (text2.Contains("="))
			{
				var text9 = text2.Split(new char[] { '=' })[0];
				text5 = "=";
				text2 = text2.Split(new char[] { '=' })[1];
			}
			else if (text2.Contains("<"))
			{
				var text10 = text2.Split(new char[] { '<' })[0];
				text5 = "<";
				text2 = text2.Split(new char[] { '<' })[1];
			}
			if (eventString.Contains("变化到"))
			{
				var num3 = 0;
				var array = text7.Split("变".ToCharArray())[1].Split("，".ToCharArray());
				var num4 = (int)Hash.results2[text7.Split("变".ToCharArray())[0]];
				var text11 = text7.Split("变".ToCharArray())[0];
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
				var text12 = array[1];
				var num5 = int.Parse(array[2].Split("帧".ToCharArray())[0]);
				condition = text;
				result = text7;
				condition2 = text2;
				contype = (int)Hash.conditions2[text3];
				contype2 = (int)Hash.conditions2[text3];
				opreator = text4;
				opreator2 = text5;
				collector = text6;
				change = num3;
				changetype = (int)Hash.type[text12];
				changevalue = num4;
				changename = (int)Hash.results2[text11];
				res = num;
				special = num2;
				if (array[0].Replace("化到", "").Contains("+"))
				{
					rand = float.Parse(array[0].Replace("化到", "").Split(new char[] { '+' })[1]);
				}
				times = num5;
				if (array[2].Contains("("))
				{
					time = int.Parse(array[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("增加"))
			{
				var num3 = 1;
				var array2 = text7.Split("增".ToCharArray())[1].Split("，".ToCharArray());
				array2[0] = array2[0].Replace("加", "");
				var num4 = (int)Hash.results2[text7.Split("增".ToCharArray())[0]];
				var text11 = text7.Split("增".ToCharArray())[0];
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
				var text12 = array2[1];
				var num5 = int.Parse(array2[2].Split("帧".ToCharArray())[0]);
				condition = text;
				result = text7;
				condition2 = text2;
				contype = (int)Hash.conditions2[text3];
				contype2 = (int)Hash.conditions2[text3];
				opreator = text4;
				opreator2 = text5;
				collector = text6;
				change = num3;
				changetype = (int)Hash.type[text12];
				changevalue = num4;
				changename = (int)Hash.results2[text11];
				res = num;
				special = num2;
				if (array2[0].Contains("+"))
				{
					rand = float.Parse(array2[0].Split(new char[] { '+' })[1]);
				}
				times = num5;
				if (array2[2].Contains("("))
				{
					time = int.Parse(array2[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("减少"))
			{
				var num3 = 2;
				var array3 = text7.Split("减少".ToCharArray())[2].Split("，".ToCharArray());
				var num4 = (int)Hash.results2[text7.Split("减少".ToCharArray())[0]];
				var text11 = text7.Split("减少".ToCharArray())[0];
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
				var text12 = array3[1];
				var num5 = int.Parse(array3[2].Split("帧".ToCharArray())[0]);
				condition = text;
				result = text7;
				condition2 = text2;
				contype = (int)Hash.conditions2[text3];
				contype2 = (int)Hash.conditions2[text3];
				opreator = text4;
				opreator2 = text5;
				collector = text6;
				change = num3;
				changetype = (int)Hash.type[text12];
				changevalue = num4;
				changename = (int)Hash.results2[text11];
				res = num;
				special = num2;
				if (array3[0].Contains("+"))
				{
					rand = float.Parse(array3[0].Split(new char[] { '+' })[1]);
				}
				times = num5;
				if (array3[2].Contains("("))
				{
					time = int.Parse(array3[2].Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
				}
			}
			else if (eventString.Contains("恢复"))
			{
				special = 1;
				opreator = text4;
				opreator2 = text5;
				condition = text;
				condition2 = text2;
				contype = (int)Hash.conditions2[text3];
				contype2 = (int)Hash.conditions2[text3];
				collector = text6;
			}
			else if (eventString.Contains("发射"))
			{
				special = 2;
				opreator = text4;
				opreator2 = text5;
				condition = text;
				condition2 = text2;
				contype = (int)Hash.conditions2[text3];
				contype2 = (int)Hash.conditions2[text3];
				collector = text6;
			}
		}

		public object Clone()
		{
			return MemberwiseClone();
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
