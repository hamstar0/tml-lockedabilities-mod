using HamstarHelpers.Helpers.TModLoader;
using LockedAbilities.Items;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace LockedAbilities {
	public partial class LockedAbilitiesMod : Mod {
		private void LoadMods() {
			if( ModLoader.GetMod( "ChestImplants" ) != null ) {
				ChestImplants.ChestImplantsAPI.AddCustomImplanter( ( context, chest ) => {
					UnifiedRandom rand = TmlHelpers.SafelyGetRand();
					if( rand.NextFloat() > LockedAbilitiesMod.Config.WorldGenChestImplantChance ) {
						return;
					}

					switch( context ) {
					case "Locked Gold Chest":
					case "Shadow Chest":
					case "Jungle Chest":
					case "Corruption Chest":
					case "Crimson Chest":
					case "Hallowed Chest":
					case "Frozen Chest":
					case "Lihzahrd Chest":
						return;
					}

					int itemType = ModContent.ItemType<AccessoryAccessoryItem>();

					ChestImplants.ChestImplanter.PrependItemToChest( chest, itemType, 1 );
				} );
			}
		}
	}
}