using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000B4 RID: 180
	internal class Emitter2 : BaseEffect
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x00029DAE File Offset: 0x00027FAE
		public Emitter2(StageDataPackage StageData, PointF Position)
			: base(StageData)
		{
			base.Position = Position;
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.LifeTime = 2;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00029DE8 File Offset: 0x00027FE8
		public override void Shoot()
		{
			int i = 0;
			while (i < 1)
			{
				string text = ((base.MyPlane.Name == "Aya") ? "MyParticle" : "MyParticle01");
				string name = base.MyPlane.Name;
				if (name == null)
				{
					goto IL_00A7;
				}
				string text2;
				if (!(name == "Aya"))
				{
					if (!(name == "Reimu"))
					{
						if (!(name == "Marisa"))
						{
							if (!(name == "Sanae"))
							{
								if (!(name == "Koishi"))
								{
									goto IL_00A7;
								}
								text2 = "Light05";
							}
							else
							{
								text2 = "Light04";
							}
						}
						else
						{
							text2 = "Light03";
						}
					}
					else
					{
						text2 = "Light01";
					}
				}
				else
				{
					text2 = "MyParticle";
				}
				IL_00AF:
				ParticleSmaller particleSmaller = new ParticleSmaller(base.StageData, text2, base.Position, (float)(base.Ran.Next(100, 300) / 10), (double)((float)base.Ran.Next(0, 180) / 180f) * 3.141592653589793);
				particleSmaller.LifeTime = 10;
				particleSmaller.Scale = (float)base.Ran.Next(60) / 100f;
				particleSmaller.TransparentValueF = 200f;
				particleSmaller.Accelerate = -particleSmaller.Velocity / (float)particleSmaller.LifeTime;
				i++;
				continue;
				IL_00A7:
				text2 = "MyParticle01";
				goto IL_00AF;
			}
		}
	}
}
