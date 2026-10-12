using System.Drawing;

namespace Shooting
{
	internal class CharacterName : BaseStoryItem
	{
		public CharacterName(StageDataPackage StageData, string textureName)
			: base(StageData)
		{
			Position = new PointF((float)(BoundRect.Right - 70), (float)(BoundRect.Top + 300));
			Velocity = 0f;
			Direction = 1.5707963267948966;
			TransparentValueF = 0f;
			TxtureObject = TextureObjectDictionary[textureName];
			TransparentVelocityDictionary.Add(10, 20f);
			TransparentVelocityDictionary.Add(210, -10f);
		}
	}
}
