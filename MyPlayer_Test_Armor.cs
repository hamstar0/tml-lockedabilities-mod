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
		private void TestArmorSlots( ISet<Type> equippedAbilityEnablingItemTypes ) {
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( this.player )
					+ PlayerItemHelpers.VanillaAccessorySlotFirst;

			string alert;

			// Test each item against equipped ability items
			for( int slot = 0; slot < maxAccSlot; slot++ ) {
				Item item = this.player.armor[slot];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( !this.TestArmorAgainstMissingAbilities( equippedAbilityEnablingItemTypes, slot, out alert ) ) {
					Main.NewText( alert, Color.Yellow );
					PlayerItemHelpers.DropEquippedArmorItem( this.player, slot, 0 );
					continue;
				}
			}

			this.TestAccessorySlotCapacity();
		}


		////

		private bool TestArmorAgainstMissingAbilities( ISet<Type> equippedAbilityEnablingItemTypes, int slot, out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;
			bool? isMissing = null;
			var missingAbilityEnablingModItems = new List<string>();

			// Test each item against missing abilities
			foreach( (Type abilityEnablingItemType, IAbilityAccessoryItem abilityEnablingItem) in mymod.AbilityItemSingletons ) {
				bool isEquipped = equippedAbilityEnablingItemTypes.Contains( abilityEnablingItemType );
				Item equippedArmorItem = this.player.armor[slot];

				if( abilityEnablingItem.EnablesArmorItem(this.player, slot, equippedArmorItem) ) {
					if( isEquipped ) {
						isMissing = false;
						break;
					}

					isMissing = true;

					var abilityEnablingModItem = (ModItem)abilityEnablingItem;
					missingAbilityEnablingModItems.Add( abilityEnablingModItem.item.Name );
				}
			}

			if( isMissing.HasValue && isMissing.Value ) {
				alert = "Need " + string.Join(" or ", missingAbilityEnablingModItems) + " to equip.";
				return false;
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
				PlayerItemHelpers.DropEquippedArmorItem( this.player, slot, 0 );
				break;
			}
		}
	}
}
