using System.Drawing;

namespace Shooting
{
	public class BaseEffect : BaseObject_CS
	{
		public int Layer { get; set; }

		public BaseEffect()
		{
		}

		public BaseEffect(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.EffectList.Add(this);
		}

		public BaseEffect(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.EffectList.Add(this);
			base.Active = true;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			this.Shoot();
			if (this.OutBoundary())
			{
				base.EffectList.Remove(this);
			}
		}

		public override bool HitCheck(BaseObject MyPlane)
		{
			return false;
		}
	}
}
