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

			this.TestMountState();

			// Find equipped ability items
			for( int i = firstAccSlot; i < maxAccSlot; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir || item.modItem == null || !( item.modItem is IAbilityAccessoryItem ) ) {
					continue;
				}

				this.TestMaxAllowedAccessorySlots( item );

				equippedAbilityItemTypes.Add( item.modItem.GetType() );
			}

			this.TestArmorSlots( equippedAbilityItemTypes );
			this.TestMiscSlots( equippedAbilityItemTypes );
			this.TestEquippedItem( equippedAbilityItemTypes );
		}
	}
}
