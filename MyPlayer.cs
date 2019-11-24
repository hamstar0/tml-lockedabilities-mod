using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using Terraria.ModLoader.IO;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		public int InternalAllowedAccessorySlots { get; private set; } = 1;
		public int TotalAllowedAccessorySlots { get; private set; } = 1;

		public override bool CloneNewInstances => false;



		////////////////

		public override void Initialize() {
			this.InternalAllowedAccessorySlots =  LockedAbilitiesConfig.Instance.InitialAccessorySlots;
			this.TotalAllowedAccessorySlots = this.InternalAllowedAccessorySlots;
		}

		public override void Load( TagCompound tag ) {
			this.InternalAllowedAccessorySlots =  LockedAbilitiesConfig.Instance.InitialAccessorySlots;
			this.TotalAllowedAccessorySlots = this.InternalAllowedAccessorySlots;

			if( tag.ContainsKey("highest_acc_slots") ) {
				this.InternalAllowedAccessorySlots = tag.GetInt( "highest_acc_slots" );
				this.TotalAllowedAccessorySlots = this.InternalAllowedAccessorySlots;
			}
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "highest_acc_slots", this.InternalAllowedAccessorySlots }
			};
		}

		////////////////

		public override void PreUpdate() {
			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( this.player ) + firstAccSlot;
			ISet<Type> equippedAbilityItemTypes = new HashSet<Type>();

			// Find equipped ability items
			for( int i = firstAccSlot; i < maxAccSlot; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir || item.modItem == null || !( item.modItem is IAbilityAccessoryItem ) ) {
					continue;
				}

				this.UpdateMaxAllowedAccessorySlots( item );

				equippedAbilityItemTypes.Add( item.modItem.GetType() );
			}

			this.TestArmorSlots( equippedAbilityItemTypes );
			this.TestMiscSlots( equippedAbilityItemTypes );
			this.TestEquippedItem( equippedAbilityItemTypes );
		}

		////

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
	}
}
