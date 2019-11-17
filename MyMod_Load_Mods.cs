using ChestImplants;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Tiles;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities {
	public partial class LockedAbilitiesMod : Mod {
		public static int GetRandomAccessoryForLocation( Chest chest, bool isLocked ) {

		}



		////////////////

		private void LoadChestImplantMod() {
			ChestImplantsAPI.AddCustomImplanter( ( context, chest ) => {
				bool isLocked = false;
				Tile mytile = Main.tile[chest.x, chest.y];
				string currentChestType;
				if( !TileFrameHelpers.VanillaChestTypeNamesByFrame.TryGetValue( mytile.frameX / 36, out currentChestType ) ) {
					throw new ModHelpersException( "Could not find chest frame" );
				}

				if( !ChestImplanter.IsChestMatch(context, currentChestType) ) {
					return;
				} else {
					if( currentChestType == "Locked Shadow Chest" ) {
						isLocked = true;
					}
				}

				for( int i=chest.item.Length; i>0; i-- ) {
					chest.item[i] = chest.item[i-1];
				}
				chest.item[0] = new Item();
				chest.item[0].SetDefaults( LockedAbilitiesMod.GetRandomAccessoryForLocation(chest, isLocked) );
			} );
		}
	}
}