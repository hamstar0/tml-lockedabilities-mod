using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace LockedAbilities {
	class LockedAbilitiesPlayer : ModPlayer {
		public int HighestAllowedAccessorySlot { get; private set; } = 1;

		public override bool CloneNewInstances => false;



		////////////////

		public override void PreUpdate() {
			var mymod = (LockedAbilitiesMod)this.mod;
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( Main.LocalPlayer )
					+ PlayerItemHelpers.VanillaAccessorySlotFirst;
			this.HighestAllowedAccessorySlot = mymod.Config.InitialAccessorySlots;

			ISet<Type> abilityItemTypes = new HashSet<Type>();
			string alert;

			// Find equipped ability items
			for( int i = PlayerItemHelpers.VanillaAccessorySlotFirst; i < maxAccSlot; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir || item.modItem == null || !(item.modItem is IAbilityAccessoryItem) ) {
					continue;
				}

				int? newMaxAccSlot = ((IAbilityAccessoryItem)item).GetMaxAccessorySlot( this.player );
				this.HighestAllowedAccessorySlot = newMaxAccSlot.HasValue ?
					newMaxAccSlot.Value :
					this.HighestAllowedAccessorySlot;

				abilityItemTypes.Add( item.GetType() );
			}

			// Test each item against equipped ability items
			for( int slot = 0; slot < maxAccSlot; slot++ ) {
				Item item = this.player.armor[slot];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( !this.TestPresentAbility(abilityItemTypes, slot, out alert) ) {
					Main.NewText( alert, Color.Yellow );
					PlayerItemHelpers.DropEquippedArmorItem( this.player, slot );
					continue;
				}
				if( !this.TestMissingAbility(abilityItemTypes, slot, out alert) ) {
					Main.NewText( alert, Color.Yellow );
					PlayerItemHelpers.DropEquippedArmorItem( this.player, slot );
					continue;
				}
			}

			// Test max accessory slots
			for( int slot = this.HighestAllowedAccessorySlot; slot < maxAccSlot; slot++ ) {
				Item item = this.player.armor[slot];
				if( item == null || item.IsAir ) {
					continue;
				}

				Main.NewText( "Invalid accessory slot.", Color.Yellow );
				PlayerItemHelpers.DropEquippedArmorItem( this.player, slot );
				break;
			}
		}


		private bool TestPresentAbility( ISet<Type> equippedAbilityItemTypes, int slot, out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;

			// Test each item against equipped abilities
			foreach( Type equippedAbilityItemType in equippedAbilityItemTypes ) {
				IAbilityAccessoryItem abilityItemTemplate = mymod.AbilityItemTemplates[equippedAbilityItemType];
				Item testItem = this.player.armor[slot];

				if( abilityItemTemplate.TestItemDisabled(this.player, slot, testItem) ) {
					alert = ((ModItem)abilityItemTemplate).DisplayName + " prohibits this.";
					return false;
				}
			}

			alert = "";
			return true;
		}

		private bool TestMissingAbility( ISet<Type> equippedAbilityItemTypes, int slot, out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;
			int _ = mymod.Config.InitialAccessorySlots;

			// Test each item against missing abilities
			foreach( (Type abilityItemType, IAbilityAccessoryItem abilityItemTemplate) in mymod.AbilityItemTemplates ) {
				if( equippedAbilityItemTypes.Contains( abilityItemType ) ) {
					continue;
				}

				Item testItem = this.player.armor[slot];

				if( abilityItemTemplate.TestItemEnabled( this.player, slot, testItem) ) {
					alert = "Need " + ((ModItem)abilityItemTemplate).DisplayName + " to equip.";
					return false;
				}
			}

			alert = "";
			return true;
		}
	}
}