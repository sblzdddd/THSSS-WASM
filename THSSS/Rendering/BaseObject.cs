using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Shooting
{
	[Serializable]
	public class BaseObject
	{
		public int Time { get; set; }

		public TextureObject TxtureObject { get; set; }

		public bool Active { get; set; }

		public PointF OriginalPosition { get; set; }

		public static float PresentLerp = 1f;

		public PointF Position
		{
			get
			{
				float x = this.OriginalPosition.X;
				float y = this.OriginalPosition.Y;
				if (BaseObject.PresentLerp < 1f && this.hasPrev)
				{
					float t = BaseObject.PresentLerp;
					x = this.prevX + (x - this.prevX) * t;
					y = this.prevY + (y - this.prevY) * t;
				}
				return new PointF(x + (float)this.BoundRect.Left, y + (float)this.BoundRect.Top);
			}
			set
			{
				this.OriginalPosition = new PointF(value.X - (float)this.BoundRect.Left, value.Y - (float)this.BoundRect.Top);
			}
		}

		public PointF DestPoint { get; set; }

		public bool Mirrored { get; set; }

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
				if (this.dirReady && value == this.direction)
				{
					return;
				}
				this.direction = value;
				this.dirCos = MathF.Cos((float)value);
				this.dirSin = MathF.Sin((float)value);
				this.dirReady = true;
			}
		}

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

		public int TransparentValue
		{
			get
			{
				return (int)this.transparentValue;
			}
		}

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

		public int LifeTime { get; set; }

		public float Accelerate { get; set; }

		public float Ax { get; set; }

		public float Ay { get; set; }

		public float AngularVelocityDegree { get; set; }

		public float DirectionVelocityDegree { get; set; }

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

		public float ScaleVL { get; set; }

		public float ScaleVW { get; set; }

		public float TransparentVelocity { get; set; }

		public bool UnRemoveable { get; set; }

		public PointF[] LastPoints { get; set; }

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

		public Color GhostingColor { get; set; }

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

		public StageDataPackage StageData { get; set; }

		public MySprite SpriteMain
		{
			get
			{
				return this.StageData.SpriteMain;
			}
		}

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

		public Dictionary<string, TextureObject> TextureObjectDictionary
		{
			get
			{
				return this.StageData.TextureObjectDictionary;
			}
		}

		public Wave_Player BGM_Player
		{
			get
			{
				return this.StageData.BGM_Player;
			}
		}

		public KeyClass KClass
		{
			get
			{
				return this.StageData.KClass;
			}
		}

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

		public DifficultLevel Difficulty
		{
			get
			{
				return this.StageData.Difficulty;
			}
		}

		public BaseObject()
		{
		}

		public BaseObject(StageDataPackage StageData)
		{
			this.StageData = StageData;
		}

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

		public virtual void Move()
		{
			if (this.DestPoint.X != 0f || this.DestPoint.Y != 0f)
			{
				this.MovePoint();
			}
			if (!this.dirReady)
			{
				this.dirCos = MathF.Cos((float)this.direction);
				this.dirSin = MathF.Sin((float)this.direction);
				this.dirReady = true;
			}
			this.OriginalPosition = new PointF(this.OriginalPosition.X + this.Velocity * this.dirCos * this.scaleX, this.OriginalPosition.Y + this.Velocity * this.dirSin * this.scaleY);
		}

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

		public virtual bool HitCheck(BaseObject Target)
		{
			return this.HitCheck(Target, (float)this.Region);
		}

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
					float reach = Radius * this.Scale + (float)Target.Region * Target.Scale;
					if (reach > 0f)
					{
						float dx = this.OriginalPosition.X - Target.OriginalPosition.X;
						float dy = this.OriginalPosition.Y - Target.OriginalPosition.Y;
						if (dx * dx + dy * dy < reach * reach)
						{
							return true;
						}
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

		public virtual void HitCheckAll()
		{
		}

		public virtual void Shoot()
		{
		}

		protected void PlaceAt(float x, float y)
		{
			if (this.hasPrev)
			{
				this.prevX += x - this.OriginalPosition.X;
				this.prevY += y - this.OriginalPosition.Y;
			}
			this.OriginalPosition = new PointF(x, y);
		}

		public virtual void Ctrl()
		{
			this.prevX = this.OriginalPosition.X;
			this.prevY = this.OriginalPosition.Y;
			this.hasPrev = true;
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
				if (this.AngularVelocityDegree != 0f)
				{
					this.AngleDegree += (double)this.AngularVelocityDegree;
				}
				if (this.DirectionVelocityDegree != 0f)
				{
					this.DirectionDegree += (double)this.DirectionVelocityDegree;
				}
				this.TransparentValueF += this.TransparentVelocity;
				this.ScaleLength += this.ScaleVL;
				this.ScaleWidth += this.ScaleVW;
				if (this.AccelerateDictionary.Count > 0 && this.AccelerateDictionary.ContainsKey(this.Time))
				{
					this.Accelerate = this.AccelerateDictionary[this.Time];
				}
				if (this.VelocityDictionary.Count > 0 && this.VelocityDictionary.ContainsKey(this.Time))
				{
					this.Velocity = this.VelocityDictionary[this.Time];
				}
				if (this.DirectionVelocityDegreeDictionary.Count > 0 && this.DirectionVelocityDegreeDictionary.ContainsKey(this.Time))
				{
					this.DirectionVelocityDegree = this.DirectionVelocityDegreeDictionary[this.Time];
				}
				if (this.DirectionDegreeDictionary.Count > 0 && this.DirectionDegreeDictionary.ContainsKey(this.Time))
				{
					this.DirectionDegree = (double)this.DirectionDegreeDictionary[this.Time];
				}
				if (this.DestPointDictionary.Count > 0 && this.DestPointDictionary.ContainsKey(this.Time))
				{
					this.DestPoint = this.DestPointDictionary[this.Time];
				}
				if (this.TransparentVelocityDictionary.Count > 0 && this.TransparentVelocityDictionary.ContainsKey(this.Time))
				{
					this.TransparentVelocity = this.TransparentVelocityDictionary[this.Time];
				}
				if (this.ScaleWidthVelocityDictionary.Count > 0 && this.ScaleWidthVelocityDictionary.ContainsKey(this.Time))
				{
					this.ScaleVW = this.ScaleWidthVelocityDictionary[this.Time];
				}
				if (this.ScaleLengthVelocityDictionary.Count > 0 && this.ScaleLengthVelocityDictionary.ContainsKey(this.Time))
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

		public void SetBinding(BaseObject BindingObj)
		{
			this.BindingObj = BindingObj;
			this.Ctrl();
		}

		public virtual void GiveEndEffect()
		{
		}

		public virtual void GiveItems()
		{
		}

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

		public virtual void ShowRegion()
		{
			if (this.Region >= 0)
			{
				double num = (this.AngleWithDirection ? (this.Direction + this.Angle - 1.5707963267948966) : (this.Angle - 1.5707963267948966));
				this.SpriteMain.Draw2D(this.TextureObjectDictionary["Region"], 2f * (float)this.Region / (float)this.TextureObjectDictionary["Region"].Width * this.ScaleWidth, 2f * (float)this.Region / (float)this.TextureObjectDictionary["Region"].Height * this.ScaleLength, (float)num, this.Position, Color.White, this.Mirrored);
			}
		}

		public double GetDirection(BaseObject Target)
		{
			return this.GetDirection(Target.OriginalPosition);
		}

		public double GetDirection(PointF TargetOriginalPosition)
		{
			return 3.141592653589793 + Math.Atan2((double)(this.OriginalPosition.Y - TargetOriginalPosition.Y), (double)(this.OriginalPosition.X - TargetOriginalPosition.X));
		}

		public double GetDistance(BaseObject Target)
		{
			return this.GetDistance(Target.OriginalPosition);
		}

		public double GetDistance(PointF TargetOriginalPosition)
		{
			return Math.Sqrt((double)((this.OriginalPosition.X - TargetOriginalPosition.X) * (this.OriginalPosition.X - TargetOriginalPosition.X) + (this.OriginalPosition.Y - TargetOriginalPosition.Y) * (this.OriginalPosition.Y - TargetOriginalPosition.Y)));
		}

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

		public virtual void CopyPosition(BaseObject b)
		{
			this.Angle = b.Angle;
			this.Direction = b.Direction;
			this.Position = b.Position;
		}

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

		public virtual object Clone()
		{
			BaseObject baseObject = (BaseObject)base.MemberwiseClone();
			baseObject.GhostingCount = this.GhostingCount;
			return baseObject;
		}

		public virtual void Dispose()
		{
		}

		[System.NonSerialized]
		private float prevX;

		[System.NonSerialized]
		private float prevY;

		[System.NonSerialized]
		private bool hasPrev;

		private float velocity = 0f;

		private float maxVelocity = 100f;

		private float minVelocity = -100f;

		private double direction = 0.0;

		private float dirCos = 1f;

		private float dirSin;

		private bool dirReady;

		private bool enabled = true;

		private bool shootEnabled = true;

		private bool hitEnabled = true;

		private int region = 3;

		private int outsideRegion = 3;

		private int damage = 40;

		private float healthPoint = 1f;

		private float transparentValue = 255f;

		private int maxTransparent = 255;

		private Color colorValue = Color.White;

		private double angle = 0.0;

		private float scaleLength = 1f;

		private float scaleWidth = 1f;

		private float maxScaleL = 100f;

		private float maxScaleW = 100f;

		private float minScaleL = 0.0001f;

		private float minScaleW = 0.0001f;

		private bool angleWithDirection = true;

		private float scaleX = 1f;

		private float scaleY = 1f;

		protected BaseObject BindingObj;

		private BaseObject LastBindingObj;

		public Dictionary<int, float> AccelerateDictionary = new Dictionary<int, float>();

		public Dictionary<int, float> VelocityDictionary = new Dictionary<int, float>();

		public Dictionary<int, float> DirectionVelocityDegreeDictionary = new Dictionary<int, float>();

		public Dictionary<int, float> DirectionDegreeDictionary = new Dictionary<int, float>();

		public Dictionary<int, PointF> DestPointDictionary = new Dictionary<int, PointF>();

		public Dictionary<int, float> TransparentVelocityDictionary = new Dictionary<int, float>();

		public Dictionary<int, float> ScaleWidthVelocityDictionary = new Dictionary<int, float>();

		public Dictionary<int, float> ScaleLengthVelocityDictionary = new Dictionary<int, float>();

		public BaseObject.CtrlActionCallBack CtrlCall;

		public delegate void CtrlActionCallBack(string s);
	}
}
