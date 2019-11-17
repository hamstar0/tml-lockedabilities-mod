using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;


namespace LockedAbilities {
	public partial class LockedAbilitiesMod : Mod {
		public static LockedAbilitiesMod Instance { get; private set; }

		public static LockedAbilitiesConfig Config => ModContent.GetInstance<LockedAbilitiesConfig>();



		////////////////

		private IDictionary<Type, IAbilityAccessoryItem> _AbilityItemTemplates = new Dictionary<Type, IAbilityAccessoryItem>();
		public IReadOnlyDictionary<Type, IAbilityAccessoryItem> AbilityItemTemplates { get; }

		public Texture2D DisabledItemTex { get; private set; }




		public LockedAbilitiesMod() {
			LockedAbilitiesMod.Instance = this;
			this.AbilityItemTemplates = new ReadOnlyDictionary<Type, IAbilityAccessoryItem>( this._AbilityItemTemplates );
		}

		////////////////

		public override void Load() {
			if( Main.netMode != 2 ) {   // Not server
				this.DisabledItemTex = ModContent.GetTexture( "Terraria/MapDeath" );
			}

			this.LoadChestImplantMod();
		}

		////

		public override void Unload() {
			LockedAbilitiesMod.Instance = null;
		}


		////////////////

		public void AddAbility( IAbilityAccessoryItem abilityTemplateItem ) {
			this._AbilityItemTemplates[ abilityTemplateItem.GetType() ] = abilityTemplateItem;
		}
	}
}