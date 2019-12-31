using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private bool TestEquipItem( ISet<Type> abilityItemTypes, Item item ) {
			if( item == null || item.IsAir ) {
				return true;
			}

			string alert;

			// Test equipped item against equipped ability items
			if( !this.TestEquipAgainstMissingAbilities( abilityItemTypes, item, out alert) ) {
				Main.NewText( alert, Color.Yellow );
				PlayerItemHelpers.DropInventoryItem( this.player, PlayerItemHelpers.VanillaInventorySelectedSlot );
				Main.mouseItem = new Item();
				return false;
			}
			
			return true;
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