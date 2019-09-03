using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void TestArmorSlots( ISet<Type> abilityItemTypes ) {
			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( Main.LocalPlayer )
					+ PlayerItemHelpers.VanillaAccessorySlotFirst;

			string alert;

			// Test each item against equipped ability items
			for( int slot = 0; slot < maxAccSlot; slot++ ) {
				Item item = this.player.armor[slot];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( !this.TestArmorAgainstMissingAbilities( abilityItemTypes, slot, out alert) ) {
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


		////

		private bool TestArmorAgainstMissingAbilities( ISet<Type> equippedAbilityItemTypes, int slot, out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;

			// Test each item against missing abilities
			foreach( (Type abilityItemType, IAbilityAccessoryItem abilityItemTemplate) in mymod.AbilityItemTemplates ) {
				if( equippedAbilityItemTypes.Contains( abilityItemType ) ) {
					continue;
				}

				ModItem abilityModItem = (ModItem)abilityItemTemplate;
				Item testItem = this.player.armor[slot];

				if( !abilityItemTemplate.IsArmorItemAnAbility( this.player, slot, testItem) ) {
					alert = "Need " + abilityModItem.item.HoverName + " to equip.";
					return false;
				}
			}

			alert = "";
			return true;
		}
	}
}