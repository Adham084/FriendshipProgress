using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using HarmonyLib;
using System.Collections.Generic;

namespace FriendshipProgress
{
    public class FriendshipProgressMod : Mod
    {
        public override void Entry(IModHelper helper)
        {
            Harmony harmony = new(ModManifest.UniqueID);

            harmony.Patch(
                AccessTools.Method(typeof(SocialPage), "drawNPCSlot"),
                postfix: new HarmonyMethod(typeof(FriendshipProgressMod), nameof(DrawNPCSlotPostfix))
            );
        }

        public static void DrawNPCSlotPostfix(SpriteBatch b, int i, SocialPage __instance, List<ClickableTextureComponent> ___sprites)
        {
            b.DrawString(
                Game1.smallFont,
                "Current Heart: " + (Game1.player.friendshipData[(string)__instance.names[i]].Points % 250) + "/250",
                new Vector2(__instance.xPositionOnScreen + 316, ___sprites[i].bounds.Y + 4),
                Game1.textColor
            );
        }
    }
}