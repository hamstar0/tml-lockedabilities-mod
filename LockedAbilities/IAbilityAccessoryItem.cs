using System;
using Terraria;


namespace LockedAbilities {
	public interface IAbilityAccessoryItem {
		float WorldGenChestWeight( Chest chest );
		int? GetAddedAccessorySlots( Player player );
		bool EnablesArmorItem( Player player, int slot, Item item );
		bool EnablesMiscItem( Player player, int slot, Item item );
		bool EnablesEquipItem( Player player, Item item );
	}
}
