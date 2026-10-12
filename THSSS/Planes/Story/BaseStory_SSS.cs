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
			CharR = new CharacterR_Touhou(StageData, " ")
			{
				OriginalPosition = new PointF((float)(BoundRect.Width - 60), 380f)
			};
			CharL = new CharacterL_Touhou(StageData, " ")
			{
				OriginalPosition = new PointF(70f, 380f)
			};
			CharL2 = new CharacterL_Touhou(StageData, " ")
			{
				OriginalPosition = new PointF(70f, 380f)
			};
			SBox = new StoryBox(StageData);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			CharL.Ctrl();
			CharL2.Ctrl();
			CharR.Ctrl();
			SBox.Ctrl();
			StoryEffectList.ForEach(delegate(BaseObject x)
			{
				x.Ctrl();
				if (x.OutBoundary())
				{
					StoryEffectList.Remove(x);
				}
			});
			if (CharN != null)
			{
				CharN.Ctrl();
			}
			if (Time < Conv.Count)
			{
				CharR.Active = Conv[Time].CharRAcitve;
				CharL.Active = Conv[Time].CharLAcitve;
				CharL2.Active = Conv[Time].CharL2Acitve;
				if (Conv[Time].CharL != null)
				{
					CharL.TxtureObject = TextureObjectDictionary[Conv[Time].CharL];
				}
				if (Conv[Time].CharL2 != null)
				{
					CharL2.TxtureObject = TextureObjectDictionary[Conv[Time].CharL2];
				}
				if (Conv[Time].CharR != null)
				{
					CharR.TxtureObject = TextureObjectDictionary[Conv[Time].CharR];
				}
				SBox.Text = Conv[Time].Text;
			}
		}

		public override void Show()
		{
			if (CharR.Active)
			{
				if (CharL2.Active)
				{
					CharL2.Show();
					CharL.Show();
				}
				else
				{
					CharL.Show();
					CharL2.Show();
				}
				CharR.Show();
			}
			else
			{
				CharR.Show();
				if (CharL2.Active)
				{
					CharL.Show();
					CharL2.Show();
				}
				else
				{
					CharL2.Show();
					CharL.Show();
				}
			}
			SBox.Show();
			if (CharN != null)
			{
				CharN.Show();
			}
			if (StoryEffectList.Count > 0)
			{
				StoryEffectList.ForEach(delegate(BaseObject x)
				{
					if (!x.Active)
					{
						x.Show();
					}
				});
				SpriteMain.End();
				SpriteMain.Begin(SpriteFlags.AlphaBlend);
				StageData.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
				StageData.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
				StoryEffectList.ForEach(delegate(BaseObject x)
				{
					if (x.Active)
					{
						x.Show();
					}
				});
				SpriteMain.End();
				SpriteMain.Begin(SpriteFlags.AlphaBlend);
			}
		}

		protected virtual void LoadConversation(string fileName)
		{
			if (File.Exists(fileName))
			{
				char[] array = new char[] { ',' };
				string[] array2 = File.ReadAllLines(fileName, Encoding.Default);
				Conv.Clear();
				int num = 0;
				foreach (string text in array2)
				{
					string[] array4 = text.Split(array);
					if (num == 0)
					{
						num++;
						NameTime = int.Parse(array4[0]);
						MusicTime = int.Parse(array4[1]);
					}
					else
					{
						Conversation conversation = new Conversation(array4[0], bool.Parse(array4[1]), bool.Parse(array4[2]), array4[3], array4[4]);
						Conv.Add(conversation);
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
				Conv.Clear();
				int num = 0;
				foreach (string text in array2)
				{
					string[] array4 = text.Split(array);
					if (num == 0)
					{
						num++;
						NameTime = int.Parse(array4[0]);
						MusicTime = int.Parse(array4[1]);
					}
					else
					{
						Conversation conversation = new Conversation(array4[0], bool.Parse(array4[1]), array4[2], bool.Parse(array4[3]), array4[4], bool.Parse(array4[5]), array4[6]);
						Conv.Add(conversation);
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
