using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class BaseBullet_CS : BaseBullet_Touhou
	{
		public override int Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
				if (value >= 0 && value < this.BulletName.Length)
				{
					string text = this.BulletName[this.type];
					base.ColorType = this.BulletColorType[this.type];
					if (text != null)
					{
						base.TxtureObject = base.TextureObjectDictionary[text];
						base.GhostingColor = Color.White;
						base.SizeValue = 32;
						base.Region = 2;
						base.SetRegion(text);
					}
					else
					{
						base.Region = -100;
					}
				}
			}
		}

		public BaseBullet_CS(StageDataPackage StageData)
			: base(StageData)
		{
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (this.Type == 181 || this.Type == 185)
			{
				base.TxtureObject = base.TextureObjectDictionary["bullet66_" + base.Time % 16 / 4];
			}
			else if (this.Type == 182)
			{
				base.TxtureObject = base.TextureObjectDictionary["bullet64_" + base.Time % 16 / 4];
			}
			else if (this.Type == 183)
			{
				base.TxtureObject = base.TextureObjectDictionary["bullet65_" + base.Time % 16 / 4];
			}
			else if (this.Type == 184)
			{
				base.TxtureObject = base.TextureObjectDictionary["bullet18-" + base.Time % 16 / 4];
			}
		}

		public override void EventCtrl()
		{
			base.EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			base.EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}

		private readonly string[] BulletName = new string[]
		{
			null, "bullet1_1", "bullet1_3", "bullet1_5", "bullet1_7", "bullet1_9", "bullet1_13", "bullet1_14", "bullet1_15", "bullet2_1",
			"bullet2_3", "bullet2_5", "bullet2_7", "bullet2_9", "bullet2_13", "bullet2_14", "bullet2_15", "bullet3_1", "bullet3_3", "bullet3_5",
			"bullet3_7", "bullet3_9", "bullet3_13", "bullet3_14", "bullet3_15", "bullet6_1", "bullet6_3", "bullet6_5", "bullet6_7", "bullet6_9",
			"bullet6_13", "bullet6_14", "bullet6_15", "bullet4_1", "bullet4_3", "bullet4_5", "bullet4_7", "bullet4_9", "bullet4_13", "bullet4_14",
			"bullet4_15", "bullet8_1", "bullet8_3", "bullet8_5", "bullet8_7", "bullet8_9", "bullet8_13", "bullet8_14", "bullet8_15", "bullet7_1",
			"bullet7_3", "bullet7_5", "bullet7_7", "bullet7_9", "bullet7_13", "bullet7_14", "bullet7_15", "bullet9_1", "bullet9_3", "bullet9_5",
			"bullet9_7", "bullet9_9", "bullet9_13", "bullet9_14", "bullet9_15", "bullet10_1", "bullet10_3", "bullet10_5", "bullet10_7", "bullet10_9",
			"bullet10_13", "bullet10_14", "bullet10_15", "bullet15_1", "bullet15_3", "bullet15_5", "bullet15_7", "bullet15_9", "bullet15_13", "bullet15_14",
			"bullet15_15", "bullet6_1", "bullet6_3", "bullet6_5", "bullet6_7", "bullet6_9", "bullet6_13", "bullet6_14", "bullet6_15", "Flower01",
			"Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "bullet12_1", "bullet12_3", "bullet12_5",
			"bullet12_7", "bullet12_9", "bullet12_13", "bullet12_14", "bullet20_1", "bullet20_2", "bullet20_3", "bullet20_4", "bullet20_5", "bullet20_6",
			"bullet20_6", "bullet20_7", "bullet21_1", "bullet21_2", "bullet21_3", "bullet21_4", "bullet21_5", "bullet21_6", "bullet21_6", "bullet21_7",
			"bullet22_1", "bullet22_2", "bullet22_3", "bullet22_4", "bullet22_5", "bullet22_6", "bullet22_6", "bullet22_7", "bullet23_1", "bullet23_2",
			"bullet23_3", "bullet23_4", "bullet23_5", "bullet23_6", "bullet23_6", "bullet23_7", "Flower01", "Flower01", "Flower01", "Flower01",
			"Flower01", "Flower01", "Flower01", "bullet24_1", "bullet24_2", "bullet24_3", "bullet24_4", "bullet24_5", "bullet24_6", "bullet24_6",
			"bullet24_7", "bullet46_1", "bullet46_3", "bullet46_5", "bullet46_7", "bullet46_9", "bullet46_13", "bullet46_14", "bullet46_15", "bullet40_1",
			"bullet40_2", "bullet40_3", "bullet40_4", "bullet40_5", "bullet40_6", "bullet40_6", "bullet40_7", "bullet61_1", "bullet61_2", "bullet61_3",
			"bullet61_4", "bullet61_5", "bullet61_6", "bullet61_6", "bullet26_0", "bullet26_1", "bullet26_0", "bullet26_2", "bullet26_3", "Flower01",
			"Flower01", "bullet66_0", "bullet64_0", "bullet65_0", "bullet18-0", "bullet67_0", "bullet13_0", "bullet13_1", "bullet13_2", "Flower01",
			"Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01",
			"Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01",
			"Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01", "Flower01",
			"Flower01", "bullet50_4", "bullet50_0", "bullet50_5", "bullet50_1", "bullet50_6", "bullet50_2", "bullet50_7", "bullet50_3"
		};

		private readonly byte[] BulletColorType = new byte[]
		{
			0, 1, 3, 5, 7, 9, 13, 14, 15, 1,
			3, 5, 7, 9, 13, 14, 15, 1, 3, 5,
			7, 9, 13, 14, 15, 1, 3, 5, 7, 9,
			13, 14, 15, 1, 3, 5, 7, 9, 13, 14,
			15, 1, 3, 5, 7, 9, 13, 14, 15, 1,
			3, 5, 7, 9, 13, 14, 15, 1, 3, 5,
			7, 9, 13, 14, 15, 1, 3, 5, 7, 9,
			13, 14, 15, 1, 3, 5, 7, 9, 13, 14,
			15, 1, 3, 5, 7, 9, 13, 14, 15, 1,
			3, 5, 7, 9, 13, 14, 15, 1, 3, 5,
			7, 9, 13, 15, 1, 3, 5, 7, 9, 13,
			14, 15, 1, 3, 5, 7, 9, 13, 14, 15,
			1, 3, 5, 7, 9, 13, 14, 15, 1, 3,
			5, 7, 9, 13, 14, 15, 1, 3, 5, 7,
			9, 11, 13, 1, 3, 5, 7, 9, 13, 14,
			15, 1, 3, 5, 7, 9, 13, 14, 15, 1,
			3, 5, 7, 9, 13, 14, 15, 1, 3, 5,
			7, 9, 13, 14, 1, 5, 3, 9, 13, 0,
			0, 6, 1, 3, 7, 6, 13, 15, 11, 1,
			5, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 7, 0, 9, 1, 13, 3, 15, 5
		};
	}
}
