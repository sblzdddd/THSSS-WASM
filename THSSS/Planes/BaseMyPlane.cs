using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	public class BaseMyPlane : BaseObject
	{
		public string Name { get; set; }

		public string WeaponType { get; set; }

		public string FullName
		{
			get
			{
				return this.Name + this.WeaponType;
			}
		}

		public bool SlowMove { get; private set; }

		public float HighSpeed { get; set; }

		public float LowSpeed { get; set; }

		public new float Vx { get; private set; }

		public new float Vy { get; private set; }

		public int DeadTime { get; set; }

		public int Life
		{
			get
			{
				return this.life;
			}
			set
			{
				if (value > 8)
				{
					this.life = 8;
				}
				else
				{
					this.life = value;
				}
			}
		}

		public int Spell
		{
			get
			{
				return this.spell;
			}
			set
			{
				if (value > 8)
				{
					this.spell = 8;
				}
				else if (value < 0)
				{
					this.spell = 0;
				}
				else
				{
					this.spell = value;
				}
			}
		}

		public int LifeChip
		{
			get
			{
				return this.lifeChip;
			}
			set
			{
				this.lifeChip = value;
				while (this.lifeChip >= this.nextLifeChip[this.lifeUpCount])
				{
					this.Extend();
					this.lifeChip -= this.nextLifeChip[this.lifeUpCount];
					this.lifeUpCount++;
				}
			}
		}

		public int SpellChip
		{
			get
			{
				return this.spellChip;
			}
			set
			{
				this.spellChip = value;
				while (this.spellChip >= this.nextSpellChip)
				{
					this.SpellExtand();
					this.spellChip -= this.nextSpellChip;
				}
			}
		}

		public int Power
		{
			get
			{
				return this.power;
			}
			set
			{
				if (value > 400)
				{
					this.power = 400;
				}
				else if (value < 100)
				{
					this.power = 100;
				}
				else
				{
					this.power = value;
				}
			}
		}

		public int PowerLevel
		{
			get
			{
				return this.power / 100;
			}
		}

		public long Score
		{
			get
			{
				return this.score;
			}
			set
			{
				if (value > 9999999999L)
				{
					this.score = 9999999999L;
				}
				else if (value < 0L)
				{
					this.score = 0L;
				}
				else
				{
					this.score = value;
				}
			}
		}

		public long HiScore
		{
			get
			{
				long num;
				if (this.score > this.hiScore)
				{
					num = this.score;
				}
				else
				{
					num = this.hiScore;
				}
				return num;
			}
			set
			{
				if (value > 9999999999L)
				{
					this.hiScore = 9999999999L;
				}
				else if (value < 0L)
				{
					this.hiScore = 0L;
				}
				else
				{
					this.hiScore = value;
				}
			}
		}

		public int BluePoint
		{
			get
			{
				return this.bluePoint;
			}
			set
			{
				this.bluePoint = value;
			}
		}

		public int GreenPoint
		{
			get
			{
				return this.greenPoint;
			}
			set
			{
				this.greenPoint = value;
			}
		}

		public int NextBluePoint
		{
			get
			{
				return this.nextBluePoint;
			}
			set
			{
				this.nextBluePoint = value;
			}
		}

		public int NextGreenPoint
		{
			get
			{
				return this.nextGreenPoint;
			}
			set
			{
				this.nextGreenPoint = value;
			}
		}

		public int StarPoint
		{
			get
			{
				return this.starPoint;
			}
			set
			{
				if (value >= this.maxStarPoint)
				{
					if (this.starPoint < this.maxStarPoint)
					{
						if (this.LastColor == EnchantmentType.Red)
						{
							this.ChangeEnchantment(EnchantmentType.Red);
						}
						else if (this.LastColor == EnchantmentType.Blue)
						{
							this.ChangeEnchantment(EnchantmentType.Blue);
						}
						else if (this.LastColor == EnchantmentType.Green)
						{
							this.ChangeEnchantment(EnchantmentType.Green);
						}
						base.StageData.SoundPlay("se_focusrot.wav");
						BaseBackground baseBackground = new BaseBackground(base.StageData, "pl02b", base.Position, 0f, 0.0);
						baseBackground.ScaleVelocity = 1f;
						baseBackground.LifeTime = 26;
						baseBackground.TransparentVelocity = -10f;
						baseBackground.Active = false;
						baseBackground.ColorValue = Color.Black;
						BaseBackground baseBackground2 = new BaseBackground(base.StageData, "pl02b", base.Position, 0f, 0.0);
						baseBackground2.ScaleVelocity = 1f;
						baseBackground2.LifeTime = 26;
						baseBackground2.TransparentVelocity = -10f;
						baseBackground2.Active = false;
						baseBackground2.ColorValue = Color.Black;
						BaseBackground baseBackground3 = new BaseBackground(base.StageData, "pl02b", base.Position, 0f, 0.0);
						baseBackground3.ScaleVelocity = 1f;
						baseBackground3.LifeTime = 26;
						baseBackground3.TransparentVelocity = -10f;
						baseBackground3.Active = false;
						baseBackground3.ColorValue = Color.Black;
						new EnchantmentMagicCircle(base.StageData, base.OriginalPosition);
					}
					this.starPoint = this.maxStarPoint;
				}
				else if (value < 0)
				{
					this.starPoint = 0;
				}
				else
				{
					this.starPoint = value;
				}
			}
		}

		public EnchantmentType EnchantmentState
		{
			get
			{
				return this.enchantmentState;
			}
			set
			{
				this.enchantmentState = value;
			}
		}

		public EnchantmentType LastColor
		{
			get
			{
				return this.lastColor;
			}
			set
			{
				this.lastColor = value;
			}
		}

		public int EnchantmentTime
		{
			get
			{
				return this.enchantmentTime;
			}
			set
			{
				if (value > 0)
				{
					this.enchantmentTime = value;
				}
				else
				{
					if (this.enchantmentTime > 0)
					{
						if (this.enchantmentTime == 1)
						{
							this.EnchantmentCount++;
							this.Rate += 0.1f;
							long num = 0L;
							if (this.enchantmentState == EnchantmentType.Blue)
							{
								num = (long)(this.HighItemScore * 20);
							}
							else if (this.enchantmentState == EnchantmentType.Green)
							{
								num = 1000000L;
							}
							else if (this.enchantmentState == EnchantmentType.Red)
							{
								num = 5120000L;
							}
							this.score += num;
							new EnchantmentBonus(base.StageData, num);
							base.StageData.SoundPlay("se_cardget.wav");
						}
						else
						{
							BaseEffect baseEffect = new BaseEffect(base.StageData, "pl02b", base.Position, 0f, 0.0);
							baseEffect.ScaleVelocity = 1f;
							baseEffect.LifeTime = 26;
							baseEffect.TransparentVelocity = -10f;
							baseEffect.Active = false;
							baseEffect.ColorValue = Color.Black;
							BaseEffect baseEffect2 = new BaseEffect(base.StageData, "pl02b", base.Position, 0f, 0.0);
							baseEffect2.ScaleVelocity = 1f;
							baseEffect2.LifeTime = 26;
							baseEffect2.TransparentVelocity = -10f;
							baseEffect2.Active = false;
							baseEffect2.ColorValue = Color.Black;
							BaseEffect baseEffect3 = new BaseEffect(base.StageData, "pl02b", base.Position, 0f, 0.0);
							baseEffect3.ScaleVelocity = 1f;
							baseEffect3.LifeTime = 26;
							baseEffect3.TransparentVelocity = -10f;
							baseEffect3.Active = false;
							baseEffect3.ColorValue = Color.Black;
						}
						if (this.enchantmentState == EnchantmentType.Green)
						{
							base.Time = this.UnmatchedTime - 60;
							this.DeadTime = -1;
							new SpellDisable(base.StageData);
						}
						this.StarPoint = 0;
						base.StageData.SoundPlay("ice005.wav");
						TextureObject textureObject = base.TextureObjectDictionary["White"];
						int num2 = 16;
						int num3 = 8;
						Color color = Color.White;
						if (this.enchantmentState == EnchantmentType.Blue)
						{
							color = Color.Blue;
						}
						else if (this.enchantmentState == EnchantmentType.Green)
						{
							color = Color.Green;
						}
						else if (this.enchantmentState == EnchantmentType.Red)
						{
							color = Color.Red;
						}
						for (int i = 0; i < num2; i++)
						{
							for (int j = 0; j < num3; j++)
							{
								Model_Triangle model_Triangle = new Model_Triangle(base.StageData.DeviceMain, textureObject, i.ToString() + "_" + j.ToString(), new Vector2((float)i / (float)num2, (float)j / (float)num3), new Vector2((float)(i + 1) / (float)num2, (float)j / (float)num3), new Vector2((float)i / (float)num2, (float)(j + 1) / (float)num3), 640 / num2, 480 / num3);
								BaseParticle3D baseParticle3D = new BaseParticle3D();
								baseParticle3D.StageData = base.StageData;
								baseParticle3D.Position = new PointF((float)(i * 640 / num2), (float)(j * 480 / num3));
								baseParticle3D.model = model_Triangle;
								baseParticle3D.Velocity = (float)base.Ran.Next(20) / 10f;
								baseParticle3D.LifeTime = 30;
								baseParticle3D.DirectionDegree = (double)base.Ran.Next(360);
								baseParticle3D.RotatingAxis = new Vector3((float)base.Ran.Next(-100, 100), (float)base.Ran.Next(-100, 100), (float)base.Ran.Next(-100, 100));
								baseParticle3D.AngularVelocity3D = (float)base.Ran.Next(20) / 100f;
								baseParticle3D.TransparentVelocity = -6f;
								baseParticle3D.ScaleVelocity = -0.005f;
								baseParticle3D.ColorValue = color;
								baseParticle3D.TransparentValueF = 150f;
								base.StageData.Particle3D.ParticleList.Add(baseParticle3D);
							}
						}
						for (int i = 0; i < num2; i++)
						{
							for (int j = 0; j < num3; j++)
							{
								Model_Triangle model_Triangle = new Model_Triangle(base.StageData.DeviceMain, textureObject, i.ToString() + "_" + j.ToString(), new Vector2((float)(i + 1) / (float)num2, (float)(j + 1) / (float)num3), new Vector2((float)i / (float)num2, (float)(j + 1) / (float)num3), new Vector2((float)(i + 1) / (float)num2, (float)j / (float)num3), 640 / num2, 480 / num3);
								BaseParticle3D baseParticle3D = new BaseParticle3D();
								baseParticle3D.StageData = base.StageData;
								baseParticle3D.Position = new PointF((float)((i + 1) * 640 / num2), (float)((j + 1) * 480 / num3));
								baseParticle3D.model = model_Triangle;
								baseParticle3D.Angle = 3.141592653589793;
								baseParticle3D.Velocity = (float)base.Ran.Next(20) / 10f;
								baseParticle3D.LifeTime = 30;
								baseParticle3D.DirectionDegree = (double)base.Ran.Next(360);
								baseParticle3D.RotatingAxis = new Vector3((float)base.Ran.Next(-100, 100), (float)base.Ran.Next(-100, 100), (float)base.Ran.Next(-100, 100));
								baseParticle3D.AngularVelocity3D = (float)base.Ran.Next(20) / 100f;
								baseParticle3D.TransparentVelocity = -6f;
								baseParticle3D.ScaleVelocity = -0.005f;
								baseParticle3D.ColorValue = color;
								baseParticle3D.TransparentValueF = 150f;
								base.StageData.Particle3D.ParticleList.Add(baseParticle3D);
							}
						}
					}
					this.enchantmentTime = 0;
					this.enchantmentState = EnchantmentType.None;
				}
			}
		}

		public int EnchantmentCount
		{
			get
			{
				return this.enchantmentCount;
			}
			set
			{
				this.enchantmentCount = value;
			}
		}

		public int EnchantmentCountNeeded
		{
			get
			{
				return this.enchantmentCountNeeded;
			}
			set
			{
				this.enchantmentCountNeeded = value;
			}
		}

		public bool FscEnabled
		{
			get
			{
				return this.EnchantmentCount >= this.EnchantmentCountNeeded && base.Difficulty > DifficultLevel.Normal;
			}
		}

		public float Rate
		{
			get
			{
				return this.rate;
			}
			set
			{
				this.rate = value;
			}
		}

		public int HighItemScore { get; set; }

		public int Graze { get; set; }

		public int UnmatchedTime
		{
			get
			{
				return 300;
			}
		}

		public TextureObject[] TxtureObjects { get; set; }

		public PointF[] SubPlanePoint { get; set; }

		public List<BaseSubPlane> SubPlaneList { get; set; }

		public bool SpellEnabled { get; set; }

		public int LifeUpCount
		{
			get
			{
				return this.lifeUpCount;
			}
			set
			{
				this.lifeUpCount = value;
			}
		}

		public BaseMyPlane()
		{
		}

		public BaseMyPlane(StageDataPackage StageData, Point Position)
		{
			base.StageData = StageData;
			base.Position = Position;
			this.TxtureObjects = new TextureObject[72];
			for (int i = 0; i < 72; i++)
			{
				this.TxtureObjects[i] = base.TextureObjectDictionary["MyPlane" + i.ToString("D3")];
			}
			base.TxtureObject = this.TxtureObjects[0];
			this.SubPlanePoint = new PointF[4];
			this.HighSpeed = 4.5f;
			this.LowSpeed = 2f;
			base.Region = 1;
			base.Time = this.UnmatchedTime;
			this.DeadTime = -1;
			this.SubPlaneList = new List<BaseSubPlane>
			{
				new BaseSubPlane(StageData, "MySubPlane", Position),
				new BaseSubPlane(StageData, "MySubPlane", Position),
				new BaseSubPlane(StageData, "MySubPlane", Position),
				new BaseSubPlane(StageData, "MySubPlane", Position)
			};
			this.Name = "Plane";
			this.WeaponType = "A";
			this.SpellEnabled = true;
			this.HighItemScore = 100000;
		}

		public virtual void Refresh()
		{
			base.Time = 300;
			this.EnchantmentCount = 0;
			this.SubPlaneList.ForEach(delegate(BaseSubPlane x)
			{
				x.Refresh();
			});
			this.SpellEnabled = true;
		}

		public override void Move()
		{
			this.Vx = 0f;
			this.Vy = 0f;
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			this.SlowMove = base.KClass.Key_Shift;
			if (!this.SlowMove)
			{
				if (base.KClass.ArrowRight)
				{
					this.Vx += this.HighSpeed;
				}
				if (base.KClass.ArrowLeft)
				{
					this.Vx -= this.HighSpeed;
				}
				if (base.KClass.ArrowUp)
				{
					this.Vy += this.HighSpeed;
				}
				if (base.KClass.ArrowDown)
				{
					this.Vy -= this.HighSpeed;
				}
			}
			else
			{
				if (base.KClass.ArrowRight)
				{
					this.Vx += this.LowSpeed;
				}
				if (base.KClass.ArrowLeft)
				{
					this.Vx -= this.LowSpeed;
				}
				if (base.KClass.ArrowUp)
				{
					this.Vy += this.LowSpeed;
				}
				if (base.KClass.ArrowDown)
				{
					this.Vy -= this.LowSpeed;
				}
			}
			if (this.Vx != 0f && this.Vy != 0f)
			{
				this.Vx = 0.71f * this.Vx;
				this.Vy = 0.71f * this.Vy;
			}
			num += this.Vx;
			num2 -= this.Vy;
			if (num < (float)(base.TxtureObject.Width / 2 - 6))
			{
				num = (float)(base.TxtureObject.Width / 2 - 6);
			}
			else if (num > (float)(base.BoundRect.Width - base.TxtureObject.Width / 2 + 6))
			{
				num = (float)(base.BoundRect.Width - base.TxtureObject.Width / 2 + 6);
			}
			if (num2 < (float)(base.TxtureObject.Height / 2))
			{
				num2 = (float)(base.TxtureObject.Height / 2);
			}
			else if (num2 >= (float)(base.BoundRect.Height - base.TxtureObject.Height / 2 + 8))
			{
				num2 = (float)(base.BoundRect.Height - base.TxtureObject.Height / 2 + 8);
			}
			base.OriginalPosition = new PointF(num, num2);
		}

		public virtual void SubPlaneCtrl()
		{
			if (base.KClass.Key_Shift)
			{
				switch (this.PowerLevel)
				{
				case 1:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 30f);
					break;
				case 2:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 15f, base.OriginalPosition.Y - 30f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 15f, base.OriginalPosition.Y - 30f);
					break;
				case 3:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 30f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 20f, base.OriginalPosition.Y - 20f);
					this.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 20f, base.OriginalPosition.Y - 20f);
					break;
				case 4:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 15f, base.OriginalPosition.Y - 40f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 15f, base.OriginalPosition.Y - 40f);
					this.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 30f, base.OriginalPosition.Y - 30f);
					this.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 30f, base.OriginalPosition.Y - 30f);
					break;
				}
			}
			else
			{
				switch (this.PowerLevel)
				{
				case 1:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 40f);
					break;
				case 2:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 25f, base.OriginalPosition.Y - 20f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 25f, base.OriginalPosition.Y - 20f);
					break;
				case 3:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 40f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 30f, base.OriginalPosition.Y - 20f);
					this.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 30f, base.OriginalPosition.Y - 20f);
					break;
				case 4:
					this.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 25f, base.OriginalPosition.Y - 0f);
					this.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 25f, base.OriginalPosition.Y - 0f);
					this.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 50f, base.OriginalPosition.Y + 5f);
					this.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 50f, base.OriginalPosition.Y + 5f);
					break;
				}
			}
			for (int i = 0; i < this.PowerLevel; i++)
			{
				this.SubPlaneList[i].Enabled = true;
				this.SubPlaneList[i].DestPoint = this.SubPlanePoint[i];
				this.SubPlaneList[i].Ctrl();
			}
			for (int i = this.PowerLevel; i < this.SubPlaneList.Count; i++)
			{
				this.SubPlaneList[i].Enabled = false;
				this.SubPlaneList[i].Position = base.Position;
			}
		}

		public virtual void TextureCtrl()
		{
			if (this.Vx > 0.5f)
			{
				if (this.SlowMove)
				{
					this.OnRotate = false;
					if (0 <= this.Angle3D && this.Angle3D < 30)
					{
						this.Angle3D += 5;
					}
					else if (this.Angle3D < 0)
					{
						this.Angle3D = 0;
					}
				}
				else if (!this.OnRotate)
				{
					if (0 <= this.Angle3D && this.Angle3D < 40)
					{
						this.Angle3D += 5;
					}
					else if (this.Angle3D < 0)
					{
						this.OnRotate = true;
					}
				}
				else
				{
					this.Angle3D += 13;
				}
			}
			else if (this.Vx < -0.5f)
			{
				if (this.SlowMove)
				{
					this.OnRotate = false;
					if (-30 < this.Angle3D && this.Angle3D <= 0)
					{
						this.Angle3D -= 5;
					}
					else if (this.Angle3D > 0)
					{
						this.Angle3D = 0;
					}
				}
				else if (!this.OnRotate)
				{
					if (-40 < this.Angle3D && this.Angle3D <= 0)
					{
						this.Angle3D -= 5;
					}
					else if (this.Angle3D > 0)
					{
						this.OnRotate = true;
					}
				}
				else
				{
					this.Angle3D -= 13;
				}
			}
			else
			{
				this.Angle3D /= 2;
				if (-10 < this.Angle3D && this.Angle3D < 10)
				{
					this.Angle3D = 0;
					this.OnRotate = false;
				}
			}
			if (this.Angle3D > 180)
			{
				this.Angle3D -= 360;
			}
			else if (this.Angle3D < -180)
			{
				this.Angle3D += 360;
			}
			int num = this.Angle3D / 5;
			if (num < 0)
			{
				num += 72;
			}
			else if (num > 72)
			{
				num -= 72;
			}
			base.TxtureObject = this.TxtureObjects[num];
		}

		public override void Shoot()
		{
			if (base.Time % 3 == 0)
			{
				base.StageData.SoundPlay("se_plst00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				float x = base.Position.X;
				float y = base.Position.Y;
				float num = 7f;
				num *= (float)Math.Cos((double)this.Angle3D * 3.141592653589793 / 180.0);
				new BaseMyBullet(base.StageData, "自机子弹", new PointF(x - num, y), 30f, -1.5707963267948966);
				new BaseMyBullet(base.StageData, "自机子弹", new PointF(x + num, y), 30f, -1.5707963267948966);
			}
			if (base.Time % 5 == 0)
			{
				for (int i = 0; i < this.PowerLevel; i++)
				{
					this.SubPlaneList[i].Shoot();
				}
			}
		}

		public virtual void SpellShoot()
		{
			new Spell(base.StageData);
			base.StageData.SoundPlay("se_nep00.wav");
		}

		public virtual void PreMiss()
		{
			if (base.Time >= this.UnmatchedTime)
			{
				if (base.Time >= this.DeadTime)
				{
					if (base.SpellList.Count == 0)
					{
						if (this.EnchantmentState == EnchantmentType.Green)
						{
							BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(base.StageData, base.OriginalPosition);
							bulletRemover_Small.Region = 500;
							this.ChangeEnchantment(EnchantmentType.None);
							base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
							{
								if (x is BaseSpellCard)
								{
									((BaseSpellCard)x).Missed = true;
								}
							});
						}
						else
						{
							new Emitter(base.StageData, base.MyPlane.Position);
							base.StageData.SoundPlay("se_pldead00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
							this.DeadTime = base.Time + 20;
						}
					}
				}
			}
		}

		public virtual void Miss()
		{
			if (this.EnchantmentState == EnchantmentType.Red || this.EnchantmentState == EnchantmentType.Blue)
			{
				this.ChangeEnchantment(EnchantmentType.None);
			}
			new BulletRemover(base.StageData, base.OriginalPosition);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height + 100));
			this.Vx = 0f;
			this.Vy = 0f;
			this.Life--;
			this.Power -= 16;
			base.Time = 0;
			this.DeadTime = -1;
		}

		public override void Ctrl()
		{
			if (base.Enabled)
			{
				base.Time++;
				if (base.Time == this.DeadTime)
				{
					this.Miss();
				}
				else if (base.Time > this.DeadTime)
				{
					if (base.Time == 1)
					{
						if (this.Spell < 2)
						{
							this.Spell = 2;
						}
					}
					if (base.Time > 70)
					{
						this.Move();
					}
					else
					{
						base.DestPoint = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height - 30));
						base.Velocity = 2f;
						base.Move();
						this.TextureCtrl();
					}
					this.TextureCtrl();
					base.Direction += 0.019999999552965164;
					this.SubPlaneCtrl();
					this.EnchantmentTime--;
					if (this.EnchantmentState == EnchantmentType.Blue)
					{
						base.ItemList.ForEach(delegate(BaseItem x)
						{
							if (!(x is ShootingStarShard))
							{
								x.Obtain = true;
								x.Doubled = true;
							}
						});
					}
				}
				if (base.Story == null && base.Time > 70)
				{
					if (base.KClass.Key_Z)
					{
						if (base.TimeMain > 15)
						{
							this.Shoot();
						}
					}
					if (base.KClass.Key_X && !this.lastX)
					{
						if (this.EnchantmentState == EnchantmentType.Green)
						{
							BulletRemover_Small bulletRemover_Small = new BulletRemover_Small(base.StageData, base.OriginalPosition);
							bulletRemover_Small.Region = 500;
							this.ChangeEnchantment(EnchantmentType.None);
							base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
							{
								if (x is BaseSpellCard)
								{
									((BaseSpellCard)x).Bombed = true;
								}
							});
						}
						else if (this.Spell > 0 && base.SpellList.Count == 0 && base.Time > 30 && this.SpellEnabled)
						{
							this.SpellShoot();
							this.Spell--;
						}
					}
					this.lastX = base.KClass.Key_X;
				}
			}
		}

		public void RetryClear()
		{
			this.EnchantmentTime = 0;
			this.Life = 2;
			this.Spell = 2;
			this.Power = 400;
			this.Score = (long)base.StageData.ContinueTimes;
			this.Graze = 0;
			this.HiScore = base.StageData.S_History[0].Score;
			this.LifeChip = 0;
			this.SpellChip = 0;
			this.LifeUpCount = 0;
			this.StarPoint = 0;
			this.HighItemScore = 100000;
			this.Rate = 0f;
			this.LastColor = EnchantmentType.Green;
		}

		public void Extend()
		{
			if (this.Life < 8)
			{
				this.Life++;
			}
			else
			{
				this.Spell++;
			}
			base.StageData.SoundPlay("se_extend.wav");
			BaseEffect baseEffect = new BaseEffect(base.StageData, "Extend", new PointF(0f, 0f), 0f, 1.5707963267948966)
			{
				Active = false,
				OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 112f),
				LifeTime = 90,
				TransparentValueF = 0f,
				Layer = 1
			};
			baseEffect.TransparentVelocityDictionary.Add(1, 13f);
			baseEffect.TransparentVelocityDictionary.Add(70, -13f);
		}

		public void SpellExtand()
		{
			base.StageData.SoundPlay("se_cardget.wav");
			this.Spell++;
			BaseEffect baseEffect = new BaseEffect(base.StageData, "SpellExtend", new PointF(0f, 0f), 0f, 1.5707963267948966)
			{
				Active = false,
				OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 80f),
				LifeTime = 90,
				TransparentValueF = 0f,
				Layer = 1
			};
			baseEffect.TransparentVelocityDictionary.Add(1, 13f);
			baseEffect.TransparentVelocityDictionary.Add(70, -13f);
		}

		public void ChangeEnchantment(EnchantmentType EnchantmentState)
		{
			switch (EnchantmentState)
			{
			case EnchantmentType.None:
				this.EnchantmentTime = 0;
				break;
			case EnchantmentType.Red:
				this.EnchantmentState = EnchantmentState;
				this.EnchantmentTime = 600;
				break;
			case EnchantmentType.Blue:
				this.EnchantmentState = EnchantmentState;
				this.EnchantmentTime = 600;
				break;
			case EnchantmentType.Green:
				this.EnchantmentState = EnchantmentState;
				this.EnchantmentTime = 420;
				break;
			default:
				this.EnchantmentTime = 0;
				break;
			}
		}

		public override void Show()
		{
			if (base.Time >= this.DeadTime)
			{
				if (base.Time != 0)
				{
					if (base.Time < this.UnmatchedTime && base.Time / 2 % 2 == 0)
					{
						base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.Position, Color.DarkBlue);
					}
					else
					{
						base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.Position, Color.White);
					}
					for (int i = 0; i < this.PowerLevel; i++)
					{
						this.SubPlaneList[i].Show();
					}
				}
			}
		}

		public virtual void ShowCenter()
		{
			if (this.SlowMove)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["MyPlaneCenter1"], 1f, (float)base.Direction * 4f, base.Position, Color.White);
			}
		}

		private const int unmatchedTime = 300;

		private const int deltaGreen = 120;

		private int life = 2;

		private int spell = 2;

		private int lifeChip = 0;

		private int spellChip = 0;

		public readonly int nextSpellChip = 4;

		public readonly int[] nextLifeChip = new int[] { 3, 5, 8, 10, 12, 15, 18, 20, 22 };

		private int lifeUpCount = 0;

		private int power = 100;

		private long score = 0L;

		private long hiScore = 0L;

		private bool lastX = false;

		private int starPoint = 0;

		public readonly int maxStarPoint = 3000;

		private int nextBluePoint = 300;

		private int nextGreenPoint = 3000;

		private int bluePoint = 0;

		private int greenPoint = 0;

		private int blueLevel = 0;

		private int greenLevel = 0;

		private EnchantmentType enchantmentState = EnchantmentType.None;

		private EnchantmentType lastColor = EnchantmentType.Green;

		private int enchantmentTime;

		private float rate = 1f;

		private int enchantmentCount = 0;

		private int enchantmentCountNeeded = 0;

		private bool OnRotate = false;

		private int Angle3D = 0;
	}
}
