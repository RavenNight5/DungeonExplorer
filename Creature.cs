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

        public virtual void DamageMonster(Monster monster, int dmg)
        {
            Console.WriteLine("No damage was dealt to the monster.");  // The default outputs for IDamageable
        }
        public virtual void DamagePlayer(int dmg)
        {
            Console.WriteLine("No damage was dealt to you.");
        }

        public abstract int Attack(bool miss = false, bool hitWeakSpot = false, List<int> availableDamage = null);

    }
}
