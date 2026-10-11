using System;
using System.Collections;

namespace Shooting
{
	// Token: 0x020001CD RID: 461
	public class Hash
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x00066BEC File Offset: 0x00064DEC
		public static void Init()
		{
			Hash.type.Add("正比", 0);
			Hash.type.Add("固定", 1);
			Hash.type.Add("正弦", 2);
			Hash.conditions.Add("", 0);
			Hash.conditions.Add("当前帧", 0);
			Hash.conditions.Add("X坐标", 1);
			Hash.conditions.Add("Y坐标", 2);
			Hash.conditions.Add("半径", 3);
			Hash.conditions.Add("半径方向", 4);
			Hash.conditions.Add("条数", 5);
			Hash.conditions.Add("周期", 6);
			Hash.conditions.Add("角度", 7);
			Hash.conditions.Add("范围", 8);
			Hash.conditions.Add("宽比", 9);
			Hash.conditions.Add("高比", 10);
			Hash.conditions.Add("不透明度", 11);
			Hash.conditions.Add("朝向", 12);
			Hash.results.Add("X坐标", 0);
			Hash.results.Add("Y坐标", 1);
			Hash.results.Add("半径", 2);
			Hash.results.Add("半径方向", 3);
			Hash.results.Add("条数", 4);
			Hash.results.Add("周期", 5);
			Hash.results.Add("角度", 6);
			Hash.results.Add("范围", 7);
			Hash.results.Add("速度", 8);
			Hash.results.Add("速度方向", 9);
			Hash.results.Add("加速度", 10);
			Hash.results.Add("加速度方向", 11);
			Hash.results.Add("生命", 12);
			Hash.results.Add("类型", 13);
			Hash.results.Add("宽比", 14);
			Hash.results.Add("高比", 15);
			Hash.results.Add("R", 16);
			Hash.results.Add("G", 17);
			Hash.results.Add("B", 18);
			Hash.results.Add("不透明度", 19);
			Hash.results.Add("朝向", 20);
			Hash.results.Add("子弹速度", 21);
			Hash.results.Add("子弹速度方向", 22);
			Hash.results.Add("子弹加速度", 23);
			Hash.results.Add("子弹加速度方向", 24);
			Hash.results.Add("横比", 25);
			Hash.results.Add("纵比", 26);
			Hash.results.Add("雾化效果", 27);
			Hash.results.Add("消除效果", 28);
			Hash.results.Add("高光效果", 29);
			Hash.results.Add("拖影效果", 30);
			Hash.results.Add("出屏即消", 31);
			Hash.results.Add("无敌状态", 32);
			Hash.conditions2.Add("", 0);
			Hash.conditions2.Add("当前帧", 0);
			Hash.conditions2.Add("X坐标", 1);
			Hash.conditions2.Add("Y坐标", 2);
			Hash.results2.Add("生命", 0);
			Hash.results2.Add("类型", 1);
			Hash.results2.Add("宽比", 2);
			Hash.results2.Add("高比", 3);
			Hash.results2.Add("R", 4);
			Hash.results2.Add("G", 5);
			Hash.results2.Add("B", 6);
			Hash.results2.Add("不透明度", 7);
			Hash.results2.Add("朝向", 8);
			Hash.results2.Add("子弹速度", 9);
			Hash.results2.Add("子弹速度方向", 10);
			Hash.results2.Add("子弹加速度", 11);
			Hash.results2.Add("子弹加速度方向", 12);
			Hash.results2.Add("横比", 13);
			Hash.results2.Add("纵比", 14);
			Hash.results2.Add("雾化效果", 15);
			Hash.results2.Add("消除效果", 16);
			Hash.results2.Add("高光效果", 17);
			Hash.results2.Add("拖影效果", 18);
			Hash.results2.Add("出屏即消", 19);
			Hash.results2.Add("无敌状态", 20);
			Hash.cconditions.Add("", 0);
			Hash.cconditions.Add("当前帧", 0);
			Hash.cconditions.Add("X坐标", 1);
			Hash.cconditions.Add("Y坐标", 2);
			Hash.cconditions.Add("半宽", 3);
			Hash.cconditions.Add("半高", 4);
			Hash.cresults.Add("半宽", 0);
			Hash.cresults.Add("半高", 1);
			Hash.cresults.Add("启用圆形", 2);
			Hash.cresults.Add("速度", 3);
			Hash.cresults.Add("速度方向", 4);
			Hash.cresults.Add("加速度", 5);
			Hash.cresults.Add("加速度方向", 6);
			Hash.cresults.Add("类型", 7);
			Hash.cresults.Add("ID", 8);
			Hash.cresults.Add("X坐标", 9);
			Hash.cresults.Add("Y坐标", 10);
		}

		// Token: 0x040003EE RID: 1006
		public static Hashtable conditions = new Hashtable();

		// Token: 0x040003EF RID: 1007
		public static Hashtable results = new Hashtable();

		// Token: 0x040003F0 RID: 1008
		public static Hashtable type = new Hashtable();

		// Token: 0x040003F1 RID: 1009
		public static Hashtable conditions2 = new Hashtable();

		// Token: 0x040003F2 RID: 1010
		public static Hashtable results2 = new Hashtable();

		// Token: 0x040003F3 RID: 1011
		public static Hashtable results3 = new Hashtable();

		// Token: 0x040003F4 RID: 1012
		public static Hashtable cconditions = new Hashtable();

		// Token: 0x040003F5 RID: 1013
		public static Hashtable cresults = new Hashtable();

		// Token: 0x040003F6 RID: 1014
		public static Hashtable lconditions = new Hashtable();

		// Token: 0x040003F7 RID: 1015
		public static Hashtable lresults = new Hashtable();

		// Token: 0x040003F8 RID: 1016
		public static Hashtable lresults2 = new Hashtable();
	}
}
