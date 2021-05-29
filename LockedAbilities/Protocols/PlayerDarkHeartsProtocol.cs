using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.TModLoader;
using ModLibsCore.Services.Network.SimplePacket;


namespace LockedAbilities.Protocols {
	[Serializable]
	class PlayerDarkHeartsProtocol : SimplePacketPayload {
		public static void Broadcast( int darkHearts ) {
			if( Main.netMode != 1 ) { throw new ModLibsException("Not client.");  }

			var payload = new PlayerDarkHeartsProtocol( Main.myPlayer, darkHearts );
			SimplePacket.SendToServer( payload );
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

		public override void ReceiveOnServer( int fromWho ) {
			var myplayer = TmlLibraries.SafelyGetModPlayer<LockedAbilitiesPlayer>( Main.player[this.PlayerWho] );

			myplayer.SetAllowedAccessorySlots( this.DarkHearts );
		}

		public override void ReceiveOnClient() {
			var myplayer = TmlLibraries.SafelyGetModPlayer<LockedAbilitiesPlayer>( Main.player[this.PlayerWho] );
			myplayer.SetAllowedAccessorySlots( this.DarkHearts );
		}
	}
}
