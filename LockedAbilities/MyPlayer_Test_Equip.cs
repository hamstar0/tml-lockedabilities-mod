using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private bool TestEquippable( ISet<Type> abilityItemTypes, Item testItem ) {
			if( testItem == null || testItem.IsAir ) {
				return true;
			}

			string alert;

			// Test equipped item against equipped ability items
			if( !this.TestEquippable(abilityItemTypes, testItem, out alert) ) {
				Main.NewText( alert, Color.Yellow );
				//PlayerItemLibraries.DropInventoryItem( this.player, PlayerItemLibraries.VanillaInventorySelectedSlot );
				//Main.mouseItem = new Item();
				return false;
			}
			
			return true;
		}


		////

		private bool TestEquippable(
					ISet<Type> equippedAbilityEnablingItemTypes,
					Item testItem,
					out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;
			bool? isMissing = null;
			var missingAbilityEnablingModItems = new List<string>();

			// Test each item against available ability-enabling items
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
				alert = "Need " + string.Join( " or ", missingAbilityEnablingModItems ) + " to wield.";
				return false;
			}

			alert = "";
			return true;
		}
	}
}