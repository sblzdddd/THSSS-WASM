using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class BaseRadialLaser_CS : Bullet_RadialLaser
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
				if (value >= 0 && value < 16)
				{
					base.ColorType = this.BulletColorType[value];
					base.TxtureObject = base.TextureObjectDictionary["Laser0_" + base.ColorType];
					base.GhostingColor = Color.White;
					base.SizeValue = 32;
					base.Region = 3;
				}
			}
		}

		public BaseRadialLaser_CS(StageDataPackage StageData)
			: base(StageData)
		{
			base.Active = true;
			base.UnmatchedTime = 60;
			base.UnRemoveable = true;
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
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

		private byte[] BulletColorType = new byte[] { 1, 3, 5, 7, 9, 13, 14, 15 };
	}
}
