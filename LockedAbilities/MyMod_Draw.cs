using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using ModLibsCore.Libraries.TModLoader;
using ModLibsGeneral.Libraries.HUD;
using ModLibsGeneral.Libraries.Players;

namespace LockedAbilities {
	public partial class LockedAbilitiesMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Inventory" ) );
			if( idx == -1 ) { return; }

			GameInterfaceDrawMethod locksUI = () => {
				if( Main.playerInventory && Main.EquipPage == 0 ) { //== 2 is misc page
					this.DrawDisabledAccessorySlotOverlays( Main.spriteBatch );
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


		private void DrawDisabledAccessorySlotOverlays( SpriteBatch sb ) {
			var myplayer = TmlLibraries.SafelyGetModPlayer<LockedAbilitiesPlayer>( Main.LocalPlayer );
			if( myplayer.TotalAllowedAccessorySlots < 0 ) {
				return;
			}

			int firstAccSlot = PlayerItemLibraries.VanillaAccessorySlotFirst;
			int maxAcc = PlayerItemLibraries.GetCurrentVanillaMaxAccessories(Main.LocalPlayer) + firstAccSlot;
			int myMaxAcc = myplayer.TotalAllowedAccessorySlots;

			for( int i=firstAccSlot; i<maxAcc; i++ ) {
				if( (i - firstAccSlot) < myMaxAcc ) {
					continue;
				}

				var pos = HUDElementLibraries.GetVanillaAccessorySlotScreenPosition( i - firstAccSlot );
				pos.X += 8;
				pos.Y += 8;

				sb.Draw( this.DisabledItemTex, pos, Color.White );
			}
		}
	}
}
