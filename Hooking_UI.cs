using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent;

namespace UnclutteredUI
{
	public static partial class Hooking
	{
		private delegate void ItemSlot_Draw_Del(ref Texture2D texture, ref Color color, int context, int slot, Vector2 position, SpriteBatch spriteBatch);

		private static void ItemSlot_Draw(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);
			ILLabel label = cursor.DefineLabel();

			if (cursor.TryGotoNext(MoveType.AfterLabel, i => i.MatchLdcI4(10), i => i.MatchBge(out _), i => i.MatchLdloc(0), i => i.MatchLdfld<Player>("selectedItem")))
			{
				cursor.Index -= 6;
				cursor.MoveAfterLabels();

				cursor.Emit(OpCodes.Ldloca, 7);
				cursor.Emit(OpCodes.Ldloca, 8);
				cursor.Emit(OpCodes.Ldarg, 2);
				cursor.Emit(OpCodes.Ldarg, 3);
				cursor.Emit(OpCodes.Ldarg, 4);
				cursor.Emit(OpCodes.Ldarg, 0);

				cursor.EmitDelegate<ItemSlot_Draw_Del>((ref Texture2D texture, ref Color color, int context, int slot, Vector2 position, SpriteBatch spriteBatch) =>
				{
					Main.spriteBatch.Draw(TextureAssets.InventoryBack.Value, position, null, Color.White, 0f, Vector2.Zero, Main.inventoryScale, SpriteEffects.None, 0f);

					Vector2 pos = position + TextureAssets.InventoryBack.Value.Size() * Main.inventoryScale;
					
					Main.spriteBatch.Draw(TextureAssets.Cursors[3].Value, pos, null, Color.White, 0f, new Vector2(20f), Main.inventoryScale * 0.9f, SpriteEffects.None, 0f);
				});

				cursor.Emit(OpCodes.Ldc_I4, 1);
				cursor.Emit(OpCodes.Stloc, 9);
				
				cursor.Emit(OpCodes.Br, label);
			}

			if (cursor.TryGotoNext(MoveType.AfterLabel, i => i.MatchLdloc(1), i => i.MatchLdfld<Item>("type"))) cursor.MarkLabel(label);
		}
	}
}