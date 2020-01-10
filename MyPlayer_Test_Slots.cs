using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using Terraria.ModLoader.IO;
using LockedAbilities.Items.Accessories;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void TestMaxAllowedAccessorySlots( Item item ) {
			if( this.InternalAllowedAccessorySlots == -1 ) {
				this.TotalAllowedAccessorySlots = -1;
				return;
			}

			var abilityItem = (IAbilityAccessoryItem)item.modItem;

			int? addedAccSlots = abilityItem.GetAddedAccessorySlots( this.player );
			int testAddedAccSlots = addedAccSlots.HasValue ? addedAccSlots.Value : 0;
			int testLastAccSlot = testAddedAccSlots + this.InternalAllowedAccessorySlots;

			this.TotalAllowedAccessorySlots = this.TotalAllowedAccessorySlots < testLastAccSlot
				? testAddedAccSlots
				: this.TotalAllowedAccessorySlots;
		}


		////////////////

		public void IncreaseAllowedAccessorySlots() {
			if( this.InternalAllowedAccessorySlots < 0 ) {
				return;
			}

			this.InternalAllowedAccessorySlots += 1;
			this.TotalAllowedAccessorySlots += 1;
		}

		public void SetAllowedAccessorySlots( int slots ) {
			if( this.InternalAllowedAccessorySlots < 0 ) {
				return;
			}

			int oldInternalAmt = this.InternalAllowedAccessorySlots;

			this.InternalAllowedAccessorySlots = slots;
			this.TotalAllowedAccessorySlots = (this.TotalAllowedAccessorySlots - oldInternalAmt) + slots;
		}
	}
}
