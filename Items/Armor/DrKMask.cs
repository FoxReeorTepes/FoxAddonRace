using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace FoxAddonRace.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class DrKMask : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("If you familiar with changed, you know who the owner of this mask");
			DisplayName.SetDefault("Dr.K' s Mask");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 100;
			item.rare = ItemRarityID.Red;
			item.defense = 2;
		}		
	}
}