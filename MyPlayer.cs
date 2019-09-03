using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace LockedAbilities {
	partial class LockedAbilitiesPlayer : ModPlayer {
		public int HighestAllowedAccessorySlot { get; private set; } = 1;

		public override bool CloneNewInstances => false;



		////////////////

		public override void PreUpdate() {
			var mymod = (LockedAbilitiesMod)this.mod;
			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;

			this.HighestAllowedAccessorySlot = mymod.Config.InitialAccessorySlots + firstAccSlot;

			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories(this.player) + firstAccSlot;
			ISet<Type> abilityItemTypes = new HashSet<Type>();

			// Find equipped ability items
			for( int i = firstAccSlot; i < maxAccSlot; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir || item.modItem == null || !(item.modItem is IAbilityAccessoryItem) ) {
					continue;
				}

				var abilityItem = (IAbilityAccessoryItem)item.modItem;

				int? newMaxAccSlotNull = abilityItem.GetMaxArmorSlot( this.player );
				int newMaxAccSlot = newMaxAccSlotNull.HasValue ?
					newMaxAccSlotNull.Value :
					this.HighestAllowedAccessorySlot;

				this.HighestAllowedAccessorySlot = newMaxAccSlot > this.HighestAllowedAccessorySlot ?
					newMaxAccSlot :
					this.HighestAllowedAccessorySlot;

				abilityItemTypes.Add( item.GetType() );
			}

			this.TestArmorSlots( abilityItemTypes );
			this.TestMiscSlots( abilityItemTypes );
			this.TestEquippedItem( abilityItemTypes );
		}
	}
}