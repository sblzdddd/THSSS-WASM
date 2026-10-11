using System;
using SlimDX;

namespace Shooting
{
	// Token: 0x02000097 RID: 151
	public interface IEnvironment
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004A8 RID: 1192
		float CameraAngle { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004A9 RID: 1193
		Vector3 CameraPos { get; }

		// Token: 0x060004AA RID: 1194
		void Ctrl();

		// Token: 0x060004AB RID: 1195
		void SetRenderPara();
	}
}
