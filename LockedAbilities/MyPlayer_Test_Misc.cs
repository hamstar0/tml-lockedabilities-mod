using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsGeneral.Libraries.Items;
using ModLibsGeneral.Libraries.Players;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void TestMiscSlots( ISet<Type> abilityItemTypes ) {
			int maxMiscSlot = this.player.miscEquips.Length;

			string alert;

			// Test each item against equipped ability items
			for( int slot = 0; slot < maxMiscSlot; slot++ ) {
				Item item = this.player.miscEquips[ slot ];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( !this.TestMiscAgainstMissingAbilities( abilityItemTypes, slot, out alert) ) {
					Main.NewText( alert, Color.Yellow );
					PlayerItemLibraries.DropEquippedMiscItem( this.player, slot, 0 );
					continue;
				}
			}
		}


		////

		private bool TestMiscAgainstMissingAbilities(
					ISet<Type> equippedAbilityEnablingItemTypes,
					int slot,
					out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;
			bool? isMissing = null;
			var missingAbilityEnablingModItems = new List<string>();

			// Test each item against missing abilities
			foreach( (Type abilityEnablingItemType, IAbilityAccessoryItem abilityEnablingItem) in mymod.AbilityItemSingletons ) {
				bool isEquipped = equippedAbilityEnablingItemTypes.Contains( abilityEnablingItemType );
				Item miscItem = this.player.miscEquips[slot];

				if( abilityEnablingItem.EnablesMiscItem( this.player, slot, miscItem ) ) {
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