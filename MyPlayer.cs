using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using Terraria.ModLoader.IO;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		public int IntrinsicallyAllowedAccessorySlots { get; private set; } = 1;
		public int AllowedAccessorySlots { get; private set; } = 1;

		public override bool CloneNewInstances => false;



		////////////////

		public override void Initialize() {
			this.IntrinsicallyAllowedAccessorySlots =  LockedAbilitiesConfig.Instance.InitialAccessorySlots;
			this.AllowedAccessorySlots = this.IntrinsicallyAllowedAccessorySlots;
		}

		public override void Load( TagCompound tag ) {
			this.IntrinsicallyAllowedAccessorySlots =  LockedAbilitiesConfig.Instance.InitialAccessorySlots;
			this.AllowedAccessorySlots = this.IntrinsicallyAllowedAccessorySlots;

			if( tag.ContainsKey("highest_acc_slots") ) {
				this.IntrinsicallyAllowedAccessorySlots = tag.GetInt( "highest_acc_slots" );
				this.AllowedAccessorySlots = this.IntrinsicallyAllowedAccessorySlots;
			}
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "highest_acc_slots", this.IntrinsicallyAllowedAccessorySlots }
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
			var abilityItem = (IAbilityAccessoryItem)item.modItem;

			int? addedAccSlots = abilityItem.GetAddedAccessorySlots( this.player );
			int testAddedAccSlots = addedAccSlots.HasValue ? addedAccSlots.Value : 0;
			int testLastAccSlot = testAddedAccSlots + this.IntrinsicallyAllowedAccessorySlots;

			this.AllowedAccessorySlots = this.AllowedAccessorySlots < testLastAccSlot
				? testAddedAccSlots
				: this.AllowedAccessorySlots;
		}


		////////////////

		public void IncreaseAllowedAccessorySlots() {
			this.IntrinsicallyAllowedAccessorySlots += 1;
			this.AllowedAccessorySlots += 1;
		}
	}
}
