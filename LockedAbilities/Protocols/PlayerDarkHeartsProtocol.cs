using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;


namespace LockedAbilities.Protocols {
	[Serializable]
	class PlayerDarkHeartsProtocol : NetIOBroadcastPayload {
		public static void Broadcast( int darkHearts ) {
			if( Main.netMode != 1 ) { throw new ModHelpersException("Not client.");  }

			var protocol = new PlayerDarkHeartsProtocol( Main.myPlayer, darkHearts );
			NetIO.Broadcast( protocol );
		}



		////////////////

		public int PlayerWho;
		public int DarkHearts;



		////////////////

		private PlayerDarkHeartsProtocol() { }

		private PlayerDarkHeartsProtocol( int playerWho, int darkHearts ) {
			this.PlayerWho = playerWho;
			this.DarkHearts = darkHearts;
		}


		////////////////

		public override bool ReceiveOnServerBeforeRebroadcast( int fromWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<LockedAbilitiesPlayer>( Main.player[this.PlayerWho] );

			myplayer.SetAllowedAccessorySlots( this.DarkHearts );

			return true;
		}

		public override void ReceiveBroadcastOnClient() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<LockedAbilitiesPlayer>( Main.player[this.PlayerWho] );
			myplayer.SetAllowedAccessorySlots( this.DarkHearts );
		}
	}
}
