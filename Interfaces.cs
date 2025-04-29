using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // Player, Monster
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
