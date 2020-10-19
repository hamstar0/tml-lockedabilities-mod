using System;
using HamstarHelpers.Classes.PlayerData;


namespace LockedAbilities {
	class LockedAbilitiesPlayerCustom : CustomPlayerData {
		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			if( isCurrentPlayer ) {
				var myplayer = this.Player.GetModPlayer<LockedAbilitiesPlayer>();
				myplayer.OnCurrentPlayerEnter();
			}
		}
	}
}
