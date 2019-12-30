using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void TestEquippedItem( ISet<Type> abilityItemTypes ) {
			string alert;

			// Test each item against equipped ability items
			Item item = this.player.inventory[ PlayerItemHelpers.VanillaInventorySelectedSlot ];
			if( item == null || item.IsAir ) {
				return;
			}

			if( !this.TestEquipAgainstMissingAbilities( abilityItemTypes, item, out alert) ) {
				Main.NewText( alert, Color.Yellow );
				PlayerItemHelpers.DropInventoryItem( this.player, PlayerItemHelpers.VanillaInventorySelectedSlot );
				Main.mouseItem = new Item();
				return;
			}
		}


		////

		private bool TestEquipAgainstMissingAbilities( ISet<Type> equippedAbilityEnablingItemTypes, Item testItem, out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;
			bool? isMissing = null;
			var missingAbilityEnablingModItems = new List<string>();

			// Test each item against missing abilities
			foreach( (Type abilityEnablingItemType, IAbilityAccessoryItem abilityEnablingItem) in mymod.AbilityItemSingletons ) {
				bool isEquipped = equippedAbilityEnablingItemTypes.Contains( abilityEnablingItemType );

				if( abilityEnablingItem.EnablesEquipItem( this.player, testItem ) ) {
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
				alert = "Need " + string.Join( " or ", missingAbilityEnablingModItems ) + " to equip.";
				return false;
			}

			alert = "";
			return true;
		}
	}
}