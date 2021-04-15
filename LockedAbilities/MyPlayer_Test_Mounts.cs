using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using LockedAbilities.Items.Accessories;
using Microsoft.Xna.Framework;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void TestMountState() {
			var config = LockedAbilitiesConfig.Instance;
			if( !config.Get<bool>( nameof(config.MountReinEnabled) ) ) {
				return;
			}

			if( !this.player.mount.Active || this.player.mount.Cart ) {
				return;
			}

			int mountReinType = ModContent.ItemType<MountReinItem>();
			int utilBeltType = ModContent.ItemType<UtilitarianBeltItem>();
			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int lastAccSlot = PlayerItemHelpers.GetFirstVanitySlot( player );

			for( int i = firstAccSlot; i < lastAccSlot; i++ ) {
				Item item = player.armor[i];
				if( item?.active != true || (item.type != mountReinType && item.type != utilBeltType) ) {
					continue;
				}

				return;
			}

			var mountReinItem = ModContent.GetInstance<MountReinItem>();
			var utilBeltItem = ModContent.GetInstance<UtilitarianBeltItem>();
			Main.NewText( "Need " + mountReinItem.item.Name + " or " + utilBeltItem.item.Name + " to equip.", Color.Yellow );

			this.player.mount.Dismount( this.player );
		}
	}
}
