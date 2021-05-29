using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.PlayerData;


namespace LockedAbilities {
	class LockedAbilitiesPlayerCustom : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( isCurrentPlayer && Main.netMode == NetmodeID.MultiplayerClient ) {
				var myplayer = this.Player.GetModPlayer<LockedAbilitiesPlayer>();
				myplayer.OnCurrentPlayerEnter();
			}
		}
	}
}
