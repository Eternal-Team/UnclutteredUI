using Terraria.ModLoader;

namespace UnclutteredUI
{
	public class UnclutteredUI : Mod
	{
		public override void Load()
		{
			Hooking.Initialize();
		}
	}
}