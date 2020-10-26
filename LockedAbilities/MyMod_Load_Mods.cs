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
				var itemBack = ModContent.GetInstance<BackBraceItem>();
				var itemBoot = ModContent.GetInstance<BootLacesItem>();
				var itemFly = ModContent.GetInstance<FlyingCertificateItem>();
				var itemGrap = ModContent.GetInstance<GrappleHarnessItem>();
				var itemGun = ModContent.GetInstance<GunPermitItem>();
				var itemMount = ModContent.GetInstance<MountReinItem>();
				var itemSafe = ModContent.GetInstance<SafetyHarnessItem>();
				var itemDark = ModContent.GetInstance<DarkHeartPieceItem>();

				if( config.Get<bool>( nameof(config.BackBraceEnabled) ) ) {
					yield return ( itemBack, itemBack.WorldGenChestWeight(chest) );
				}
				if( config.Get<bool>( nameof(config.BootLacesEnabled) ) ) {
					yield return ( itemBoot, itemBoot.WorldGenChestWeight(chest) );
				}
				if( config.Get<bool>( nameof(config.FlyingCertificateEnabled) ) ) {
					yield return ( itemFly, itemFly.WorldGenChestWeight(chest) );
				}
				if( config.Get<bool>( nameof(config.GrappleHarnessEnabled) ) ) {
					yield return ( itemGrap, itemGrap.WorldGenChestWeight(chest) );
				}
				if( config.Get<bool>( nameof(config.GunPermitEnabled) ) ) {
					yield return ( itemGun, itemGun.WorldGenChestWeight(chest) );
				}
				if( config.Get<bool>( nameof(config.MountReinEnabled) ) ) {
					yield return ( itemMount, itemMount.WorldGenChestWeight(chest) );
				}
				if( config.Get<bool>( nameof(config.SafetyHarnessEnabled) ) ) {
					yield return ( itemSafe, itemSafe.WorldGenChestWeight(chest) );
				}

				if( config.Get<float>( nameof(config.WorldGenChestImplantDarkHeartPieceChance) ) > 0f ) {
					yield return ( itemDark, itemDark.WorldGenChestWeight(chest) );
				}
			}

			//

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			IEnumerable<(ModItem myitem, float chance)> weights = getWeight();
			float totalWeight = weights
				.SafeSelect( item => item.chance )
				.Sum();

			float randVal = rand.NextFloat() * totalWeight;
			float climbingWeights = 0f;

			foreach( (ModItem myitem, float chance) in weights ) {
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
				float implantChance = config.Get<float>( nameof(config.WorldGenChestImplantChance) );
				if( rand.NextFloat() > implantChance ) {
					return;
				}

				int randItemType = LockedAbilitiesMod.GetRandomAccessoryForLocation( chest, isLocked );
				if( randItemType == -1 ) {
					//throw new ModHelpersException( "Could not pick random item for chest" );
					return;
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