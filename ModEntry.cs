using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using static StardewValley.Minigames.BoatJourney;
using SObject = StardewValley.Object;

namespace TreeShaker
{
    public sealed class ModConfig
    {
        
        public Vector2 ChestCoords = new(69f, 16f);
        public string DefaultCoordinates = "next to the Default farm Mailbox are 69,16.";
        
        public bool CheckForSeeds = true;
        public string seedcheck = "Checks if trees actually have seeds, if false just gives a seed for every tree present.";
        public bool CleanupGroundSeeds = false;
        public string cleanup = "if Enabled, picks up seeds from the ground, preventing more trees from growing.";
    }
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        private ModConfig Config;
        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            helper.Events.GameLoop.DayStarted += this.DayStarted;
        }

        private void DayStarted(object? sender, DayStartedEventArgs e)
        {
            Farm farm = Game1.getFarm();
            if (farm.objects.TryGetValue(Config.ChestCoords, out SObject obj) && obj is Chest chest)
            {
                foreach (Farm location in Game1.locations.OfType<Farm>())
                {
                    List<Vector2> seedsToRemove = new List<Vector2>();
                    foreach (KeyValuePair<Vector2, TerrainFeature> pair in location.terrainFeatures.Pairs)
                    {
                        Vector2 tile = pair.Key;
                        if (pair.Value is not Tree tree)
                            continue;
                        if (this.Config.CheckForSeeds == true & tree.hasSeed.Value == false)
                            continue;
                        {
                            bool added = false;
                            switch (tree.treeType.Value)
                            {
                                case "1":
                                    Item a = chest.addItem(new SObject("309", 1));
                                    if (a == null)
                                    {
                                        added = true;
                                    }
                                    break;
                                case "2":
                                    Item m = chest.addItem(new SObject("310", 1));
                                    if (m == null)
                                    {
                                        added = true;
                                    }
                                    break;
                                case "3":
                                    Item p = chest.addItem(new SObject("311", 1));
                                    if (p == null)
                                    {
                                        added = true;
                                    }
                                    break;
                                case "6":
                                    Item c = chest.addItem(new SObject("88", 1));
                                    if (c == null)
                                    {
                                        added = true;
                                    }
                                    break;
                                case "7":
                                    Item M = chest.addItem(new SObject("891", 1));
                                    if (M == null)
                                    {
                                        added = true;
                                    }
                                    break;
                                case "8":
                                    Item n = chest.addItem(new SObject("292", 1));
                                    if (n == null)
                                    {
                                        added = true;
                                    }
                                    break;
                                case "10":
                                    Item r1 = chest.addItem(new SObject("MossySeed", 1));
                                    if (r1 == null)
                                    {
                                        added = true;
                                    }
                                    break;
                                case "11":
                                    Item r2 = chest.addItem(new SObject("MossySeed", 1));
                                    if (r2 == null)
                                    {
                                        added = true;
                                    }
                                    break;
                                case "12":
                                    Item r3 = chest.addItem(new SObject("MossySeed", 1));
                                    if (r3 == null)
                                    {
                                        added = true;
                                    }
                                    break;
                            }

                            if (added && tree.growthStage.Value != Tree.seedStage)
                            {
                                tree.hasSeed.Value = false;
                            }
                            if (this.Config.CleanupGroundSeeds == true && added && tree.growthStage.Value == Tree.seedStage)
                            {
                                seedsToRemove.Add(tile);
                            }
                        }
                    }
                    foreach (Vector2 seedTile in seedsToRemove)
                    {
                        location.terrainFeatures.Remove(seedTile);
                    }
                }
            }
        }
    }
}