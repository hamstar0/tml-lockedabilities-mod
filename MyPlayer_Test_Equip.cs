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

		private bool TestEquipAgainstMissingAbilities( ISet<Type> equippedAbilityItemTypes, Item testItem, out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;

			// Test each item against missing abilities
			foreach( (Type missingAbilityItemType, IAbilityAccessoryItem missingAbilityItemTemplate) in mymod.AbilityItemTemplates ) {
				if( equippedAbilityItemTypes.Contains( missingAbilityItemType ) ) {
					continue;
				}

				ModItem missingAbilityModItem = (ModItem)missingAbilityItemTemplate;

				if( missingAbilityItemTemplate.IsEquipItemAnAbility( this.player, testItem) ) {
					alert = "Need " + missingAbilityModItem.item.HoverName + " to equip.";
					return false;
				}
			}

			alert = "";
			return true;
		}
	}
}