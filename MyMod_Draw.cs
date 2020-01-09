using HamstarHelpers.Helpers.HUD;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace LockedAbilities {
	public partial class LockedAbilitiesMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( idx == -1 ) { return; }

			GameInterfaceDrawMethod locksUI = () => {
				if( Main.playerInventory && Main.EquipPage == 0 ) { //== 2 is misc page
					this.DrawAccessoryOverlays( Main.spriteBatch );
				}
				return true;
			};

			var tradeLayer = new LegacyGameInterfaceLayer( "LockedAbilities: Accessory Locks", locksUI, InterfaceScaleType.UI );
			layers.Insert( idx + 1, tradeLayer );
		}

		/*public override void PostDrawInterface( SpriteBatch sb ) {
			if( Main.playerInventory && Main.EquipPage == 0 ) {	//== 2 is misc page
				this.DrawAccessoryOverlays( sb );
			}
		}*/


		private void DrawAccessoryOverlays( SpriteBatch sb ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<LockedAbilitiesPlayer>( Main.LocalPlayer );
			if( myplayer.TotalAllowedAccessorySlots < 0 ) {
				return;
			}

			int firstAccSlot = PlayerItemHelpers.VanillaAccessorySlotFirst;
			int maxAcc = PlayerItemHelpers.GetCurrentVanillaMaxAccessories(Main.LocalPlayer) + firstAccSlot;
			int myMaxAcc = myplayer.TotalAllowedAccessorySlots;

			for( int i=firstAccSlot; i<maxAcc; i++ ) {
				if( (i - firstAccSlot) < myMaxAcc ) {
					continue;
				}

				var pos = HUDElementHelpers.GetVanillaAccessorySlotScreenPosition( i - firstAccSlot );
				pos.X += 8;
				pos.Y += 8;

				sb.Draw( this.DisabledItemTex, pos, Color.White );
			}
		}
	}
}
