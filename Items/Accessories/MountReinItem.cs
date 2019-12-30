﻿using System;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities.Items.Accessories {
	public class MountReinItem : ModItem, IAbilityAccessoryItem {
		public const int Width = 22;
		public const int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Mount Rein" );
			this.Tooltip.SetDefault( "Allows mounts" );
		}

		public override void SetDefaults() {
			this.item.width = MountReinItem.Width;
			this.item.height = MountReinItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 50, 0, 0 );
			this.item.rare = 6;
			this.item.accessory = true;
		}


		////////////////

		public int? GetAddedAccessorySlots( Player player ) {
			return null;
		}

		////////////////

		public bool IsArmorItemEnabled( Player player, int slot, Item item ) {
			return false;
		}

		public bool IsMiscItemEnabled( Player player, int slot, Item item ) {
			if( !LockedAbilitiesConfig.Instance.MountReinEnabled ) {
				return false;
			}

			if( item.mountType >= 0 ) {
				return true;
			}
			return false;
		}

		public bool IsEquipItemEnabled( Player player, Item item ) {
			return false;
		}

		////////////////

		public float WorldGenChestWeight( Chest chest ) {
			return LockedAbilitiesConfig.Instance.WorldGenChestImplantMountReinChance;
		}
	}
}
