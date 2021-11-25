using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using LockedAbilities.Recipes;


namespace LockedAbilities {
	public partial class LockedAbilitiesMod : Mod {
		public static LockedAbilitiesMod Instance { get; private set; }



		////////////////

		private IDictionary<Type, IAbilityAccessoryItem> _AbilityItemTemplates = new Dictionary<Type, IAbilityAccessoryItem>();
		public IReadOnlyDictionary<Type, IAbilityAccessoryItem> AbilityItemSingletons { get; }

		public Texture2D DisabledItemTex { get; private set; }



		////////////////

		public LockedAbilitiesMod() {
			LockedAbilitiesMod.Instance = this;
			this.AbilityItemSingletons = new ReadOnlyDictionary<Type, IAbilityAccessoryItem>( this._AbilityItemTemplates );
		}

		////////////////

		public override void Load() {
			LockedAbilitiesMod.Instance = this;

			if( Main.netMode != 2 ) {   // Not server
				this.DisabledItemTex = ModContent.GetTexture( "Terraria/MapDeath" );
			}
		}

		////

		public override void Unload() {
			LockedAbilitiesMod.Instance = null;
		}


		////////////////

		public override void AddRecipes() {
			var utBeltRecipe = new UtilitarianBeltRecipe();
			utBeltRecipe.AddRecipe();
		}


		////////////////

		public void AddAbility( IAbilityAccessoryItem abilityTemplateItem ) {
			this._AbilityItemTemplates[ abilityTemplateItem.GetType() ] = abilityTemplateItem;
		}
	}
}