using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities {
	class LockedAbilitiesPlayer : ModPlayer {
		public ISet<Type> AbilityAccessories { get; private set; } = new HashSet<Type>();

		public override bool CloneNewInstances => false;



		////////////////

		public override void PreUpdate() {
			var mymod = (LockedAbilitiesMod)this.mod;
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( Main.LocalPlayer )
					+ PlayerItemHelpers.VanillaAccessorySlotFirst;
			int myMaxAccSlot = mymod.Config.InitialAccessorySlots;

			ISet<Type> abilityItemTypes = new HashSet<Type>();

			// Find equipped ability items
			for( int i = PlayerItemHelpers.VanillaAccessorySlotFirst; i < maxAccSlot; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir || item.modItem == null || !(item.modItem is IAbilityAccessoryItem) ) {
					continue;
				}

				int? newMaxAccSlot = ((IAbilityAccessoryItem)item).GetMaxAccessorySlot( this.player );
				myMaxAccSlot = newMaxAccSlot.HasValue ? newMaxAccSlot.Value : myMaxAccSlot;

				abilityItemTypes.Add( item.GetType() );
			}

			// Test each item against equipped ability items
			for( int slot = 0; slot < maxAccSlot; slot++ ) {
				Item item = this.player.armor[slot];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( !this.TestPresentAbility(abilityItemTypes, slot) ) {
					PlayerItemHelpers.DropEquippedArmorItem( this.player, slot );
					continue;
				}
				if( !this.TestMissingAbility( abilityItemTypes, slot ) ) {
					PlayerItemHelpers.DropEquippedArmorItem( this.player, slot );
					continue;
				}
			}

			// Test max accessory slots
			for( int slot = myMaxAccSlot; slot < maxAccSlot; slot++ ) {
				Item item = this.player.armor[slot];
				if( item == null || item.IsAir ) {
					continue;
				}

				PlayerItemHelpers.DropEquippedArmorItem( this.player, slot );
			}
		}


		private bool TestPresentAbility( ISet<Type> equippedAbilityItemTypes, int slot ) {
			var mymod = (LockedAbilitiesMod)this.mod;

			// Test each item against equipped abilities
			foreach( Type equippedAbilityItemType in equippedAbilityItemTypes ) {
				IAbilityAccessoryItem abilityItemTemplate = mymod.AbilityItemTemplates[equippedAbilityItemType];
				Item testItem = this.player.armor[slot];

				if( abilityItemTemplate.TestItemDisabled(this.player, slot, testItem) ) {
					PlayerItemHelpers.DropEquippedArmorItem( this.player, slot );
					return false;
				}
			}

			return true;
		}

		private bool TestMissingAbility( ISet<Type> equippedAbilityItemTypes, int slot ) {
			var mymod = (LockedAbilitiesMod)this.mod;
			int _ = mymod.Config.InitialAccessorySlots;

			// Test each item against missing abilities
			foreach( (Type abilityItemType, IAbilityAccessoryItem abilityItemTemplate) in mymod.AbilityItemTemplates ) {
				if( equippedAbilityItemTypes.Contains( abilityItemType ) ) {
					continue;
				}

				Item testItem = this.player.armor[slot];

				if( abilityItemTemplate.TestItemEnabled( this.player, slot, testItem) ) {
					return false;
				}
			}

			return true;
		}
	}
}