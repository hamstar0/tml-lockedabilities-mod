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
			this.HighestAllowedAccessorySlot = mymod.Config.InitialAccessorySlots;

			int maxAccSlot = PlayerItemHelpers.GetCurrentVanillaMaxAccessories( Main.LocalPlayer )
					+ PlayerItemHelpers.VanillaAccessorySlotFirst;
			ISet<Type> abilityItemTypes = new HashSet<Type>();

			// Find equipped ability items
			for( int i = PlayerItemHelpers.VanillaAccessorySlotFirst; i < maxAccSlot; i++ ) {
				Item item = this.player.armor[i];
				if( item == null || item.IsAir || item.modItem == null || !( item.modItem is IAbilityAccessoryItem ) ) {
					continue;
				}

				int? newMaxAccSlot = ( (IAbilityAccessoryItem)item ).GetMaxAccessorySlot( this.player );
				this.HighestAllowedAccessorySlot = newMaxAccSlot.HasValue ?
					newMaxAccSlot.Value :
					this.HighestAllowedAccessorySlot;

				abilityItemTypes.Add( item.GetType() );
			}

			this.TestArmorSlots( abilityItemTypes );
			this.TestMiscSlots( abilityItemTypes );
			this.TestEquippedItem( abilityItemTypes );
		}
	}
}