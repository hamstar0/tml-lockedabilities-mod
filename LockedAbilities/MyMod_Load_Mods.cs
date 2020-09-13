using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.TModLoader;
using LockedAbilities.Items.Accessories;
using LockedAbilities.Items.Consumable;
using ChestImplants;


namespace LockedAbilities {
	public partial class LockedAbilitiesMod : Mod {
		public static int GetRandomAccessoryForLocation( Chest chest, bool isLocked ) {
			var config = LockedAbilitiesConfig.Instance;

			IEnumerable<(ModItem myitem, float chance)> getWeight() {
				var item1 = ModContent.GetInstance<BackBraceItem>();
				var item2 = ModContent.GetInstance<BootLacesItem>();
				var item3 = ModContent.GetInstance<FlyingCertificateItem>();
				var item4 = ModContent.GetInstance<GrappleHarnessItem>();
				var item5 = ModContent.GetInstance<GunPermitItem>();
				var item6 = ModContent.GetInstance<MountReinItem>();
				var item7 = ModContent.GetInstance<SafetyHarnessItem>();
				var item8 = ModContent.GetInstance<DarkHeartPieceItem>();
				yield return ( item1, item1.WorldGenChestWeight(chest) );
				yield return ( item2, item2.WorldGenChestWeight(chest) );
				yield return ( item3, item3.WorldGenChestWeight(chest) );
				yield return ( item4, item4.WorldGenChestWeight(chest) );
				yield return ( item5, item5.WorldGenChestWeight(chest) );
				yield return ( item6, item6.WorldGenChestWeight(chest) );
				yield return ( item7, item7.WorldGenChestWeight(chest) );

				if( config.Get<float>( nameof(LockedAbilitiesConfig.WorldGenChestImplantDarkHeartPieceChance) ) > 0 ) {
					yield return ( item8, item8.WorldGenChestWeight(chest) );
				}
			}

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			float totalWeight = getWeight()
				.SafeSelect( item => item.chance )
				.Sum();

			float randVal = rand.NextFloat() * totalWeight;
			float climbingWeights = 0f;

			foreach( (ModItem myitem, float chance) in getWeight() ) {
				climbingWeights += chance;
				if( randVal < climbingWeights ) {
					return myitem.item.type;
				}
			}

			return -1;
		}



		////////////////

		private void LoadChestImplantMod() {
			ChestImplantsAPI.AddCustomImplanter( "LockedAbilities", ( context, chest ) => {
				bool isLocked = false;
				Tile mytile = Main.tile[chest.x, chest.y];

				string currentChestType;
				if( !TileFrameHelpers.VanillaChestTypeNamesByFrame.TryGetValue(mytile.frameX / 36, out currentChestType) ) {
					throw new ModHelpersException( "Could not find chest frame" );
				}

				if( !ChestImplanter.IsChestMatch(context, currentChestType) ) {
					return;
				} else {
					if( currentChestType == "Locked Shadow Chest" ) {
						isLocked = true;
					}
				}

				var config = LockedAbilitiesConfig.Instance;
				UnifiedRandom rand = TmlHelpers.SafelyGetRand();
				float implantChance = config.Get<float>( nameof( LockedAbilitiesConfig.WorldGenChestImplantChance ) );
				if( rand.NextFloat() > implantChance ) {
					return;
				}

				int randItemType = LockedAbilitiesMod.GetRandomAccessoryForLocation( chest, isLocked );
				if( randItemType == -1 ) {
					throw new ModHelpersException( "Could not pick random item for chest" );
				}

				for( int i=chest.item.Length-1; i>0; i-- ) {
					chest.item[i] = chest.item[i-1];
				}
				chest.item[0] = new Item();
				chest.item[0].SetDefaults( randItemType );
			} );
		}
	}
}