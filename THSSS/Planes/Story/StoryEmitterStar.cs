using System.Drawing;

namespace Shooting.Planes.Story
{
	internal class StoryEmitterStar : BaseObject
	{
		public StoryEmitterStar(StageDataPackage StageData, PointF Position, float Velocity, double Direction)
			: base(StageData, null, Position, Velocity, Direction)
		{
			LifeTime = 30;
			Story.StoryEffectList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			StoryStar storyStar = new StoryStar(StageData, "Star", new PointF((float)Ran.Next((int)Position.X - 70, (int)Position.X + 70), Position.Y + 20f), (float)(StageData.Ran.Next(10, 20) / 10), -1.5707963267948966 - (double)((float)StageData.Ran.Next(1, 5) / 10f))
			{
				Active = true,
				Scale = (float)StageData.Ran.Next(15, 40) / 80f,
				LifeTime = 40
			};
			Story.StoryEffectList.Add(storyStar);
		}
	}
}
