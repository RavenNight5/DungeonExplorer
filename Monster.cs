using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Monster : Creature, IDamageable
    {
        public override bool IsPlayer => false;
        public override bool IsEnemy => true;
        public override bool IsNPC => false;

        public override string Name { get; set; }
        public override int MaxHealth { get; set; }

        public static int Health { get; set; }

        public Monster(string name, int maxHealth) : base(name, maxHealth)
        {
            Health = maxHealth;
        }

        //public override void AccessInventory()
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Attack()
        //{
        //    throw new NotImplementedException();
        //}

        //public override void EquipItem()
        //{
        //    throw new NotImplementedException();
        //}

        //public override void PassTurn()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
