using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000069 RID: 105
	internal class ParticleBack : BaseEffect
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0001A2CC File Offset: 0x000184CC
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0001A2E3 File Offset: 0x000184E3
		public float AngularVelocity { get; set; }

		// Token: 0x060003C5 RID: 965 RVA: 0x0001A2EC File Offset: 0x000184EC
		public ParticleBack(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Diretion)
		{
			base.StageData = StageData;
			base.Position = Position;
			base.Velocity = Velocity;
			base.Direction = Diretion;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.LifeTime = 200;
			base.TransparentValueF = 0f;
			this.AngularVelocity = 0.01f;
			base.EffectList.Add(this);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001A368 File Offset: 0x00018568
		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle += (double)this.AngularVelocity;
			if (0 < base.Time && base.Time < base.LifeTime / 3)
			{
				base.TransparentValueF += (float)(765 / base.LifeTime);
			}
			else if (base.LifeTime * 2 / 3 < base.Time && base.Time < base.LifeTime)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime * 3);
			}
		}
	}
}
