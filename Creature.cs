// Filename: Creature.cs
using System;
using System.Collections.Generic;

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

        // Interfaces
        public virtual void DamageMonster(Monster monster, int dmg)
        {
            Console.WriteLine("No damage was dealt to the monster.");  // The default output for IDamageable, DamageMonster
        }
        public virtual void DamagePlayer(int dmg, int healthDefense = 0, bool lifeShield = false)
        {
            Console.WriteLine("No damage was dealt to you.");  // The default output for IDamageable, DamagePlayer
        }
        //

        public abstract int Attack(bool miss = false, bool hitWeakSpot = false, List<int> availableDamage = null);

    }
}
