using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.Players;
using LockedAbilities.Items.Accessories;
using HamstarHelpers.Helpers.Debug;


namespace LockedAbilities {
	class MyProjectile : GlobalProjectile {
		public override bool? CanUseGrapple( int projType, Player player ) {
			if( LockedAbilitiesConfig.Instance.GrappleRequiresChainAmount > 0 ) {
				int idx = ItemFinderHelpers.FindIndexOfFirstOfItemInCollection(
					player.inventory,
					new HashSet<int> { ItemID.Chain }
				);

				if( idx == -1 ) {
					return false;
				}
			}

			if( !LockedAbilitiesConfig.Instance.GrappleHarnessEnabled ) {
				return null;
			}

			int grappleHarnessType = ModContent.ItemType<GrappleHarnessItem>();
			int utilBeltType = ModContent.ItemType<UtilitarianBeltItem>();
			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int lastAccSlot = PlayerItemHelpers.GetFirstVanitySlot( player );

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
			if( LockedAbilitiesConfig.Instance.GrappleRequiresChainAmount > 0 ) {
				int idx = ItemFinderHelpers.FindIndexOfFirstOfItemInCollection(
					player.inventory,
					new HashSet<int> { ItemID.Chain }
				);

				if( idx == -1 ) {
					Main.NewText( "No chains available for grappling.", Color.Yellow );
					return;
				}

				PlayerItemHelpers.RemoveInventoryItemQuantity(
					player,
					ItemID.Chain,
					LockedAbilitiesConfig.Instance.GrappleRequiresChainAmount
				);
			}
		}
	}
}
