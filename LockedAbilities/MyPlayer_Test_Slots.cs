using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void UpdateMaxAllowedAccessorySlots( Item item ) {
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
