using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


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

			this.TestAccessorySlotCapacity();
		}


		////

		private bool TestArmorAgainstMissingAbilities( ISet<Type> equippedAbilityItemTypes, int slot, out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;

			// Test each item against missing abilities
			foreach( (Type neededAbilityItemType, IAbilityAccessoryItem neededAbilityDef) in mymod.AbilityItemSingletons ) {
				// Ignore ability enabling items themselves
				if( equippedAbilityItemTypes.Contains( neededAbilityItemType ) ) {
					continue;
				}

				var neededAbilityModItem = (ModItem)neededAbilityDef;
				Item testItem = this.player.armor[slot];

				if( neededAbilityDef.IsArmorItemAnAbility( this.player, slot, testItem) ) {
					alert = "Need " + neededAbilityModItem.item.HoverName + " to equip.";
					return false;
				}
			}

			alert = "";
			return true;
		}


		////

		private void TestAccessorySlotCapacity() {
			if( this.TotalAllowedAccessorySlots < 0 ) {
				return;
			}

			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( this.player ) + firstAccSlot;

			// Test max accessory slots
			for( int slot = (firstAccSlot + this.TotalAllowedAccessorySlots); slot < maxAccSlot; slot++ ) {
				Item item = this.player.armor[slot];
				if( item == null || item.IsAir ) {
					continue;
				}

				Main.NewText( "Invalid accessory slot.", Color.Yellow );
				PlayerItemHelpers.DropEquippedArmorItem( this.player, slot );
				break;
			}
		}
	}
}
