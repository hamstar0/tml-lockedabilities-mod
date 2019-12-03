using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		private void TestMiscSlots( ISet<Type> abilityItemTypes ) {
			int maxMiscSlot = this.player.miscEquips.Length;

			string alert;

			// Test each item against equipped ability items
			for( int slot = 0; slot < maxMiscSlot; slot++ ) {
				Item item = this.player.miscEquips[slot];
				if( item == null || item.IsAir ) {
					continue;
				}

				if( !this.TestMiscAgainstMissingAbilities( abilityItemTypes, slot, out alert) ) {
					Main.NewText( alert, Color.Yellow );
					PlayerItemHelpers.DropEquippedMiscItem( this.player, slot );
					continue;
				}
			}
		}


		////

		private bool TestMiscAgainstMissingAbilities( ISet<Type> equippedAbilityItemTypes, int slot, out string alert ) {
			var mymod = (LockedAbilitiesMod)this.mod;

			// Test each item against missing abilities
			foreach( (Type missingAbilityItemType, IAbilityAccessoryItem missingAbilityItemTemplate) in mymod.AbilityItemSingletons ) {
				if( equippedAbilityItemTypes.Contains( missingAbilityItemType ) ) {
					continue;
				}

				ModItem missingAbilityModItem = (ModItem)missingAbilityItemTemplate;
				Item testItem = this.player.miscEquips[slot];

				if( missingAbilityItemTemplate.IsMiscItemAnAbility( this.player, slot, testItem) ) {
					alert = "Need " + missingAbilityModItem.item.HoverName + " to equip.";
					return false;
				}
			}

			alert = "";
			return true;
		}
	}
}