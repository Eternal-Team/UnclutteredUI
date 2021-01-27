using IL.Terraria.UI;

namespace UnclutteredUI
{
	public static partial class Hooking
	{
		public static void Initialize()
		{
			ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += ItemSlot_Draw;
		}
	}
}