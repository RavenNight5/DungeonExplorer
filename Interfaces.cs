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
        void Damage(int dmg);
    }

    // Weapons, Bonus_Items
    internal interface ICollectible
    {
        bool CanCollect(); 
    }
}
