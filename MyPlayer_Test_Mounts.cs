using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using LockedAbilities.Items.Accessories;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void TestMountState() {
			if( !LockedAbilitiesConfig.Instance.MountReinEnabled ) {
				return;
			}

			if( this.player.mount.Active && !this.player.mount.Cart ) {
				int mountReinType = ModContent.ItemType<MountReinItem>();
				int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
				int lastAccSlot = PlayerItemHelpers.GetFirstVanitySlot( player );

				for( int i = firstAccSlot; i < lastAccSlot; i++ ) {
					Item item = player.armor[i];
					if( item?.active != true || item.type != mountReinType ) {
						continue;
					}

					return;
				}

				this.player.mount.Dismount( this.player );
			}
		}
	}
}
