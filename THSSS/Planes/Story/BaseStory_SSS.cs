using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using SlimDX.Direct3D9;

namespace Shooting.Planes.Story
{
	internal class BaseStory_SSS : BaseStory
	{
		public BaseStory_SSS()
		{
		}

		public BaseStory_SSS(StageDataPackage StageData)
			: base(StageData)
		{
			this.CharR = new CharacterR_Touhou(StageData, " ")
			{
				OriginalPosition = new PointF((float)(base.BoundRect.Width - 60), 380f)
			};
			this.CharL = new CharacterL_Touhou(StageData, " ")
			{
				OriginalPosition = new PointF(70f, 380f)
			};
			this.CharL2 = new CharacterL_Touhou(StageData, " ")
			{
				OriginalPosition = new PointF(70f, 380f)
			};
			this.SBox = new StoryBox(StageData);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			this.CharL.Ctrl();
			this.CharL2.Ctrl();
			this.CharR.Ctrl();
			this.SBox.Ctrl();
			this.StoryEffectList.ForEach(delegate(BaseObject x)
			{
				x.Ctrl();
				if (x.OutBoundary())
				{
					this.StoryEffectList.Remove(x);
				}
			});
			if (this.CharN != null)
			{
				this.CharN.Ctrl();
			}
			if (base.Time < this.Conv.Count)
			{
				this.CharR.Active = this.Conv[base.Time].CharRAcitve;
				this.CharL.Active = this.Conv[base.Time].CharLAcitve;
				this.CharL2.Active = this.Conv[base.Time].CharL2Acitve;
				if (this.Conv[base.Time].CharL != null)
				{
					this.CharL.TxtureObject = base.TextureObjectDictionary[this.Conv[base.Time].CharL];
				}
				if (this.Conv[base.Time].CharL2 != null)
				{
					this.CharL2.TxtureObject = base.TextureObjectDictionary[this.Conv[base.Time].CharL2];
				}
				if (this.Conv[base.Time].CharR != null)
				{
					this.CharR.TxtureObject = base.TextureObjectDictionary[this.Conv[base.Time].CharR];
				}
				this.SBox.Text = this.Conv[base.Time].Text;
			}
		}

		public override void Show()
		{
			if (this.CharR.Active)
			{
				if (this.CharL2.Active)
				{
					this.CharL2.Show();
					this.CharL.Show();
				}
				else
				{
					this.CharL.Show();
					this.CharL2.Show();
				}
				this.CharR.Show();
			}
			else
			{
				this.CharR.Show();
				if (this.CharL2.Active)
				{
					this.CharL.Show();
					this.CharL2.Show();
				}
				else
				{
					this.CharL2.Show();
					this.CharL.Show();
				}
			}
			this.SBox.Show();
			if (this.CharN != null)
			{
				this.CharN.Show();
			}
			if (this.StoryEffectList.Count > 0)
			{
				this.StoryEffectList.ForEach(delegate(BaseObject x)
				{
					if (!x.Active)
					{
						x.Show();
					}
				});
				base.SpriteMain.End();
				base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
				base.StageData.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
				base.StageData.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
				this.StoryEffectList.ForEach(delegate(BaseObject x)
				{
					if (x.Active)
					{
						x.Show();
					}
				});
				base.SpriteMain.End();
				base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			}
		}

		protected virtual void LoadConversation(string fileName)
		{
			if (File.Exists(fileName))
			{
				char[] array = new char[] { ',' };
				string[] array2 = File.ReadAllLines(fileName, Encoding.Default);
				this.Conv.Clear();
				int num = 0;
				foreach (string text in array2)
				{
					string[] array4 = text.Split(array);
					if (num == 0)
					{
						num++;
						this.NameTime = int.Parse(array4[0]);
						this.MusicTime = int.Parse(array4[1]);
					}
					else
					{
						Conversation conversation = new Conversation(array4[0], bool.Parse(array4[1]), bool.Parse(array4[2]), array4[3], array4[4]);
						this.Conv.Add(conversation);
					}
				}
			}
		}

		protected virtual void LoadConversation2(string fileName)
		{
			if (File.Exists(fileName))
			{
				char[] array = new char[] { ',' };
				string[] array2 = File.ReadAllLines(fileName, Encoding.Default);
				this.Conv.Clear();
				int num = 0;
				foreach (string text in array2)
				{
					string[] array4 = text.Split(array);
					if (num == 0)
					{
						num++;
						this.NameTime = int.Parse(array4[0]);
						this.MusicTime = int.Parse(array4[1]);
					}
					else
					{
						Conversation conversation = new Conversation(array4[0], bool.Parse(array4[1]), array4[2], bool.Parse(array4[3]), array4[4], bool.Parse(array4[5]), array4[6]);
						this.Conv.Add(conversation);
					}
				}
			}
		}

		protected List<Conversation> Conv = new List<Conversation>();

		protected CharacterL_Touhou CharL;

		protected CharacterL_Touhou CharL2;

		protected CharacterR_Touhou CharR;

		protected StoryBox SBox;

		protected CharacterName CharN;

		protected int LastTime;

		protected int NameTime;

		protected int MusicTime;
	}
}
