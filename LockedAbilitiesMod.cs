using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities {
	public partial class LockedAbilitiesMod : Mod {
		public static LockedAbilitiesMod Instance { get; private set; }



		////////////////

		public LockedAbilitiesConfig Config => this.GetConfig<LockedAbilitiesConfig>();

		private IDictionary<Type, IAbilityAccessoryItem> _Abilities = new Dictionary<Type, IAbilityAccessoryItem>();
		public IReadOnlyDictionary<Type, IAbilityAccessoryItem> AbilityItemTemplates { get; }

		public Texture2D DisabledItemTex { get; private set; }




		public LockedAbilitiesMod() {
			LockedAbilitiesMod.Instance = this;
			this.AbilityItemTemplates = new ReadOnlyDictionary<Type, IAbilityAccessoryItem>( this._Abilities );
		}

		////////////////

		public override void Load() {
			if( Main.netMode != 2 ) {   // Not server
				this.DisabledItemTex = ModContent.GetTexture( "Terraria/MapDeath" );
			}
		}

		public override void Unload() {
			LockedAbilitiesMod.Instance = null;
		}


		////////////////

		public void AddAbility( IAbilityAccessoryItem abilityTemplateItem ) {
			this._Abilities[ abilityTemplateItem.GetType() ] = abilityTemplateItem;
		}
	}
}