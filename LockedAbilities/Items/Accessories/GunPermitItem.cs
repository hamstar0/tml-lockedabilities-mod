﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LockedAbilities.Items.Accessories {
	public class GunPermitItem : ModItem, IAbilityAccessoryItem {
		public const int Width = 22;
		public const int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Gun Permit" );
			this.Tooltip.SetDefault( "Allows equipping guns" );
		}

		public override void SetDefaults() {
			this.item.width = GunPermitItem.Width;
			this.item.height = GunPermitItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 25, 0, 0 );
			this.item.rare = 5;
			this.item.accessory = true;
		}


		////////////////

		public int? GetAddedAccessorySlots( Player player ) {
			return null;
		}

		////////////////

		public bool EnablesArmorItem( Player player, int slot, Item item ) {
			return false;
		}

		public bool EnablesMiscItem( Player player, int slot, Item item ) {
			return false;
		}

		public bool EnablesEquipItem( Player player, Item item ) {
			if( !LockedAbilitiesConfig.Instance.Get<bool>( nameof(LockedAbilitiesConfig.GunPermitEnabled) ) ) {
				return false;
			}

			if( item.ranged && item.useAmmo == AmmoID.Bullet ) {
				return true;
			}
			return false;
		}

		////////////////

		public float WorldGenChestWeight() {
			var config = LockedAbilitiesConfig.Instance;
			return config.Get<float>( nameof(LockedAbilitiesConfig.WorldGenChestImplantGunPermitChance) );
		}
	}
}
