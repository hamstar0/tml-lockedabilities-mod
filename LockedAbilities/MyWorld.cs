using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.World.Generation;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsGeneral.Libraries.World.Chests;
using LockedAbilities.Items.Accessories;
using LockedAbilities.Items.Consumable;


namespace LockedAbilities {
	public partial class AbilityItemChestsPass : GenPass {
		private static IEnumerable<(ModItem myitem, float chance)> GetAbilityItemWeights() {
			var config = LockedAbilitiesConfig.Instance;
			var itemBack = ModContent.GetInstance<BackBraceItem>();
			var itemBoot = ModContent.GetInstance<BootLacesItem>();
			var itemFly = ModContent.GetInstance<FlyingCertificateItem>();
			var itemGrap = ModContent.GetInstance<GrappleHarnessItem>();
			var itemGun = ModContent.GetInstance<GunPermitItem>();
			var itemMount = ModContent.GetInstance<MountReinItem>();
			var itemSafe = ModContent.GetInstance<SafetyHarnessItem>();
			var itemDark = ModContent.GetInstance<DarkHeartPieceItem>();

			if( config.Get<bool>( nameof(config.BackBraceEnabled) ) ) {
				yield return ( itemBack, itemBack.WorldGenChestWeight() );
			}
			if( config.Get<bool>( nameof(config.BootLacesEnabled) ) ) {
				yield return ( itemBoot, itemBoot.WorldGenChestWeight() );
			}
			if( config.Get<bool>( nameof(config.FlyingCertificateEnabled) ) ) {
				yield return ( itemFly, itemFly.WorldGenChestWeight() );
			}
			if( config.Get<bool>( nameof(config.GrappleHarnessEnabled) ) ) {
				yield return ( itemGrap, itemGrap.WorldGenChestWeight() );
			}
			if( config.Get<bool>( nameof(config.GunPermitEnabled) ) ) {
				yield return ( itemGun, itemGun.WorldGenChestWeight() );
			}
			if( config.Get<bool>( nameof(config.MountReinEnabled) ) ) {
				yield return ( itemMount, itemMount.WorldGenChestWeight() );
			}
			if( config.Get<bool>( nameof(config.SafetyHarnessEnabled) ) ) {
				yield return ( itemSafe, itemSafe.WorldGenChestWeight() );
			}

			if( config.Get<float>( nameof(config.WorldGenChestImplantDarkHeartPieceChance) ) > 0f ) {
				yield return ( itemDark, itemDark.WorldGenChestWeight() );
			}
		}



		////////////////

		public AbilityItemChestsPass() : base( "Locked Ability Items Chest Implants", 1f ) { }

		////////////////

		public override void Apply( GenerationProgress progress ) {
			var config = LockedAbilitiesConfig.Instance;
			UnifiedRandom rand = TmlLibraries.SafelyGetRand();

			float implantChance = config.Get<float>( nameof(config.WorldGenChestImplantChance) );
			if( implantChance <= 0f ) {
				return;
			}

			var any = AbilityItemChestsPass.GetAbilityItemWeights()
				.Select( kv =>  (kv.chance, new ChestFillItemDefinition(kv.myitem.item.type)) )
				.ToArray();

			var chestDef = new ChestTypeDefinition(
				anyOfTiles: new (int?, int?)[0],
				alsoUndergroundChests: true,
				alsoDungeonAndTempleChests: false
			);

			var fillDef = new ChestFillDefinition( any, implantChance );

			WorldChestLibraries.AddToWorldChests( fillDef, chestDef );
		}
	}




	public partial class LockedAbilitiesWorld : ModWorld {
		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight ) {
			tasks.Add( new AbilityItemChestsPass() );
		}
	}
}