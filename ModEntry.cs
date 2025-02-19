using StardewModdingAPI;
using StardewValley;
using Microsoft.Xna.Framework;

namespace SummonNPCs
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.ConsoleCommands.Add("summonnpc",
                Helper.Translation.Get("OMEGAlinc.SummonNPCs.CommandDescription"),
                SummonNPC);
        }

        private void SummonNPC(string command, string[] args)
        {
            if (args.Length == 0)
            {
                Monitor.Log(Helper.Translation.Get("OMEGAlinc.SummonNPCs.ErrorUsage"), LogLevel.Info);
                return;
            }

            string npcName = string.Join(" ", args);
            NPC npc = Game1.getCharacterFromName(npcName, false);

            if (npc == null)
            {
                Monitor.Log(string.Format(Helper.Translation.Get("OMEGAlinc.SummonNPCs.ErrorNPCNotFound"), npcName), LogLevel.Warn);
                return;
            }

            if (Game1.player.currentLocation == null)
            {
                Monitor.Log(Helper.Translation.Get("OMEGAlinc.SummonNPCs.ErrorNoPlayerLocation"), LogLevel.Warn);
                return;
            }

            // Get player position
            Farmer player = Game1.player;
            GameLocation location = player.currentLocation;

            // Calculate spawn position (1 tile to the right)
            Vector2 spawnTile = new(player.Tile.X + 1, player.Tile.Y);

            // Move the NPC properly
            Game1.warpCharacter(npc, location, spawnTile);

            Monitor.Log(string.Format(Helper.Translation.Get("OMEGAlinc.SummonNPCs.Success"), npcName), LogLevel.Info);
        }
    }
}
