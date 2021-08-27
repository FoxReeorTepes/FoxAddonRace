using Terraria.ModLoader;

namespace FoxAddonRace
 {
	public class FoxAddonRace : Mod
	{
		public static FoxAddonRace Instance { get; private set; }
        public override void Load()
        {
            MrPlagueRaces.Core.Loadables.LoadableManager.Autoload(this);
        }
        public override void Unload()
        {
            MrPlagueRaces.Common.Races.RaceLoader.Races.Clear();
            MrPlagueRaces.Common.Races.RaceLoader.RacesByLegacyIds.Clear();
            MrPlagueRaces.Common.Races.RaceLoader.RacesByFullNames.Clear();
		}
	}
}