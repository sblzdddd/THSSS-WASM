using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
#if !BROWSER
using System.Runtime.Serialization.Formatters.Binary;
#endif

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
				float x = OriginalPosition.X;
				float y = OriginalPosition.Y;
				if (PresentLerp < 1f && hasPrev)
				{
					float t = PresentLerp;
					x = prevX + (x - prevX) * t;
					y = prevY + (y - prevY) * t;
				}
				return new PointF(x + (float)BoundRect.Left, y + (float)BoundRect.Top);
			}
			set
			{
				OriginalPosition = new PointF(value.X - (float)BoundRect.Left, value.Y - (float)BoundRect.Top);
			}
		}

		public PointF DestPoint { get; set; }

		public bool Mirrored { get; set; }

		public float Velocity
		{
			get
			{
				return velocity;
			}
			set
			{
				if (value > MaxVelocity)
				{
					velocity = MaxVelocity;
				}
				else if (value < MinVelocity)
				{
					velocity = minVelocity;
				}
				else
				{
					velocity = value;
				}
			}
		}

		public float MaxVelocity
		{
			get
			{
				return maxVelocity;
			}
			set
			{
				maxVelocity = value;
			}
		}

		public float MinVelocity
		{
			get
			{
				return minVelocity;
			}
			set
			{
				minVelocity = value;
			}
		}

		public float Vx
		{
			get
			{
				return Velocity * (float)Math.Cos(Direction);
			}
			set
			{
				float vy = Vy;
				Velocity = (float)Math.Sqrt((double)(value * value + vy * vy));
				Direction = ((Velocity == 0f) ? 0.0 : Math.Atan2((double)vy, (double)value));
			}
		}

		public float Vy
		{
			get
			{
				return Velocity * (float)Math.Sin(Direction);
			}
			set
			{
				float vx = Vx;
				Velocity = (float)Math.Sqrt((double)(value * value + vx * vx));
				Direction = ((Velocity == 0f) ? 0.0 : Math.Atan2((double)value, (double)vx));
			}
		}

		public double Angle
		{
			get
			{
				return angle;
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
				angle = value;
			}
		}

		public double AngleDegree
		{
			get
			{
				return Angle / 3.141592653589793 * 180.0;
			}
			set
			{
				Angle = value * 3.141592653589793 / 180.0;
			}
		}

		public double Direction
		{
			get
			{
				return direction;
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
				if (dirReady && value == direction)
				{
					return;
				}
				direction = value;
				dirCos = MathF.Cos((float)value);
				dirSin = MathF.Sin((float)value);
				dirReady = true;
			}
		}

		public double DirectionDegree
		{
			get
			{
				return Direction / 3.141592653589793 * 180.0;
			}
			set
			{
				Direction = value * 3.141592653589793 / 180.0;
			}
		}

		public float Scale
		{
			get
			{
				return scaleLength;
			}
			set
			{
				if (value > MaxScale)
				{
					scaleLength = MaxScale;
					scaleWidth = MaxScale;
				}
				else if (value < MinScale)
				{
					scaleLength = 0f;
					scaleWidth = 0f;
				}
				else
				{
					scaleLength = value;
					scaleWidth = value;
				}
			}
		}

		public float MaxScale
		{
			get
			{
				return maxScaleL;
			}
			set
			{
				maxScaleL = value;
				maxScaleW = value;
			}
		}

		public float MinScale
		{
			get
			{
				return minScaleL;
			}
			set
			{
				minScaleL = value;
				minScaleW = value;
			}
		}

		public float MaxScaleL
		{
			get
			{
				return maxScaleL;
			}
			set
			{
				maxScaleL = value;
			}
		}

		public float MaxScaleW
		{
			get
			{
				return maxScaleW;
			}
			set
			{
				maxScaleW = value;
			}
		}

		public float MinScaleL
		{
			get
			{
				return minScaleL;
			}
			set
			{
				minScaleL = value;
			}
		}

		public float MinScaleW
		{
			get
			{
				return minScaleW;
			}
			set
			{
				minScaleW = value;
			}
		}

		public float ScaleX
		{
			get
			{
				return scaleX;
			}
			set
			{
				scaleX = value;
			}
		}

		public float ScaleY
		{
			get
			{
				return scaleY;
			}
			set
			{
				scaleY = value;
			}
		}

		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
			}
		}

		public bool HitEnabled
		{
			get
			{
				return hitEnabled;
			}
			set
			{
				hitEnabled = value;
			}
		}

		public bool ShootEnabled
		{
			get
			{
				return shootEnabled;
			}
			set
			{
				shootEnabled = value;
			}
		}

		public int Region
		{
			get
			{
				return region;
			}
			set
			{
				region = value;
				outsideRegion = value;
			}
		}

		public int OutsideRegion
		{
			get
			{
				return outsideRegion;
			}
			set
			{
				outsideRegion = value;
			}
		}

		public int Damage
		{
			get
			{
				return damage;
			}
			set
			{
				damage = value;
			}
		}

		public float HealthPoint
		{
			get
			{
				return healthPoint;
			}
			set
			{
				healthPoint = value;
			}
		}

		public float ScaleLength
		{
			get
			{
				return scaleLength;
			}
			set
			{
				if (value > MaxScaleL)
				{
					scaleLength = MaxScaleL;
				}
				else if (value < MinScaleL)
				{
					scaleLength = MinScaleL;
				}
				else
				{
					scaleLength = value;
				}
			}
		}

		public float ScaleWidth
		{
			get
			{
				return scaleWidth;
			}
			set
			{
				if (value > MaxScaleW)
				{
					scaleWidth = MaxScaleW;
				}
				else if (value < MinScaleW)
				{
					scaleWidth = MinScaleW;
				}
				else
				{
					scaleWidth = value;
				}
			}
		}

		public int MaxTransparent
		{
			get
			{
				return maxTransparent;
			}
			set
			{
				if (value < 0)
				{
					maxTransparent = 0;
				}
				else if (value > 255)
				{
					maxTransparent = 255;
				}
				else
				{
					maxTransparent = value;
				}
				if (transparentValue > (float)maxTransparent)
				{
					transparentValue = (float)maxTransparent;
				}
			}
		}

		public float TransparentValueF
		{
			get
			{
				return transparentValue;
			}
			set
			{
				if (value < 0f)
				{
					transparentValue = 0f;
				}
				else if (value > (float)MaxTransparent)
				{
					transparentValue = (float)MaxTransparent;
				}
				else
				{
					transparentValue = value;
				}
			}
		}

		public int TransparentValue
		{
			get
			{
				return (int)transparentValue;
			}
		}

		public Color ColorValue
		{
			get
			{
				return colorValue;
			}
			set
			{
				colorValue = value;
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
				return ScaleVL;
			}
			set
			{
				ScaleVL = value;
				ScaleVW = value;
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
				if (LastPoints != null)
				{
					num = LastPoints.Length;
				}
				else
				{
					num = 0;
				}
				return num;
			}
			set
			{
				LastPoints = new PointF[value];
				for (int i = 0; i < value; i++)
				{
					LastPoints[i] = OriginalPosition;
				}
			}
		}

		public Color GhostingColor { get; set; }

		public bool AngleWithDirection
		{
			get
			{
				return angleWithDirection;
			}
			set
			{
				angleWithDirection = value;
			}
		}

		public StageDataPackage StageData { get; set; }

		public MySprite SpriteMain
		{
			get
			{
				return StageData.SpriteMain;
			}
		}

		public Rectangle BoundRect
		{
			get
			{
				return StageData.BoundRect;
			}
			set
			{
				StageData.BoundRect = value;
			}
		}

		public int TimeMain
		{
			get
			{
				return StageData.TimeMain;
			}
			set
			{
				StageData.TimeMain = value;
			}
		}

		public BaseMyPlane MyPlane
		{
			get
			{
				return StageData.MyPlane;
			}
			set
			{
				StageData.MyPlane = value;
			}
		}

		public List<BaseBullet_Touhou> BulletList
		{
			get
			{
				return StageData.BulletList;
			}
			set
			{
				StageData.BulletList = value;
			}
		}

		public List<BaseObject> MyBulletList
		{
			get
			{
				return StageData.MyBulletList;
			}
			set
			{
				StageData.MyBulletList = value;
			}
		}

		public List<BaseObject> SpellList
		{
			get
			{
				return StageData.SpellList;
			}
			set
			{
				StageData.SpellList = value;
			}
		}

		public List<BaseEnemyPlane> EnemyPlaneList
		{
			get
			{
				return StageData.EnemyPlaneList;
			}
			set
			{
				StageData.EnemyPlaneList = value;
			}
		}

		public List<BaseEffect> EffectList
		{
			get
			{
				return StageData.EffectList;
			}
			set
			{
				StageData.EffectList = value;
			}
		}

		public List<BaseItem> ItemList
		{
			get
			{
				return StageData.ItemList;
			}
			set
			{
				StageData.ItemList = value;
			}
		}

		public BaseStory Story
		{
			get
			{
				return StageData.Story;
			}
			set
			{
				StageData.Story = value;
			}
		}

		public BackgroundManager Background
		{
			get
			{
				return StageData.Background;
			}
			set
			{
				StageData.Background = value;
			}
		}

		public BackgroundManager Background2
		{
			get
			{
				return StageData.Background2;
			}
			set
			{
				StageData.Background2 = value;
			}
		}

		public BackgroundManager3D Background3D
		{
			get
			{
				return StageData.Background3D;
			}
			set
			{
				StageData.Background3D = value;
			}
		}

		public BaseBossTouhou Boss
		{
			get
			{
				return StageData.Boss;
			}
			set
			{
				StageData.Boss = value;
			}
		}

		public List<XAudio2_Player> SoundPlayList
		{
			get
			{
				return StageData.SoundPlayList;
			}
			set
			{
				StageData.SoundPlayList = value;
			}
		}

		public Dictionary<string, TextureObject> TextureObjectDictionary
		{
			get
			{
				return StageData.TextureObjectDictionary;
			}
		}

		public Wave_Player BGM_Player
		{
			get
			{
				return StageData.BGM_Player;
			}
		}

		public KeyClass KClass
		{
			get
			{
				return StageData.KClass;
			}
		}

		public MyRandom Ran
		{
			get
			{
				return StageData.Ran;
			}
			set
			{
				StageData.Ran = value;
			}
		}

		public DifficultLevel Difficulty
		{
			get
			{
				return StageData.Difficulty;
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
				TxtureObject = TextureObjectDictionary[textureName];
			}
			GhostingColor = Color.White;
		}

		public virtual void Move()
		{
			if (DestPoint.X != 0f || DestPoint.Y != 0f)
			{
				MovePoint();
			}
			if (!dirReady)
			{
				dirCos = MathF.Cos((float)direction);
				dirSin = MathF.Sin((float)direction);
				dirReady = true;
			}
			OriginalPosition = new PointF(OriginalPosition.X + Velocity * dirCos * scaleX, OriginalPosition.Y + Velocity * dirSin * scaleY);
		}

		public void MovePoint()
		{
			float num = (float)GetDistance(DestPoint);
			if (num > Math.Abs(Velocity))
			{
				Direction = GetDirection(DestPoint);
				Accelerate = -Velocity * Velocity / (2f * num);
				if (Velocity < 0f)
				{
					Velocity = 0f;
				}
			}
			else
			{
				Accelerate = 0f;
				Velocity = 0f;
			}
		}

		public virtual bool OutBoundary()
		{
			bool flag;
			if (LifeTime != 0 && Time > LifeTime)
			{
				flag = true;
			}
			else if (Time < 100)
			{
				flag = false;
			}
			else
			{
				int num;
				if ((float)OutsideRegion * Scale * 2f < 20f)
				{
					num = 20;
				}
				else
				{
					num = (int)((float)OutsideRegion * Scale * 2f);
				}
				if ((float)(-(float)num) < OriginalPosition.X && OriginalPosition.X < (float)(BoundRect.Width + num) && (float)(-(float)num) < OriginalPosition.Y && OriginalPosition.Y < (float)(BoundRect.Height + num))
				{
					flag = false;
				}
				else
				{
					if (GhostingCount > 0)
					{
						if ((float)(-(float)num) < LastPoints[GhostingCount - 1].X && LastPoints[GhostingCount - 1].X < (float)(BoundRect.Width + num) && (float)(-(float)num) < LastPoints[GhostingCount - 1].Y && LastPoints[GhostingCount - 1].Y < (float)(BoundRect.Height + num))
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
			return HitCheck(Target, (float)Region);
		}

		public virtual bool HitCheck(BaseObject Target, float Radius)
		{
			bool flag;
			if (Time < 9)
			{
				flag = false;
			}
			else
			{
				if (ScaleLength == ScaleWidth)
				{
					float reach = Radius * Scale + (float)Target.Region * Target.Scale;
					if (reach > 0f)
					{
						float dx = OriginalPosition.X - Target.OriginalPosition.X;
						float dy = OriginalPosition.Y - Target.OriginalPosition.Y;
						if (dx * dx + dy * dy < reach * reach)
						{
							return true;
						}
					}
				}
				else
				{
					double num = (AngleWithDirection ? (Direction + Angle) : Angle);
					double num2 = GetDistance(Target) + (double)((float)Target.Region * Target.Scale);
					double num3 = GetDirection(Target);
					double num4 = num2 * Math.Cos((double)((float)(num - num3)));
					double num5 = num2 * Math.Sin((double)((float)(num - num3)));
					double num6 = (double)(Radius * ScaleWidth);
					double num7 = (double)(Radius * ScaleLength);
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
			if (hasPrev)
			{
				prevX += x - OriginalPosition.X;
				prevY += y - OriginalPosition.Y;
			}
			OriginalPosition = new PointF(x, y);
		}

		public virtual void Ctrl()
		{
			prevX = OriginalPosition.X;
			prevY = OriginalPosition.Y;
			hasPrev = true;
			if (Enabled)
			{
				Time++;
				Move();
				if (BindingObj != null)
				{
					if (LastBindingObj == null)
					{
						LastBindingObj = new BaseObject();
						LastBindingObj.StageData = StageData;
						LastBindingObj.CopyPosition(BindingObj);
					}
					double distance = GetDistance(LastBindingObj);
					double num = GetDirection(LastBindingObj) + 3.141592653589793 + BindingObj.Angle - LastBindingObj.Angle;
					Direction += BindingObj.Angle - LastBindingObj.Angle;
					if (BindingObj.AngleWithDirection)
					{
						num += BindingObj.Direction - LastBindingObj.Direction;
						Direction += BindingObj.Direction - LastBindingObj.Direction;
					}
					OriginalPosition = new PointF(BindingObj.OriginalPosition.X + (float)(distance * Math.Cos(num)), BindingObj.OriginalPosition.Y + (float)(distance * Math.Sin(num)));
					LastBindingObj.CopyPosition(BindingObj);
				}
				if (Ax != 0f || Ay != 0f)
				{
					float num2 = Velocity;
					double num = Direction;
					Velocity = (float)Math.Sqrt((double)(num2 * num2 + Ax * Ax + Ay * Ay) + (double)(2f * num2 * Ax) * Math.Cos(num) + (double)(2f * num2 * Ay) * Math.Sin(num));
					Direction = Math.Atan2((double)num2 * Math.Sin(num) + (double)Ay, (double)num2 * Math.Cos(num) + (double)Ax);
				}
				Velocity += Accelerate;
				if (AngularVelocityDegree != 0f)
				{
					AngleDegree += (double)AngularVelocityDegree;
				}
				if (DirectionVelocityDegree != 0f)
				{
					DirectionDegree += (double)DirectionVelocityDegree;
				}
				TransparentValueF += TransparentVelocity;
				ScaleLength += ScaleVL;
				ScaleWidth += ScaleVW;
				if (AccelerateDictionary.Count > 0 && AccelerateDictionary.ContainsKey(Time))
				{
					Accelerate = AccelerateDictionary[Time];
				}
				if (VelocityDictionary.Count > 0 && VelocityDictionary.ContainsKey(Time))
				{
					Velocity = VelocityDictionary[Time];
				}
				if (DirectionVelocityDegreeDictionary.Count > 0 && DirectionVelocityDegreeDictionary.ContainsKey(Time))
				{
					DirectionVelocityDegree = DirectionVelocityDegreeDictionary[Time];
				}
				if (DirectionDegreeDictionary.Count > 0 && DirectionDegreeDictionary.ContainsKey(Time))
				{
					DirectionDegree = (double)DirectionDegreeDictionary[Time];
				}
				if (DestPointDictionary.Count > 0 && DestPointDictionary.ContainsKey(Time))
				{
					DestPoint = DestPointDictionary[Time];
				}
				if (TransparentVelocityDictionary.Count > 0 && TransparentVelocityDictionary.ContainsKey(Time))
				{
					TransparentVelocity = TransparentVelocityDictionary[Time];
				}
				if (ScaleWidthVelocityDictionary.Count > 0 && ScaleWidthVelocityDictionary.ContainsKey(Time))
				{
					ScaleVW = ScaleWidthVelocityDictionary[Time];
				}
				if (ScaleLengthVelocityDictionary.Count > 0 && ScaleLengthVelocityDictionary.ContainsKey(Time))
				{
					ScaleVL = ScaleLengthVelocityDictionary[Time];
				}
				if (CtrlCall != null)
				{
					CtrlCall(null);
				}
				if (GhostingCount > 0)
				{
					for (int i = LastPoints.Length - 1; i > 0; i--)
					{
						LastPoints[i] = LastPoints[i - 1];
					}
					LastPoints[0] = OriginalPosition;
				}
			}
		}

		public void SetBinding(BaseObject BindingObj)
		{
			this.BindingObj = BindingObj;
			Ctrl();
		}

		public virtual void GiveEndEffect()
		{
		}

		public virtual void GiveItems()
		{
		}

		public virtual void Show()
		{
			if (TxtureObject != null)
			{
				if (TransparentValue > 0)
				{
					double num = (AngleWithDirection ? (Direction + Angle - 1.5707963267948966) : (Angle - 1.5707963267948966));
					for (int i = GhostingCount - 1; i >= 0; i -= 2)
					{
						float num2 = 1f - 0.1f * (float)i / (float)LastPoints.Length;
						PointF pointF = new PointF(LastPoints[i].X + (float)BoundRect.Left, LastPoints[i].Y + (float)BoundRect.Top);
						SpriteMain.Draw2D(TxtureObject, ScaleWidth * num2, ScaleLength * num2, (float)num, pointF, Color.FromArgb(TransparentValue * (GhostingCount - i) / (GhostingCount + 1) / 4, GhostingColor), Mirrored);
					}
					SpriteMain.Draw2D(TxtureObject, ScaleWidth, ScaleLength, (float)num, Position, Color.FromArgb(TransparentValue, ColorValue), Mirrored);
				}
			}
		}

		public virtual void ShowRegion()
		{
			if (Region >= 0)
			{
				double num = (AngleWithDirection ? (Direction + Angle - 1.5707963267948966) : (Angle - 1.5707963267948966));
				SpriteMain.Draw2D(TextureObjectDictionary["Region"], 2f * (float)Region / (float)TextureObjectDictionary["Region"].Width * ScaleWidth, 2f * (float)Region / (float)TextureObjectDictionary["Region"].Height * ScaleLength, (float)num, Position, Color.White, Mirrored);
			}
		}

		public double GetDirection(BaseObject Target)
		{
			return GetDirection(Target.OriginalPosition);
		}

		public double GetDirection(PointF TargetOriginalPosition)
		{
			return 3.141592653589793 + Math.Atan2((double)(OriginalPosition.Y - TargetOriginalPosition.Y), (double)(OriginalPosition.X - TargetOriginalPosition.X));
		}

		public double GetDistance(BaseObject Target)
		{
			return GetDistance(Target.OriginalPosition);
		}

		public double GetDistance(PointF TargetOriginalPosition)
		{
			return Math.Sqrt((double)((OriginalPosition.X - TargetOriginalPosition.X) * (OriginalPosition.X - TargetOriginalPosition.X) + (OriginalPosition.Y - TargetOriginalPosition.Y) * (OriginalPosition.Y - TargetOriginalPosition.Y)));
		}

		public virtual void Copy(BaseObject b)
		{
			Time = b.Time;
			TxtureObject = b.TxtureObject;
			Active = b.Active;
			Position = b.Position;
			DestPoint = b.DestPoint;
			Mirrored = b.Mirrored;
			Velocity = b.Velocity;
			Angle = b.Angle;
			Direction = b.Direction;
			Scale = b.Scale;
			Enabled = b.Enabled;
			HitEnabled = b.HitEnabled;
			Region = b.Region;
			Damage = b.Damage;
			HealthPoint = b.HealthPoint;
			ScaleLength = b.ScaleLength;
			ScaleWidth = b.ScaleWidth;
			MaxTransparent = b.MaxTransparent;
			TransparentValueF = b.TransparentValueF;
			ColorValue = b.ColorValue;
			LifeTime = b.LifeTime;
			Accelerate = b.Accelerate;
			AngularVelocityDegree = b.AngularVelocityDegree;
			DirectionVelocityDegree = b.DirectionVelocityDegree;
			ScaleVelocity = b.ScaleVelocity;
			TransparentVelocity = b.TransparentVelocity;
			GhostingCount = b.GhostingCount;
			GhostingColor = b.GhostingColor;
			UnRemoveable = b.UnRemoveable;
			AngleWithDirection = b.AngleWithDirection;
			BindingObj = b.BindingObj;
		}

		public virtual void CopyPosition(BaseObject b)
		{
			Angle = b.Angle;
			Direction = b.Direction;
			Position = b.Position;
		}

		public object Clone(object Obj)
		{
#if BROWSER
			throw new NotSupportedException("BinaryFormatter clone is not available in the browser host.");
#else
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
#endif
		}

		public virtual object Clone()
		{
			BaseObject baseObject = (BaseObject)MemberwiseClone();
			baseObject.GhostingCount = GhostingCount;
			return baseObject;
		}

		public virtual void Dispose()
		{
		}

		[NonSerialized]
		private float prevX;

		[NonSerialized]
		private float prevY;

		[NonSerialized]
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

		public CtrlActionCallBack CtrlCall;

		public delegate void CtrlActionCallBack(string s);
	}
}
