using HamstarHelpers.Helpers.HUD;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities {
	public partial class LockedAbilitiesMod : Mod {
		public override void PostDrawInterface( SpriteBatch sb ) {
			if( Main.playerInventory && Main.EquipPage == 0 ) {	//== 2 is misc page
				this.DrawAccessoryOverlays( sb );
			}
		}


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
