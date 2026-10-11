using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Shooting
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class BaseObject
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002908 File Offset: 0x00000B08
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000291F File Offset: 0x00000B1F
		public int Time { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002928 File Offset: 0x00000B28
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000293F File Offset: 0x00000B3F
		public TextureObject TxtureObject { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002948 File Offset: 0x00000B48
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000295F File Offset: 0x00000B5F
		public bool Active { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002968 File Offset: 0x00000B68
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000297F File Offset: 0x00000B7F
		public PointF OriginalPosition { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002988 File Offset: 0x00000B88
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000029DC File Offset: 0x00000BDC
		public PointF Position
		{
			get
			{
				return new PointF(this.OriginalPosition.X + (float)this.BoundRect.Left, this.OriginalPosition.Y + (float)this.BoundRect.Top);
			}
			set
			{
				this.OriginalPosition = new PointF(value.X - (float)this.BoundRect.Left, value.Y - (float)this.BoundRect.Top);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002A24 File Offset: 0x00000C24
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002A3B File Offset: 0x00000C3B
		public PointF DestPoint { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002A44 File Offset: 0x00000C44
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002A5B File Offset: 0x00000C5B
		public bool Mirrored { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002A64 File Offset: 0x00000C64
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002A7C File Offset: 0x00000C7C
		public float Velocity
		{
			get
			{
				return this.velocity;
			}
			set
			{
				if (value > this.MaxVelocity)
				{
					this.velocity = this.MaxVelocity;
				}
				else if (value < this.MinVelocity)
				{
					this.velocity = this.minVelocity;
				}
				else
				{
					this.velocity = value;
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002AD4 File Offset: 0x00000CD4
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002AEC File Offset: 0x00000CEC
		public float MaxVelocity
		{
			get
			{
				return this.maxVelocity;
			}
			set
			{
				this.maxVelocity = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002AF8 File Offset: 0x00000CF8
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002B10 File Offset: 0x00000D10
		public float MinVelocity
		{
			get
			{
				return this.minVelocity;
			}
			set
			{
				this.minVelocity = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002B1C File Offset: 0x00000D1C
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002B44 File Offset: 0x00000D44
		public float Vx
		{
			get
			{
				return this.Velocity * (float)Math.Cos(this.Direction);
			}
			set
			{
				float vy = this.Vy;
				this.Velocity = (float)Math.Sqrt((double)(value * value + vy * vy));
				this.Direction = ((this.Velocity == 0f) ? 0.0 : Math.Atan2((double)vy, (double)value));
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002B98 File Offset: 0x00000D98
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public float Vy
		{
			get
			{
				return this.Velocity * (float)Math.Sin(this.Direction);
			}
			set
			{
				float vx = this.Vx;
				this.Velocity = (float)Math.Sqrt((double)(value * value + vx * vx));
				this.Direction = ((this.Velocity == 0f) ? 0.0 : Math.Atan2((double)value, (double)vx));
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002C14 File Offset: 0x00000E14
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002C2C File Offset: 0x00000E2C
		public double Angle
		{
			get
			{
				return this.angle;
			}
			set
			{
				while (value > 6.283185307179586)
				{
					value -= 6.283185307179586;
				}
				while (value < 0.0)
				{
					value += 6.283185307179586;
				}
				this.angle = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C84 File Offset: 0x00000E84
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public double AngleDegree
		{
			get
			{
				return this.Angle / 3.141592653589793 * 180.0;
			}
			set
			{
				this.Angle = value * 3.141592653589793 / 180.0;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002CD0 File Offset: 0x00000ED0
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public double Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				while (value > 6.283185307179586)
				{
					value -= 6.283185307179586;
				}
				while (value < 0.0)
				{
					value += 6.283185307179586;
				}
				this.direction = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002D40 File Offset: 0x00000F40
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002D6C File Offset: 0x00000F6C
		public double DirectionDegree
		{
			get
			{
				return this.Direction / 3.141592653589793 * 180.0;
			}
			set
			{
				this.Direction = value * 3.141592653589793 / 180.0;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002D8C File Offset: 0x00000F8C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public float Scale
		{
			get
			{
				return this.scaleLength;
			}
			set
			{
				if (value > this.MaxScale)
				{
					this.scaleLength = this.MaxScale;
					this.scaleWidth = this.MaxScale;
				}
				else if (value < this.MinScale)
				{
					this.scaleLength = 0f;
					this.scaleWidth = 0f;
				}
				else
				{
					this.scaleLength = value;
					this.scaleWidth = value;
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002E18 File Offset: 0x00001018
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002E30 File Offset: 0x00001030
		public float MaxScale
		{
			get
			{
				return this.maxScaleL;
			}
			set
			{
				this.maxScaleL = value;
				this.maxScaleW = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002E44 File Offset: 0x00001044
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002E5C File Offset: 0x0000105C
		public float MinScale
		{
			get
			{
				return this.minScaleL;
			}
			set
			{
				this.minScaleL = value;
				this.minScaleW = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002E70 File Offset: 0x00001070
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002E88 File Offset: 0x00001088
		public float MaxScaleL
		{
			get
			{
				return this.maxScaleL;
			}
			set
			{
				this.maxScaleL = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002E94 File Offset: 0x00001094
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002EAC File Offset: 0x000010AC
		public float MaxScaleW
		{
			get
			{
				return this.maxScaleW;
			}
			set
			{
				this.maxScaleW = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002EB8 File Offset: 0x000010B8
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002ED0 File Offset: 0x000010D0
		public float MinScaleL
		{
			get
			{
				return this.minScaleL;
			}
			set
			{
				this.minScaleL = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002EDC File Offset: 0x000010DC
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002EF4 File Offset: 0x000010F4
		public float MinScaleW
		{
			get
			{
				return this.minScaleW;
			}
			set
			{
				this.minScaleW = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002F00 File Offset: 0x00001100
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002F18 File Offset: 0x00001118
		public float ScaleX
		{
			get
			{
				return this.scaleX;
			}
			set
			{
				this.scaleX = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002F24 File Offset: 0x00001124
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002F3C File Offset: 0x0000113C
		public float ScaleY
		{
			get
			{
				return this.scaleY;
			}
			set
			{
				this.scaleY = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002F48 File Offset: 0x00001148
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002F60 File Offset: 0x00001160
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002F6C File Offset: 0x0000116C
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002F84 File Offset: 0x00001184
		public bool HitEnabled
		{
			get
			{
				return this.hitEnabled;
			}
			set
			{
				this.hitEnabled = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002F90 File Offset: 0x00001190
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002FA8 File Offset: 0x000011A8
		public bool ShootEnabled
		{
			get
			{
				return this.shootEnabled;
			}
			set
			{
				this.shootEnabled = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002FC4 File Offset: 0x000011C4
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002FB2 File Offset: 0x000011B2
		public int Region
		{
			get
			{
				return this.region;
			}
			set
			{
				this.region = value;
				this.outsideRegion = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002FDC File Offset: 0x000011DC
		public int OutsideRegion
		{
			get
			{
				return this.outsideRegion;
			}
			set
			{
				this.outsideRegion = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000300C File Offset: 0x0000120C
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003000 File Offset: 0x00001200
		public int Damage
		{
			get
			{
				return this.damage;
			}
			set
			{
				this.damage = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003024 File Offset: 0x00001224
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000303C File Offset: 0x0000123C
		public float HealthPoint
		{
			get
			{
				return this.healthPoint;
			}
			set
			{
				this.healthPoint = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003048 File Offset: 0x00001248
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003060 File Offset: 0x00001260
		public float ScaleLength
		{
			get
			{
				return this.scaleLength;
			}
			set
			{
				if (value > this.MaxScaleL)
				{
					this.scaleLength = this.MaxScaleL;
				}
				else if (value < this.MinScaleL)
				{
					this.scaleLength = this.MinScaleL;
				}
				else
				{
					this.scaleLength = value;
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000030B8 File Offset: 0x000012B8
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000030D0 File Offset: 0x000012D0
		public float ScaleWidth
		{
			get
			{
				return this.scaleWidth;
			}
			set
			{
				if (value > this.MaxScaleW)
				{
					this.scaleWidth = this.MaxScaleW;
				}
				else if (value < this.MinScaleW)
				{
					this.scaleWidth = this.MinScaleW;
				}
				else
				{
					this.scaleWidth = value;
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003128 File Offset: 0x00001328
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003140 File Offset: 0x00001340
		public int MaxTransparent
		{
			get
			{
				return this.maxTransparent;
			}
			set
			{
				if (value < 0)
				{
					this.maxTransparent = 0;
				}
				else if (value > 255)
				{
					this.maxTransparent = 255;
				}
				else
				{
					this.maxTransparent = value;
				}
				if (this.transparentValue > (float)this.maxTransparent)
				{
					this.transparentValue = (float)this.maxTransparent;
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000031B0 File Offset: 0x000013B0
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000031C8 File Offset: 0x000013C8
		public float TransparentValueF
		{
			get
			{
				return this.transparentValue;
			}
			set
			{
				if (value < 0f)
				{
					this.transparentValue = 0f;
				}
				else if (value > (float)this.MaxTransparent)
				{
					this.transparentValue = (float)this.MaxTransparent;
				}
				else
				{
					this.transparentValue = value;
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003220 File Offset: 0x00001420
		public int TransparentValue
		{
			get
			{
				return (int)this.transparentValue;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000323C File Offset: 0x0000143C
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003254 File Offset: 0x00001454
		public Color ColorValue
		{
			get
			{
				return this.colorValue;
			}
			set
			{
				this.colorValue = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003260 File Offset: 0x00001460
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003277 File Offset: 0x00001477
		public int LifeTime { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003280 File Offset: 0x00001480
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003297 File Offset: 0x00001497
		public float Accelerate { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000032A0 File Offset: 0x000014A0
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000032B7 File Offset: 0x000014B7
		public float Ax { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000032C0 File Offset: 0x000014C0
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000032D7 File Offset: 0x000014D7
		public float Ay { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000032E0 File Offset: 0x000014E0
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000032F7 File Offset: 0x000014F7
		public float AngularVelocityDegree { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003300 File Offset: 0x00001500
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003317 File Offset: 0x00001517
		public float DirectionVelocityDegree { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003320 File Offset: 0x00001520
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003338 File Offset: 0x00001538
		public float ScaleVelocity
		{
			get
			{
				return this.ScaleVL;
			}
			set
			{
				this.ScaleVL = value;
				this.ScaleVW = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000334C File Offset: 0x0000154C
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003363 File Offset: 0x00001563
		public float ScaleVL { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000336C File Offset: 0x0000156C
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003383 File Offset: 0x00001583
		public float ScaleVW { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000338C File Offset: 0x0000158C
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000033A3 File Offset: 0x000015A3
		public float TransparentVelocity { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000033AC File Offset: 0x000015AC
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000033C3 File Offset: 0x000015C3
		public bool UnRemoveable { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000033CC File Offset: 0x000015CC
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000033E3 File Offset: 0x000015E3
		public PointF[] LastPoints { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000033EC File Offset: 0x000015EC
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000341C File Offset: 0x0000161C
		public int GhostingCount
		{
			get
			{
				int num;
				if (this.LastPoints != null)
				{
					num = this.LastPoints.Length;
				}
				else
				{
					num = 0;
				}
				return num;
			}
			set
			{
				this.LastPoints = new PointF[value];
				for (int i = 0; i < value; i++)
				{
					this.LastPoints[i] = this.OriginalPosition;
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003460 File Offset: 0x00001660
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003477 File Offset: 0x00001677
		public Color GhostingColor { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003480 File Offset: 0x00001680
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003498 File Offset: 0x00001698
		public bool AngleWithDirection
		{
			get
			{
				return this.angleWithDirection;
			}
			set
			{
				this.angleWithDirection = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000034A4 File Offset: 0x000016A4
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000034BB File Offset: 0x000016BB
		public StageDataPackage StageData { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000034C4 File Offset: 0x000016C4
		public MySprite SpriteMain
		{
			get
			{
				return this.StageData.SpriteMain;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000034E4 File Offset: 0x000016E4
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003501 File Offset: 0x00001701
		public Rectangle BoundRect
		{
			get
			{
				return this.StageData.BoundRect;
			}
			set
			{
				this.StageData.BoundRect = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003510 File Offset: 0x00001710
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000352D File Offset: 0x0000172D
		public int TimeMain
		{
			get
			{
				return this.StageData.TimeMain;
			}
			set
			{
				this.StageData.TimeMain = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000353C File Offset: 0x0000173C
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003559 File Offset: 0x00001759
		public BaseMyPlane MyPlane
		{
			get
			{
				return this.StageData.MyPlane;
			}
			set
			{
				this.StageData.MyPlane = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003568 File Offset: 0x00001768
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003585 File Offset: 0x00001785
		public List<BaseBullet_Touhou> BulletList
		{
			get
			{
				return this.StageData.BulletList;
			}
			set
			{
				this.StageData.BulletList = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003594 File Offset: 0x00001794
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000035B1 File Offset: 0x000017B1
		public List<BaseObject> MyBulletList
		{
			get
			{
				return this.StageData.MyBulletList;
			}
			set
			{
				this.StageData.MyBulletList = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000035DD File Offset: 0x000017DD
		public List<BaseObject> SpellList
		{
			get
			{
				return this.StageData.SpellList;
			}
			set
			{
				this.StageData.SpellList = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000035EC File Offset: 0x000017EC
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003609 File Offset: 0x00001809
		public List<BaseEnemyPlane> EnemyPlaneList
		{
			get
			{
				return this.StageData.EnemyPlaneList;
			}
			set
			{
				this.StageData.EnemyPlaneList = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003618 File Offset: 0x00001818
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003635 File Offset: 0x00001835
		public List<BaseEffect> EffectList
		{
			get
			{
				return this.StageData.EffectList;
			}
			set
			{
				this.StageData.EffectList = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003644 File Offset: 0x00001844
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003661 File Offset: 0x00001861
		public List<BaseItem> ItemList
		{
			get
			{
				return this.StageData.ItemList;
			}
			set
			{
				this.StageData.ItemList = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003670 File Offset: 0x00001870
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000368D File Offset: 0x0000188D
		public BaseStory Story
		{
			get
			{
				return this.StageData.Story;
			}
			set
			{
				this.StageData.Story = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000369C File Offset: 0x0000189C
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000036B9 File Offset: 0x000018B9
		public BackgroundManager Background
		{
			get
			{
				return this.StageData.Background;
			}
			set
			{
				this.StageData.Background = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000036C8 File Offset: 0x000018C8
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000036E5 File Offset: 0x000018E5
		public BackgroundManager Background2
		{
			get
			{
				return this.StageData.Background2;
			}
			set
			{
				this.StageData.Background2 = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000036F4 File Offset: 0x000018F4
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003711 File Offset: 0x00001911
		public BackgroundManager3D Background3D
		{
			get
			{
				return this.StageData.Background3D;
			}
			set
			{
				this.StageData.Background3D = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003720 File Offset: 0x00001920
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000373D File Offset: 0x0000193D
		public BaseBossTouhou Boss
		{
			get
			{
				return this.StageData.Boss;
			}
			set
			{
				this.StageData.Boss = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000374C File Offset: 0x0000194C
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003769 File Offset: 0x00001969
		public List<XAudio2_Player> SoundPlayList
		{
			get
			{
				return this.StageData.SoundPlayList;
			}
			set
			{
				this.StageData.SoundPlayList = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003778 File Offset: 0x00001978
		public Dictionary<string, TextureObject> TextureObjectDictionary
		{
			get
			{
				return this.StageData.TextureObjectDictionary;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003798 File Offset: 0x00001998
		public Wave_Player BGM_Player
		{
			get
			{
				return this.StageData.BGM_Player;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000037B8 File Offset: 0x000019B8
		public KeyClass KClass
		{
			get
			{
				return this.StageData.KClass;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000037D8 File Offset: 0x000019D8
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000037F5 File Offset: 0x000019F5
		public MyRandom Ran
		{
			get
			{
				return this.StageData.Ran;
			}
			set
			{
				this.StageData.Ran = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003804 File Offset: 0x00001A04
		public DifficultLevel Difficulty
		{
			get
			{
				return this.StageData.Difficulty;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003824 File Offset: 0x00001A24
		public BaseObject()
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003988 File Offset: 0x00001B88
		public BaseObject(StageDataPackage StageData)
		{
			this.StageData = StageData;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public BaseObject(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
		{
			this.StageData = StageData;
			this.Position = Position;
			this.Velocity = Velocity;
			this.Direction = Direction;
			if (textureName != null)
			{
				this.TxtureObject = this.TextureObjectDictionary[textureName];
			}
			this.GhostingColor = Color.White;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public virtual void Move()
		{
			bool flag;
			if (this.DestPoint != new PointF(0f, 0f))
			{
				PointF destPoint = this.DestPoint;
				flag = 1 == 0;
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				this.MovePoint();
			}
			this.Position = new PointF((float)((double)this.Position.X + (double)this.Velocity * Math.Cos(this.Direction) * (double)this.scaleX), (float)((double)this.Position.Y + (double)this.Velocity * Math.Sin(this.Direction) * (double)this.scaleY));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003D50 File Offset: 0x00001F50
		public void MovePoint()
		{
			float num = (float)this.GetDistance(this.DestPoint);
			if (num > Math.Abs(this.Velocity))
			{
				this.Direction = this.GetDirection(this.DestPoint);
				this.Accelerate = -this.Velocity * this.Velocity / (2f * num);
				if (this.Velocity < 0f)
				{
					this.Velocity = 0f;
				}
			}
			else
			{
				this.Accelerate = 0f;
				this.Velocity = 0f;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public virtual bool OutBoundary()
		{
			bool flag;
			if (this.LifeTime != 0 && this.Time > this.LifeTime)
			{
				flag = true;
			}
			else if (this.Time < 100)
			{
				flag = false;
			}
			else
			{
				int num;
				if ((float)this.OutsideRegion * this.Scale * 2f < 20f)
				{
					num = 20;
				}
				else
				{
					num = (int)((float)this.OutsideRegion * this.Scale * 2f);
				}
				if ((float)(-(float)num) < this.OriginalPosition.X && this.OriginalPosition.X < (float)(this.BoundRect.Width + num) && (float)(-(float)num) < this.OriginalPosition.Y && this.OriginalPosition.Y < (float)(this.BoundRect.Height + num))
				{
					flag = false;
				}
				else
				{
					if (this.GhostingCount > 0)
					{
						if ((float)(-(float)num) < this.LastPoints[this.GhostingCount - 1].X && this.LastPoints[this.GhostingCount - 1].X < (float)(this.BoundRect.Width + num) && (float)(-(float)num) < this.LastPoints[this.GhostingCount - 1].Y && this.LastPoints[this.GhostingCount - 1].Y < (float)(this.BoundRect.Height + num))
						{
							return false;
						}
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FBC File Offset: 0x000021BC
		public virtual bool HitCheck(BaseObject Target)
		{
			return this.HitCheck(Target, (float)this.Region);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FDC File Offset: 0x000021DC
		public virtual bool HitCheck(BaseObject Target, float Radius)
		{
			bool flag;
			if (this.Time < 9)
			{
				flag = false;
			}
			else
			{
				if (this.ScaleLength == this.ScaleWidth)
				{
					if (this.GetDistance(Target) - (double)((float)Target.Region * Target.Scale) < (double)(Radius * this.Scale))
					{
						return true;
					}
				}
				else
				{
					double num = (this.AngleWithDirection ? (this.Direction + this.Angle) : this.Angle);
					double num2 = this.GetDistance(Target) + (double)((float)Target.Region * Target.Scale);
					double num3 = this.GetDirection(Target);
					double num4 = num2 * Math.Cos((double)((float)(num - num3)));
					double num5 = num2 * Math.Sin((double)((float)(num - num3)));
					double num6 = (double)(Radius * this.ScaleWidth);
					double num7 = (double)(Radius * this.ScaleLength);
					if (num5 * num5 / num6 / num6 + num4 * num4 / num7 / num7 < 1.0)
					{
						return true;
					}
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000040FF File Offset: 0x000022FF
		public virtual void HitCheckAll()
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004102 File Offset: 0x00002302
		public virtual void Shoot()
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004108 File Offset: 0x00002308
		public virtual void Ctrl()
		{
			if (this.Enabled)
			{
				this.Time++;
				this.Move();
				if (this.BindingObj != null)
				{
					if (this.LastBindingObj == null)
					{
						this.LastBindingObj = new BaseObject();
						this.LastBindingObj.StageData = this.StageData;
						this.LastBindingObj.CopyPosition(this.BindingObj);
					}
					double distance = this.GetDistance(this.LastBindingObj);
					double num = this.GetDirection(this.LastBindingObj) + 3.141592653589793 + this.BindingObj.Angle - this.LastBindingObj.Angle;
					this.Direction += this.BindingObj.Angle - this.LastBindingObj.Angle;
					if (this.BindingObj.AngleWithDirection)
					{
						num += this.BindingObj.Direction - this.LastBindingObj.Direction;
						this.Direction += this.BindingObj.Direction - this.LastBindingObj.Direction;
					}
					this.OriginalPosition = new PointF(this.BindingObj.OriginalPosition.X + (float)(distance * Math.Cos(num)), this.BindingObj.OriginalPosition.Y + (float)(distance * Math.Sin(num)));
					this.LastBindingObj.CopyPosition(this.BindingObj);
				}
				if (this.Ax != 0f || this.Ay != 0f)
				{
					float num2 = this.Velocity;
					double num = this.Direction;
					this.Velocity = (float)Math.Sqrt((double)(num2 * num2 + this.Ax * this.Ax + this.Ay * this.Ay) + (double)(2f * num2 * this.Ax) * Math.Cos(num) + (double)(2f * num2 * this.Ay) * Math.Sin(num));
					this.Direction = Math.Atan2((double)num2 * Math.Sin(num) + (double)this.Ay, (double)num2 * Math.Cos(num) + (double)this.Ax);
				}
				this.Velocity += this.Accelerate;
				this.AngleDegree += (double)this.AngularVelocityDegree;
				this.DirectionDegree += (double)this.DirectionVelocityDegree;
				this.TransparentValueF += this.TransparentVelocity;
				this.ScaleLength += this.ScaleVL;
				this.ScaleWidth += this.ScaleVW;
				if (this.AccelerateDictionary.ContainsKey(this.Time))
				{
					this.Accelerate = this.AccelerateDictionary[this.Time];
				}
				if (this.VelocityDictionary.ContainsKey(this.Time))
				{
					this.Velocity = this.VelocityDictionary[this.Time];
				}
				if (this.DirectionVelocityDegreeDictionary.ContainsKey(this.Time))
				{
					this.DirectionVelocityDegree = this.DirectionVelocityDegreeDictionary[this.Time];
				}
				if (this.DirectionDegreeDictionary.ContainsKey(this.Time))
				{
					this.DirectionDegree = (double)this.DirectionDegreeDictionary[this.Time];
				}
				if (this.DestPointDictionary.ContainsKey(this.Time))
				{
					this.DestPoint = this.DestPointDictionary[this.Time];
				}
				if (this.TransparentVelocityDictionary.ContainsKey(this.Time))
				{
					this.TransparentVelocity = this.TransparentVelocityDictionary[this.Time];
				}
				if (this.ScaleWidthVelocityDictionary.ContainsKey(this.Time))
				{
					this.ScaleVW = this.ScaleWidthVelocityDictionary[this.Time];
				}
				if (this.ScaleLengthVelocityDictionary.ContainsKey(this.Time))
				{
					this.ScaleVL = this.ScaleLengthVelocityDictionary[this.Time];
				}
				if (this.CtrlCall != null)
				{
					this.CtrlCall(null);
				}
				if (this.GhostingCount > 0)
				{
					for (int i = this.LastPoints.Length - 1; i > 0; i--)
					{
						this.LastPoints[i] = this.LastPoints[i - 1];
					}
					this.LastPoints[0] = this.OriginalPosition;
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004609 File Offset: 0x00002809
		public void SetBinding(BaseObject BindingObj)
		{
			this.BindingObj = BindingObj;
			this.Ctrl();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000461A File Offset: 0x0000281A
		public virtual void GiveEndEffect()
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000461D File Offset: 0x0000281D
		public virtual void GiveItems()
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004620 File Offset: 0x00002820
		public virtual void Show()
		{
			if (this.TxtureObject != null)
			{
				if (this.TransparentValue > 0)
				{
					double num = (this.AngleWithDirection ? (this.Direction + this.Angle - 1.5707963267948966) : (this.Angle - 1.5707963267948966));
					for (int i = this.GhostingCount - 1; i >= 0; i -= 2)
					{
						float num2 = 1f - 0.1f * (float)i / (float)this.LastPoints.Length;
						PointF pointF = new PointF(this.LastPoints[i].X + (float)this.BoundRect.Left, this.LastPoints[i].Y + (float)this.BoundRect.Top);
						this.SpriteMain.Draw2D(this.TxtureObject, this.ScaleWidth * num2, this.ScaleLength * num2, (float)num, pointF, Color.FromArgb(this.TransparentValue * (this.GhostingCount - i) / (this.GhostingCount + 1) / 4, this.GhostingColor), this.Mirrored);
					}
					this.SpriteMain.Draw2D(this.TxtureObject, this.ScaleWidth, this.ScaleLength, (float)num, this.Position, Color.FromArgb(this.TransparentValue, this.ColorValue), this.Mirrored);
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000047A4 File Offset: 0x000029A4
		public virtual void ShowRegion()
		{
			if (this.Region >= 0)
			{
				double num = (this.AngleWithDirection ? (this.Direction + this.Angle - 1.5707963267948966) : (this.Angle - 1.5707963267948966));
				this.SpriteMain.Draw2D(this.TextureObjectDictionary["Region"], 2f * (float)this.Region / (float)this.TextureObjectDictionary["Region"].Width * this.ScaleWidth, 2f * (float)this.Region / (float)this.TextureObjectDictionary["Region"].Height * this.ScaleLength, (float)num, this.Position, Color.White, this.Mirrored);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004880 File Offset: 0x00002A80
		public double GetDirection(BaseObject Target)
		{
			return this.GetDirection(Target.OriginalPosition);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000048A0 File Offset: 0x00002AA0
		public double GetDirection(PointF TargetOriginalPosition)
		{
			return 3.141592653589793 + Math.Atan2((double)(this.OriginalPosition.Y - TargetOriginalPosition.Y), (double)(this.OriginalPosition.X - TargetOriginalPosition.X));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000048F0 File Offset: 0x00002AF0
		public double GetDistance(BaseObject Target)
		{
			return this.GetDistance(Target.OriginalPosition);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004910 File Offset: 0x00002B10
		public double GetDistance(PointF TargetOriginalPosition)
		{
			return Math.Sqrt((double)((this.OriginalPosition.X - TargetOriginalPosition.X) * (this.OriginalPosition.X - TargetOriginalPosition.X) + (this.OriginalPosition.Y - TargetOriginalPosition.Y) * (this.OriginalPosition.Y - TargetOriginalPosition.Y)));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004984 File Offset: 0x00002B84
		public virtual void Copy(BaseObject b)
		{
			this.Time = b.Time;
			this.TxtureObject = b.TxtureObject;
			this.Active = b.Active;
			this.Position = b.Position;
			this.DestPoint = b.DestPoint;
			this.Mirrored = b.Mirrored;
			this.Velocity = b.Velocity;
			this.Angle = b.Angle;
			this.Direction = b.Direction;
			this.Scale = b.Scale;
			this.Enabled = b.Enabled;
			this.HitEnabled = b.HitEnabled;
			this.Region = b.Region;
			this.Damage = b.Damage;
			this.HealthPoint = b.HealthPoint;
			this.ScaleLength = b.ScaleLength;
			this.ScaleWidth = b.ScaleWidth;
			this.MaxTransparent = b.MaxTransparent;
			this.TransparentValueF = b.TransparentValueF;
			this.ColorValue = b.ColorValue;
			this.LifeTime = b.LifeTime;
			this.Accelerate = b.Accelerate;
			this.AngularVelocityDegree = b.AngularVelocityDegree;
			this.DirectionVelocityDegree = b.DirectionVelocityDegree;
			this.ScaleVelocity = b.ScaleVelocity;
			this.TransparentVelocity = b.TransparentVelocity;
			this.GhostingCount = b.GhostingCount;
			this.GhostingColor = b.GhostingColor;
			this.UnRemoveable = b.UnRemoveable;
			this.AngleWithDirection = b.AngleWithDirection;
			this.BindingObj = b.BindingObj;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004B24 File Offset: 0x00002D24
		public virtual void CopyPosition(BaseObject b)
		{
			this.Angle = b.Angle;
			this.Direction = b.Direction;
			this.Position = b.Position;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004B50 File Offset: 0x00002D50
		public object Clone(object Obj)
		{
			object obj;
			if (Obj == null)
			{
				obj = null;
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, Obj);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				object obj2 = binaryFormatter.Deserialize(memoryStream);
				memoryStream.Close();
				obj = obj2;
			}
			return obj;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public virtual object Clone()
		{
			BaseObject baseObject = (BaseObject)base.MemberwiseClone();
			baseObject.GhostingCount = this.GhostingCount;
			return baseObject;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public virtual void Dispose()
		{
		}

		// Token: 0x04000003 RID: 3
		private float velocity = 0f;

		// Token: 0x04000004 RID: 4
		private float maxVelocity = 100f;

		// Token: 0x04000005 RID: 5
		private float minVelocity = -100f;

		// Token: 0x04000006 RID: 6
		private double direction = 0.0;

		// Token: 0x04000007 RID: 7
		private bool enabled = true;

		// Token: 0x04000008 RID: 8
		private bool shootEnabled = true;

		// Token: 0x04000009 RID: 9
		private bool hitEnabled = true;

		// Token: 0x0400000A RID: 10
		private int region = 3;

		// Token: 0x0400000B RID: 11
		private int outsideRegion = 3;

		// Token: 0x0400000C RID: 12
		private int damage = 40;

		// Token: 0x0400000D RID: 13
		private float healthPoint = 1f;

		// Token: 0x0400000E RID: 14
		private float transparentValue = 255f;

		// Token: 0x0400000F RID: 15
		private int maxTransparent = 255;

		// Token: 0x04000010 RID: 16
		private Color colorValue = Color.White;

		// Token: 0x04000011 RID: 17
		private double angle = 0.0;

		// Token: 0x04000012 RID: 18
		private float scaleLength = 1f;

		// Token: 0x04000013 RID: 19
		private float scaleWidth = 1f;

		// Token: 0x04000014 RID: 20
		private float maxScaleL = 100f;

		// Token: 0x04000015 RID: 21
		private float maxScaleW = 100f;

		// Token: 0x04000016 RID: 22
		private float minScaleL = 0.0001f;

		// Token: 0x04000017 RID: 23
		private float minScaleW = 0.0001f;

		// Token: 0x04000018 RID: 24
		private bool angleWithDirection = true;

		// Token: 0x04000019 RID: 25
		private float scaleX = 1f;

		// Token: 0x0400001A RID: 26
		private float scaleY = 1f;

		// Token: 0x0400001B RID: 27
		protected BaseObject BindingObj;

		// Token: 0x0400001C RID: 28
		private BaseObject LastBindingObj;

		// Token: 0x0400001D RID: 29
		public Dictionary<int, float> AccelerateDictionary = new Dictionary<int, float>();

		// Token: 0x0400001E RID: 30
		public Dictionary<int, float> VelocityDictionary = new Dictionary<int, float>();

		// Token: 0x0400001F RID: 31
		public Dictionary<int, float> DirectionVelocityDegreeDictionary = new Dictionary<int, float>();

		// Token: 0x04000020 RID: 32
		public Dictionary<int, float> DirectionDegreeDictionary = new Dictionary<int, float>();

		// Token: 0x04000021 RID: 33
		public Dictionary<int, PointF> DestPointDictionary = new Dictionary<int, PointF>();

		// Token: 0x04000022 RID: 34
		public Dictionary<int, float> TransparentVelocityDictionary = new Dictionary<int, float>();

		// Token: 0x04000023 RID: 35
		public Dictionary<int, float> ScaleWidthVelocityDictionary = new Dictionary<int, float>();

		// Token: 0x04000024 RID: 36
		public Dictionary<int, float> ScaleLengthVelocityDictionary = new Dictionary<int, float>();

		// Token: 0x04000025 RID: 37
		public BaseObject.CtrlActionCallBack CtrlCall;

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x060000BC RID: 188
		public delegate void CtrlActionCallBack(string s);
	}
}
