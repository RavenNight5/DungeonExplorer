using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonExplorer
{
    public class Monster : Creature, IDamageable
    {
        public override bool IsPlayer => false;
        public override bool IsEnemy => true;
        public override bool IsNPC => false;

        public override string Name { get; set; }
        public override int MaxHealth { get; set; }

        public int Health { get; set; }

        public string Species { get; set; }
        public string MonsterInterface { get; set; }
        public string SpecialAbility { get; set; }  // Contains substrings that determin any special attacks or defence moves the monster can take
        public int SpecialAbilityUses { get; set; }

        public int BaseDamage { get; set; }
        public int CRITDamage { get; set; }  // A % that determines the additional damage dealt if the attack is a CRIT HIT
        public int CRITRate { get; set; }  // A % chance out of 100 that the attack will be a CRIT HIT

        public Monster(string species, string name, int maxHealth, string specialAbility, int specialAbilityUses, int baseDamage, int critDamage, int critRate) : base(name, maxHealth)
        {
            Species = species;
            Name = name;
            Health = maxHealth;

            SpecialAbility = specialAbility;
            SpecialAbilityUses = specialAbilityUses;

            BaseDamage = baseDamage;
            CRITDamage = critDamage;
            CRITRate = critRate;

            AssignInterface(species);
        }

        // Based on the monster, create an interface for them during a battle (with the assigned values)
        private void AssignInterface(string species)
        {
            string HealthVisual = "";

            for (int i = 0; i < Health; i += 10)
            {
                HealthVisual += "+ ";
            }

            if (species == "Dragon")
            {
                MonsterInterface = $@"
    '{Name}' | Species: {species}

    ╔═──  ─~─  ──═╗   ╔════─════─════=────────────---
    │ (         ) │   │ Health: {HealthVisual} [ {Health}/{MaxHealth} ]
      ░\       /░     ╚════─════─════=────────────---
    |  ░\^   ^/░  |   ╔══──══=───---
    |   ▒░) (░▒   |   │   {BaseDamage}     Base Damage
    │   \▒▓ ▓▒/   │   │ ------
    │   )▓▓^▓▓(   │   │+[ {CRITDamage} ]%  CRIT dmg
    │ ~)\╤▓▒▓╤/(~ │   │ [ {CRITRate} ]%  Chance of CRIT hit
    |  \▒│░^░│▒/  |   │ ------
    |   \░▓▒▓░/   |   │ Ability: Has one opportunity to steal your turn.
         |vvv|        ╚══──══=───---
    │     \V/     │
    ╚═──  ─~─  ──═╝
    ";
            }
            else if (species == "Gnome")
            {
                MonsterInterface = $@"
    '{Name}' | Species: {species}

    ╔═──  ─~─  ──═╗   ╔════─════─════=────────────---
    │  /════+     │   │ Health: {HealthVisual} [ {Health}/{MaxHealth} ]
      // -  \\        ╚════─════─════=────────────---
    |//|+════\-   |   ╔══──══=───---
    |+//~╖░░░╓~░  |   │   {BaseDamage}     Base Damage
    │|/░░▒▒~▒▒░░  │   │ ------
    │o  ░░░▒░░░   │   │+[ {CRITDamage} ]%  CRIT dmg
    │  /-|▒▒▒|-\  │   │ [ {CRITRate} ]%  Chance of CRIT hit
    | ═|░▒▒▒▒▒░\═ |   │ ------
    | ░▒▒▒▒▒▒▒▒▒░ |   │ Ability: Can double Base Damage up to three times.
      ░|▒▒▒▒▒▒▒|░     ╚══──══=───---
    │ +|▒▒▒▒▒▒▒|+ │
    ╚═──  ─~─  ──═╝
    ";
            }
        }

        public override void Attack(bool miss = false, bool hitWeakSpot = false)
        {
            
        }

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
