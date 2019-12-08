using HamstarHelpers.Helpers.Players;
using LockedAbilities.Items.Accessories;
using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities {
	class MyProjectile : GlobalProjectile {
		public override bool? CanUseGrapple( int projType, Player player ) {
			if( !LockedAbilitiesConfig.Instance.GrappleHarnessEnabled ) {
				return null;
			}

			int grappleHarnessType = ModContent.ItemType<GrappleHarnessItem>();
			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int lastAccSlot = PlayerItemHelpers.GetFirstVanitySlot( player );

			for( int i=firstAccSlot; i<lastAccSlot; i++ ) {
				Item item = player.armor[i];
				if( item?.active != true || item.type != grappleHarnessType ) {
					continue;
				}

				return true;
			}

			return false;
		}
	}
}
