using System;
using System.ComponentModel;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LockedAbilities.Items {
	class SafetyHarnessItem : ModItem, IAbilityAccessoryItem {
		public static int Width = 22;
		public static int Height = 18;



		////////////////

		public override void SetStaticDefaults() {
			var mymod = (LockedAbilitiesMod)this.mod;
			mymod.AddAbility( this );

			this.DisplayName.SetDefault( "Safety Harness" );
			this.Tooltip.SetDefault( "Allows equipping protective items" );
		}

		public override void SetDefaults() {
			this.item.width = SafetyHarnessItem.Width;
			this.item.height = SafetyHarnessItem.Height;
			this.item.maxStack = 1;
			this.item.value = Item.buyPrice( 0, 10, 0, 0 );
			this.item.rare = 4;
			this.item.accessory = true;
		}


		////////////////

		public int? GetMaxAccessorySlot( Player player ) {
			return null;
		}

		////////////////

		public bool IsArmorItemAnAbility( Player player, int slot, Item item ) {
			switch( item.type ) {
			case ItemID.AdhesiveBandage:
			case ItemID.AnkhCharm:
			case ItemID.AnkhShield:
			case ItemID.ArmorBracing:
			case ItemID.ArmorPolish:
			case ItemID.Bezoar:
			case ItemID.Blindfold:
			case ItemID.CountercurseMantra:
			case ItemID.FastClock:
			case ItemID.HandWarmer:
			case ItemID.MedicatedBandage:
			case ItemID.Megaphone:
			case ItemID.Nazar:
			case ItemID.ObsidianSkull:
			case ItemID.ThePlan:
			case ItemID.TrifoldMap:
			case ItemID.Vitamins:
			case ItemID.PocketMirror:
			case ItemID.BlackBelt:
			case ItemID.BrainOfConfusion:
			case ItemID.CobaltShield:
			case ItemID.CrossNecklace:
			case ItemID.FleshKnuckles:
			case ItemID.FrozenTurtleShell:
			case ItemID.LavaCharm:
			case ItemID.ObsidianShield:
			case ItemID.PaladinsShield:
			case ItemID.RoyalGel:
			case ItemID.Shackle:
			case ItemID.StarVeil:
			case ItemID.WormScarf:
				return true;
			}

			if( item.accessory && item.modItem != null ) {
				var attributes = item.modItem.GetType()
					.GetCustomAttributes( typeof(DescriptionAttribute), false );

				foreach( var attribute in attributes ) {
					string[] descriptions = ((DescriptionAttribute)attribute).Description.Split( ',' );
					if( Array.IndexOf( descriptions, "Defensive" ) != -1 ) {
						return true;
					}
				}
			}

			return false;
		}

		public bool IsMiscItemAnAbility( Player player, int slot, Item item ) {
			return false;
		}

		public bool IsEquipItemAnAbility( Player player, Item item ) {
			return false;
		}
	}
}
