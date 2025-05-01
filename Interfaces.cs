// Filename: Interfaces.cs

namespace DungeonExplorer
{
    // Player, Monster (Handled through the abstract Creature class)
    internal interface IDamageable
    {
        void DamageMonster(Monster monster, int dmg);
        void DamagePlayer(int dmg, int healthDefense = 0, bool lifeShield = false);
    }

    // Weapons, Bonus_Items
    internal interface ICollectible
    {
        bool CanCollect(); 
    }
}
