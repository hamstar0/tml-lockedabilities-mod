using System;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;
using HamstarHelpers.Helpers.TModLoader;


namespace LockedAbilities.Protocols {
	class PlayerDarkHeartsProtocol : PacketProtocolSentToEither {
		public static void Sync( int fromPlayerWho, int darkHearts ) {
			if( Main.netMode != 1 ) { throw new ModHelpersException("Not client.");  }

			var protocol = new PlayerDarkHeartsProtocol( fromPlayerWho, darkHearts );

			protocol.SendToServer( true );
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

		protected override void ReceiveOnServer( int fromWho ) {
			int darkHearts = this.DarkHearts;
			var myplayer = TmlHelpers.SafelyGetModPlayer<LockedAbilitiesPlayer>( Main.player[this.PlayerWho] );

			myplayer.SetAllowedAccessorySlots( darkHearts );

			this.SendToClient( -1, fromWho );

			for( int i=0; i<Main.maxPlayers; i++ ) {
				Player player = Main.player[i];
				if( player?.active != true || i == fromWho ) { continue; }

				var myotherplayer = TmlHelpers.SafelyGetModPlayer<LockedAbilitiesPlayer>( Main.player[i] );

				this.PlayerWho = i;
				this.DarkHearts = myotherplayer.InternalAllowedAccessorySlots;

				this.SendToClient( fromWho, -1 );
			}

			this.PlayerWho = fromWho;
			this.DarkHearts = darkHearts;
		}

		protected override void ReceiveOnClient() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<LockedAbilitiesPlayer>( Main.player[this.PlayerWho] );
			myplayer.SetAllowedAccessorySlots( this.DarkHearts );
		}
	}
}
