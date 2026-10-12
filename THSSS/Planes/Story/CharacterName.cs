using System.Drawing;

namespace Shooting
{
	internal class CharacterName : BaseStoryItem
	{
		public CharacterName(StageDataPackage StageData, string textureName)
			: base(StageData)
		{
			base.Position = new PointF((float)(base.BoundRect.Right - 70), (float)(base.BoundRect.Top + 300));
			base.Velocity = 0f;
			base.Direction = 1.5707963267948966;
			base.TransparentValueF = 0f;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			this.TransparentVelocityDictionary.Add(10, 20f);
			this.TransparentVelocityDictionary.Add(210, -10f);
		}
	}
}
