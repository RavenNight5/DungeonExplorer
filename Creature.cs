using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Creature : IDamageable
    {
        public abstract bool IsPlayer { get; }
        public abstract bool IsEnemy { get; }
        public abstract bool IsNPC { get; }

        public abstract string Name { get; set; }
        public abstract string Plural { get; set; }
        public abstract int MaxHealth { get; set; }

        public Creature(string name, string plural, int maxHealth)
        {
            this.Name = name;
            this.Plural = plural;
            this.MaxHealth = maxHealth;
        }

        public virtual void Damage(int dmg)
        {
            Console.WriteLine("No damage was dealt.");  // The default output for IDamageable
        }

        public abstract void Attack(bool miss = false, bool hitWeakSpot = false);

    }
}
