using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using MrPlagueRaces.Common.Races;

namespace FoxAddonRace.Common.Races.Foxkin
 {
    public class Foxkin : Race
    {         
        public override string RaceSelectIcon => ($"FoxAddonRace/Common/UI/RaceDisplay/FoxkinSelect");
        public override string RaceDisplayMaleIcon => ($"FoxAddonRace/Common/UI/RaceDisplay/FoxkinDisplayMale");
        public override string RaceDisplayFemaleIcon => ($"FoxAddonRace/Common/UI/RaceDisplay/FoxkinDisplayFemale");
        
        public override string RaceDisplayName => "Foxkin Species";
        public override string RaceLore1 => "This race has " + "\ngreat agility" + "\n and movement" + "\n speed";
        public override string RaceLore2 => "The race was unknown" + "\nfor a long time in history" + "\nuntil when the" + "\nGreat Inventor found them";
		public override string RaceAbilityName => "Danger! [i:43]";
		public override string RaceAbilityDescription1 => "|----------------------------------------|";
		public override string RaceAbilityDescription2 => "Your Movement speed increases when";
		public override string RaceAbilityDescription3 => "[c/34EB93:Race Ability Key] is pressed";
		public override string RaceAbilityDescription4 => "[c/FF4F64:(you probably need to bound the key!)]";
		public override string RaceAbilityDescription5 => "|----------------------------------------|";
		public override string RaceAbilityDescription6 => "";
		public override string RaceAdditionalNotesDescription1 => "- [c/34EB93:+3] minion slot";
		public override string RaceAdditionalNotesDescription2 => "- [c/34EB93:+2] sentry slot";
		public override string RaceAdditionalNotesDescription3 => "- [c/34EB93:+40] fall damage resistance";
		public override string RaceAdditionalNotesDescription4 => "";
		public override string RaceAdditionalNotesDescription5 => "";
		public override string RaceAdditionalNotesDescription6 => "";
		public override bool UsesCustomHurtSound => true;
		public override bool UsesCustomDeathSound => false;
		public override bool HasFemaleHurtSound => false;

		public override string RaceHealthDisplayText => "[c/FF4F64:-10%]";
		public override string RaceRegenerationDisplayText => "[c/34EB93:+4]";
		public override string RaceDefenseDisplayText => "[c/FF4F64:-5]";
        public override string RaceMeleeDamageDisplayText => "[c/FF4F64:-20%]";
		public override string RaceRunSpeedDisplayText => "[c/34EB93:+10%]";
        public override string RaceRunAccelerationDisplayText => "[c/34EB93:+20%]";
		public override string RaceMovementSpeedDisplayText => "[c/34EB93:+35%]";
		public override string RaceJumpSpeedDisplayText => "[c/34EB93:+200%]";
		public override string RaceRangedDamageDisplayText => "[c/34EB93:+20%]";

		
		public override string RaceGoodBiomesDisplayText => "Underground Tundra, Tundra ";
		public override string RaceBadBiomesDisplayText => "Underground Desert, Desert";

		public override bool PreHurt(Player player, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			return true;
		}

		public override void PreUpdate(Player player, Mod mod)
		{
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			var _MrPlagueRaces = ModLoader.GetMod("MrPlagueRaces");
			var FoxAddonRace = ModLoader.GetMod("FoxAddonRace");
            if (player.HasBuff(_MrPlagueRaces.BuffType("DetectHurt")) && (player.statLife != player.statLifeMax2))
            {
                if (Main.rand.Next(3) == 1)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, FoxAddonRace.GetSoundSlot(SoundType.Custom, "Sounds/" + this.Name + "_Hurt"));
                }
                else if (Main.rand.Next(3) == 2)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, FoxAddonRace.GetSoundSlot(SoundType.Custom, "Sounds/" + this.Name + "_Hurt2"));
                }
                else
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, FoxAddonRace.GetSoundSlot(SoundType.Custom, "Sounds/" + this.Name + "_Hurt3"));
                }
            }
        }

		public override void Load(Player player)
		{
			//if your custom race has increased health, it should also be added here (the player first joins in with vanilla health by default regardless of what their max health is). Does not need to be done with decreased health
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			if (modPlayer.RaceStats)
			{
				player.statLife -= 50;
			}
		}

		//things that affect the player's stats should be put in ResetEffects
        public override void ResetEffects(Player player)
        {
            //RaceStats is a bool in MrPlagueRaces that decides whether the player's racial changes are enabled or not. Make sure to put gameplay-affecting racial changes in an 'if statement' that detects if RaceStats is true
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            if (modPlayer.RaceStats)
            {
                player.statLifeMax2 -= (player.statLifeMax2 / 10);
                player.lifeRegen += 4;
                player.statDefense -= 5;
                player.meleeDamage -= 0.2f;
                player.magicDamage += 0.2f;
                player.manaCost -= 0.2f;
                player.maxMinions += 3;
                player.maxTurrets += 2;
                player.extraFall += 40;
                player.maxRunSpeed += 0.1f;
                player.runAcceleration += 0.2f;
                player.moveSpeed += 0.35f;
				player.jumpSpeedBoost += 2f;
				player.rangedDamage += 0.20f;
			}
        }

		public override void ProcessTriggers(Player player, Mod mod)
		{
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			if (modPlayer.RaceStats)
			{
				if (MrPlagueRaces.MrPlagueRaces.RacialAbilityHotKey.Current && !player.dead)
				{
					player.AddBuff(3, 7200, true);
				}
			}
		}

		public override void ModifyDrawInfo(Player player, Mod mod, ref PlayerDrawInfo drawInfo)
        {
			//custom race's default color values and clothing styles go here
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            Item familiarshirt = new Item();
            familiarshirt.SetDefaults(ItemID.FamiliarShirt);
            Item familiarpants = new Item();
            familiarpants.SetDefaults(ItemID.FamiliarPants);
            if (modPlayer.resetDefaultColors)
            {
                modPlayer.resetDefaultColors = false;
                player.hairColor = new Color(90, 87, 250);
                player.skinColor = new Color(247, 136, 61);
                player.eyeColor = new Color(209, 149, 0);
				player.shirtColor = new Color(111, 111, 111);
				player.underShirtColor = new Color(193, 193, 193);
				player.pantsColor = new Color(193, 193, 193);
				player.shoeColor = new Color(59, 32, 15);
				player.skinVariant = 3;
				if (player.armor[1].type < ItemID.IronPickaxe && player.armor[2].type < ItemID.IronPickaxe)
				{
					player.armor[1] = familiarshirt;
					player.armor[2] = familiarpants;
				}
			}
		}

		public override void ModifyDrawLayers(Player player, List<PlayerLayer> layers)
		{

			//applying the racial textures
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();

			bool hideChestplate = modPlayer.hideChestplate;
			bool hideLeggings = modPlayer.hideLeggings;

			Main.playerTextures[0, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head");
			Main.playerTextures[0, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2");
			Main.playerTextures[0, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes");
			Main.playerTextures[0, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_1");
			}
			else
			{
				Main.playerTextures[0, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_1");
			}

			Main.playerTextures[0, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_1");
			}
			else
			{
				Main.playerTextures[0, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_1");
			}

			Main.playerTextures[0, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_1");
			}
			else
			{
				Main.playerTextures[0, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_1");
			}

			Main.playerTextures[0, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand");
			Main.playerTextures[0, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[0, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_1");
				Main.playerTextures[0, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_1");
			}
			else
			{
				Main.playerTextures[0, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_1");
				Main.playerTextures[0, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_1");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_1_2");
			}
			else
			{
				Main.playerTextures[0, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_1_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[0, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_1_2");
			}
			else
			{
				Main.playerTextures[0, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_1_2");
			}

			Main.playerTextures[1, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head");
			Main.playerTextures[1, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2");
			Main.playerTextures[1, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes");
			Main.playerTextures[1, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_2");
			}
			else
			{
				Main.playerTextures[1, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_2");
			}

			Main.playerTextures[1, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_2");
			}
			else
			{
				Main.playerTextures[1, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_2");
			}

			Main.playerTextures[1, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_2");
			}
			else
			{
				Main.playerTextures[1, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_2");
			}

			Main.playerTextures[1, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand");
			Main.playerTextures[1, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[1, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_2");
				Main.playerTextures[1, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_2");
			}
			else
			{
				Main.playerTextures[1, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_2");
				Main.playerTextures[1, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_2");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_2_2");
			}
			else
			{
				Main.playerTextures[1, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_2_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[1, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_2_2");
			}
			else
			{
				Main.playerTextures[1, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_2_2");
			}

			Main.playerTextures[2, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head");
			Main.playerTextures[2, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2");
			Main.playerTextures[2, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes");
			Main.playerTextures[2, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_3");
			}
			else
			{
				Main.playerTextures[2, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_3");
			}

			Main.playerTextures[2, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_3");
			}
			else
			{
				Main.playerTextures[2, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_3");
			}

			Main.playerTextures[2, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_3");
			}
			else
			{
				Main.playerTextures[2, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_3");
			}

			Main.playerTextures[2, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand");
			Main.playerTextures[2, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[2, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_3");
				Main.playerTextures[2, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_3");
			}
			else
			{
				Main.playerTextures[2, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_3");
				Main.playerTextures[2, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_3");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_3_2");
			}
			else
			{
				Main.playerTextures[2, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_3_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[2, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_3_2");
			}
			else
			{
				Main.playerTextures[2, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_3_2");
			}

			Main.playerTextures[3, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head");
			Main.playerTextures[3, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2");
			Main.playerTextures[3, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes");
			Main.playerTextures[3, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_4");
			}
			else
			{
				Main.playerTextures[3, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_4");
			}

			Main.playerTextures[3, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_4");
			}
			else
			{
				Main.playerTextures[3, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_4");
			}

			Main.playerTextures[3, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_4");
			}
			else
			{
				Main.playerTextures[3, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_4");
			}

			Main.playerTextures[3, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand");
			Main.playerTextures[3, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[3, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_4");
				Main.playerTextures[3, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_4");
			}
			else
			{
				Main.playerTextures[3, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_4");
				Main.playerTextures[3, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_4");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_4_2");
			}
			else
			{
				Main.playerTextures[3, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_4_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[3, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_4_2");
			}
			else
			{
				Main.playerTextures[3, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_4_2");
			}

			Main.playerTextures[8, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head");
			Main.playerTextures[8, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2");
			Main.playerTextures[8, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes");
			Main.playerTextures[8, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_9");
			}
			else
			{
				Main.playerTextures[8, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_9");
			}

			Main.playerTextures[8, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_9");
			}
			else
			{
				Main.playerTextures[8, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_9");
			}

			Main.playerTextures[8, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_9");
			}
			else
			{
				Main.playerTextures[8, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_9");
			}

			Main.playerTextures[8, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand");
			Main.playerTextures[8, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[8, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_9");
				Main.playerTextures[8, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_9");
			}
			else
			{
				Main.playerTextures[8, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_9");
				Main.playerTextures[8, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_9");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_9_2");
			}
			else
			{
				Main.playerTextures[8, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_9_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[8, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_9_2");
			}
			else
			{
				Main.playerTextures[8, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_9_2");
			}

			Main.playerTextures[4, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head_Female");
			Main.playerTextures[4, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2_Female");
			Main.playerTextures[4, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_Female");
			Main.playerTextures[4, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_5");
			}
			else
			{
				Main.playerTextures[4, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_5");
			}

			Main.playerTextures[4, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_5");
			}
			else
			{
				Main.playerTextures[4, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_5");
			}

			Main.playerTextures[4, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_5");
			}
			else
			{
				Main.playerTextures[4, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_5");
			}

			Main.playerTextures[4, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand_Female");
			Main.playerTextures[4, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[4, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_5");
				Main.playerTextures[4, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_5");
			}
			else
			{
				Main.playerTextures[4, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_5");
				Main.playerTextures[4, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_5");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_5_2");
			}
			else
			{
				Main.playerTextures[4, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_5_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[4, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_5_2");
			}
			else
			{
				Main.playerTextures[4, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_5_2");
			}

			Main.playerTextures[5, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head_Female");
			Main.playerTextures[5, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2_Female");
			Main.playerTextures[5, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_Female");
			Main.playerTextures[5, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_6");
			}
			else
			{
				Main.playerTextures[5, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_6");
			}

			Main.playerTextures[5, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_6");
			}
			else
			{
				Main.playerTextures[5, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_6");
			}

			Main.playerTextures[5, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_6");
			}
			else
			{
				Main.playerTextures[5, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_6");
			}

			Main.playerTextures[5, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand_Female");
			Main.playerTextures[5, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[5, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_6");
				Main.playerTextures[5, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_6");
			}
			else
			{
				Main.playerTextures[5, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_6");
				Main.playerTextures[5, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_6");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_6_2");
			}
			else
			{
				Main.playerTextures[5, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_6_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[5, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_6_2");
			}
			else
			{
				Main.playerTextures[5, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_6_2");
			}

			Main.playerTextures[6, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head_Female");
			Main.playerTextures[6, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2_Female");
			Main.playerTextures[6, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_Female");
			Main.playerTextures[6, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_7");
			}
			else
			{
				Main.playerTextures[6, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_7");
			}

			Main.playerTextures[6, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_7");
			}
			else
			{
				Main.playerTextures[6, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_7");
			}

			Main.playerTextures[6, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_7");
			}
			else
			{
				Main.playerTextures[6, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_7");
			}

			Main.playerTextures[6, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand_Female");
			Main.playerTextures[6, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[6, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_7");
				Main.playerTextures[6, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_7");
			}
			else
			{
				Main.playerTextures[6, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_7");
				Main.playerTextures[6, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_7");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_7_2");
			}
			else
			{
				Main.playerTextures[6, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_7_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[6, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_7_2");
			}
			else
			{
				Main.playerTextures[6, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_7_2");
			}

			Main.playerTextures[7, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head_Female");
			Main.playerTextures[7, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2_Female");
			Main.playerTextures[7, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_Female");
			Main.playerTextures[7, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_8");
			}
			else
			{
				Main.playerTextures[7, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_8");
			}

			Main.playerTextures[7, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_8");
			}
			else
			{
				Main.playerTextures[7, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_8");
			}

			Main.playerTextures[7, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_8");
			}
			else
			{
				Main.playerTextures[7, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_8");
			}

			Main.playerTextures[7, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand_Female");
			Main.playerTextures[7, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[7, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_8");
				Main.playerTextures[7, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_8");
			}
			else
			{
				Main.playerTextures[7, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_8");
				Main.playerTextures[7, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_8");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_8_2");
			}
			else
			{
				Main.playerTextures[7, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_8_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[7, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_8_2");
			}
			else
			{
				Main.playerTextures[7, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_8_2");
			}

			Main.playerTextures[9, 0] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Head_Female");
			Main.playerTextures[9, 1] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_2_Female");
			Main.playerTextures[9, 2] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Eyes_Female");
			Main.playerTextures[9, 3] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_10");
			}
			else
			{
				Main.playerTextures[9, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_10");
			}

			Main.playerTextures[9, 5] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_10");
			}
			else
			{
				Main.playerTextures[9, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_10");
			}

			Main.playerTextures[9, 7] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_10");
			}
			else
			{
				Main.playerTextures[9, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_10");
			}

			Main.playerTextures[9, 9] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Hand_Female");
			Main.playerTextures[9, 10] = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[9, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_10");
				Main.playerTextures[9, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_10");
			}
			else
			{
				Main.playerTextures[9, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_10");
				Main.playerTextures[9, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_10");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_10_2");
			}
			else
			{
				Main.playerTextures[9, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_10_2");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[9, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_10_2");
			}
			else
			{
				Main.playerTextures[9, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_10_2");
			}

			for (int i = 0; i < 133; i++)
			{
				Main.playerHairTexture[i] = ModContent.GetTexture($"FoxAddonRace/Content/RaceTextures/Foxkin/Hair/Foxkin_Hair_{i + 1}");
				Main.playerHairAltTexture[i] = ModContent.GetTexture($"FoxAddonRace/Content/RaceTextures/Foxkin/Hair/Foxkin_HairAlt_{i + 1}");
			}

			Main.ghostTexture = ModContent.GetTexture("FoxAddonRace/Content/RaceTextures/Foxkin/Foxkin_Ghost");
		}
 	}
}