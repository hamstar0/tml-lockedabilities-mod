using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using HamstarHelpers.Helpers.Debug;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void TestArmorSlots( ISet<Type> abilityItemTypes ) {
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( this.player )
					+ PlayerItemHelpers.VanillaAccessorySlotFirst;

			string alert;

			// Test each item against equipped ability items
			for( int slot = 0; slot < maxAccSlot; slot++ ) {
				Item item = this.player.armor[slot];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( !this.TestArmorAgainstMissingAbilities( abilityItemTypes, slot, out alert ) ) {
					Main.NewText( alert, Color.Yellow );
					PlayerItemHelpers.DropEquippedArmorItem( this.player, slot );
					continue;
				}
			}

			this.TestArmorSlotCapacity();
		}

		////

		private void TestArmorSlotCapacity() {
			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( this.player ) + firstAccSlot;

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


		////

		private bool TestArmorAgainstMissingAbilities( ISet<Type> equippedAbilityItemTypes, int slot, out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;

			// Test each item against missing abilities
			foreach( (Type missingAbilityItemType, IAbilityAccessoryItem missingAbilityItemTemplate) in mymod.AbilityItemTemplates ) {
				if( equippedAbilityItemTypes.Contains( missingAbilityItemType ) ) {
					continue;
				}

				var missingAbilityModItem = (ModItem)missingAbilityItemTemplate;
				Item testItem = this.player.armor[slot];

				if( missingAbilityItemTemplate.IsArmorItemAnAbility( this.player, slot, testItem) ) {
					alert = "Need " + missingAbilityModItem.item.HoverName + " to equip.";
					return false;
				}
			}

			alert = "";
			return true;
		}
	}
}