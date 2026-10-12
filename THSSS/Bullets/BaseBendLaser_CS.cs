using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class BaseBendLaser_CS : Bullet_BendLaser
	{
		public override int Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
				if (value >= 0 && value < 16)
				{
					ColorType = BulletColorType[value];
					TxtureObject = TextureObjectDictionary["Laser0_" + ColorType];
					GhostingColor = Color.White;
					SizeValue = 32;
					Region = 1;
				}
			}
		}

		public BaseBendLaser_CS(StageDataPackage StageData)
			: base(StageData)
		{
			Active = true;
			EventGroupList = new List<EventGroup>();
			EventsExecutionList = new List<Execution>();
		}

		public override void EventCtrl()
		{
			EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}

		private byte[] BulletColorType = new byte[] { 1, 3, 5, 7, 9, 13, 14, 15 };
	}
}
