using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.Items;
using ModLibsGeneral.Libraries.Players;
using LockedAbilities.Items.Accessories;


namespace LockedAbilities {
	class MyProjectile : GlobalProjectile {
		public override bool? CanUseGrapple( int projType, Player player ) {
			var config = LockedAbilitiesConfig.Instance;

			if( config.Get<int>( nameof(LockedAbilitiesConfig.GrappleRequiresChainAmount) ) > 0 ) {
				int idx = ItemFinderLibraries.FindIndexOfFirstOfItemInCollection(
					player.inventory,
					new HashSet<int> { ItemID.Chain }
				);

				if( idx == -1 ) {
					return false;
				}
			}
			
			if( !config.Get<bool>( nameof(config.GrappleHarnessEnabled) ) ) {
				return null;
			}

			int grappleHarnessType = ModContent.ItemType<GrappleHarnessItem>();
			int utilBeltType = ModContent.ItemType<UtilitarianBeltItem>();
			int firstAccSlot = PlayerItemLibraries.VanillaAccessorySlotFirst;
			int lastAccSlot = PlayerItemLibraries.GetFirstVanitySlot( player );

			for( int i=firstAccSlot; i<lastAccSlot; i++ ) {
				Item item = player.armor[i];
				if( item?.active != true || (item.type != grappleHarnessType && item.type != utilBeltType) ) {
					continue;
				}

				return true;
			}
			
			return false;
		}


		public override void UseGrapple( Player player, ref int type ) {
			var config = LockedAbilitiesConfig.Instance;
			int chainAmt = config.Get<int>( nameof(LockedAbilitiesConfig.GrappleRequiresChainAmount) );

			if( chainAmt > 0 ) {
				int idx = ItemFinderLibraries.FindIndexOfFirstOfItemInCollection(
					player.inventory,
					new HashSet<int> { ItemID.Chain }
				);

				if( idx == -1 ) {
					Main.NewText( "No chains available for grappling.", Color.Yellow );
					return;
				}

				PlayerItemLibraries.RemoveInventoryItemQuantity(
					player,
					ItemID.Chain,
					chainAmt
				);
			}
		}
	}
}
