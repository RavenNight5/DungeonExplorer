using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Creature
    {
        public abstract bool IsPlayer { get; }
        public abstract bool IsEnemy { get; }
        public abstract bool IsNPC { get; }

        public abstract string Name { get; set; }
        public abstract int MaxHealth { get; set; }

        public Creature(string name, int maxHealth)
        {
            this.Name = name;
            this.MaxHealth = maxHealth;
        }

        public abstract void Attack(bool miss = false, bool hitWeakSpot = false);
        //public abstract void PassTurn();
        //public abstract void EquipItem();
    }
}
