using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class StoryEmitterStar : BaseObject
	{
		public StoryEmitterStar(StageDataPackage StageData, PointF Position, float Velocity, double Direction)
			: base(StageData, null, Position, Velocity, Direction)
		{
			base.LifeTime = 30;
			base.Story.StoryEffectList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			StoryStar storyStar = new StoryStar(base.StageData, "Star", new PointF((float)base.Ran.Next((int)base.Position.X - 70, (int)base.Position.X + 70), base.Position.Y + 20f), (float)(base.StageData.Ran.Next(10, 20) / 10), -1.5707963267948966 - (double)((float)base.StageData.Ran.Next(1, 5) / 10f))
			{
				Active = true,
				Scale = (float)base.StageData.Ran.Next(15, 40) / 80f,
				LifeTime = 40
			};
			base.Story.StoryEffectList.Add(storyStar);
		}
	}
}
