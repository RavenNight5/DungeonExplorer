// Filename: Monster.cs
using System;
using System.Collections.Generic;
using System.Threading;

namespace DungeonExplorer
{
    public class Monster : Creature
    {
        public override bool IsPlayer => false;
        public override bool IsEnemy => true;
        public override bool IsNPC => false;

        public override string Name { get; set; }
        public override string Plural { get; set; }
        public override int MaxHealth { get; set; }

        public int Health { get; set; }

        public string Species { get; set; }
        public string MonsterInterface { get; set; }
        public string SpecialAbility { get; set; }  // Contains substrings that determin any special attacks or defence moves the monster can take
        public int SpecialAbilityUses { get; set; }

        public int BaseDamage { get; set; }
        public int CRITDamage { get; set; }  // A % that determines the additional damage dealt if the attack is a CRIT HIT
        public int CRITRate { get; set; }  // A % chance out of 100 that the attack will be a CRIT HIT

        public int DefaultBaseDamage = 0;
        public int DefaultCRITDamage = 0;
        public int DefaultCRITRate = 0;

        public Monster(string species, string name, int maxHealth, string specialAbility, int specialAbilityUses, int baseDamage, int critDamage, int critRate) : base(name, null, maxHealth)
        {
            Species = species;

            Name = name;
            Plural = null;

            Health = maxHealth;

            SpecialAbility = specialAbility;
            SpecialAbilityUses = specialAbilityUses;

            BaseDamage = baseDamage;
            CRITDamage = critDamage;
            CRITRate = critRate;

            DefaultBaseDamage = baseDamage;
            DefaultCRITDamage = critDamage;
            DefaultCRITRate = critRate;

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
    |  \▒│▒^▒│▒/  |   │ ------
    |   \▒░▒░▒/   |   │ Ability: Has one opportunity to Steal your Turn.
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
      //▓-▓▓\\        ╚════─════─════=────────────---
    |//|+════\-\  |   ╔══──══=───---
    |+▓/~╤░░░╤~░| |   │   {BaseDamage}     Base Damage
    │▓/░░|▒^▒|░░| │   │ ------
    │o  \░▒▒▒░/   │   │+[ {CRITDamage} ]%  CRIT dmg
    │  /-|▒▒▒|-\  │   │ [ {CRITRate} ]%  Chance of CRIT hit
    | ═|░▒▒▒▒▒░\═ |   │ ------
    | ░░░░▒▒▒░░░░ |   │ Ability: Can double {SpecialAbility} up to three times.
      ░|░░░▒░░░|░     ╚══──══=───---
    │ +|░░░░░░░|+ │
    ╚═──  ─~─  ──═╝
    ";
            }
        }

        public override int Attack(bool miss = false, bool hitWeakSpot = false, List<int> availableDamage = null)
        {
            Program.CLEAR_CONSOLE();

            float result = 0;

            if (!(availableDamage == null) && miss == false)
            {
                int baseDamage = availableDamage[0];
                int critDamage = availableDamage[1];
                int critRate = availableDamage[2];

                Random critRateChance = new Random();
                Random randomD10 = new Random(Guid.NewGuid().GetHashCode());

                int dice1 = randomD10.Next(1, 11);

                if (critRateChance.Next(0, 100) <= critRate)  // CRIT Hit  
                {
                    result = baseDamage + (baseDamage * critDamage / 100);  // Adds the percentage of crit damage to the base damage

                    result += dice1;

                    Console.WriteLine($" {Name} attacks, causing CRIT Damage.\n");
                    Thread.Sleep(200);
                    Console.WriteLine($" [ {baseDamage} ] Base Dmg");
                    Thread.Sleep(125);
                    Console.WriteLine($" +[ {critDamage} ]% CRIT Dmg");
                    Thread.Sleep(100);
                    Console.WriteLine($"   [ {dice1} ] + DICE Damage");
                    Thread.Sleep(50);
                    Console.WriteLine($" ---\n\n [ {(int)Math.Ceiling(result)} ] Total Dmg");
                }
                else  // Regular hit  
                {
                    result = baseDamage;

                    result += dice1;

                    Console.WriteLine($" {Name} attacks.\n");
                    Thread.Sleep(200); 
                    Console.WriteLine($" [ {baseDamage} ] Base Dmg");
                    Thread.Sleep(100);
                    Console.WriteLine($"  [ {dice1} ] + DICE Damage");
                    Thread.Sleep(50);
                    Console.WriteLine($" ---\n\n [ {(int)Math.Ceiling(result)} ] Total Dmg");
                }

                Console.WriteLine($"\n\nPress [any key] to continue.\n");

                Console.ReadKey();
            }

            return (int)Math.Ceiling(result);
        }

        public override void DamageMonster(Monster monster, int dmg)
        {
            if (monster.Health - dmg <= 0)
            {
                monster.Health = 0;
            }
            else
            {
                monster.Health -= dmg;
            }

            AssignInterface(monster.Species);  // Update the monsters interfaces
        }

        public void ResetAllDamageStats()
        {
            BaseDamage = DefaultBaseDamage;
            CRITDamage = DefaultCRITDamage;
            CRITRate = DefaultCRITRate;
        }


        // Static polymorphism to take action on the special abilities

        // Writes a notice using the passed string
        public void DoSpecialAbility(string action)
        {
            Console.WriteLine($@"  ╔═--    ---────── !! ──────---    --═╗
  │                                    │
  │        Special Ability Used!       │
  |                ----                |
  | The Opponent Has:                  |
    {action.ToUpper()}
  |                                    |
  ╚═--     ---───── !! ─────---     --═╝
");
        }

        // Changes the stats of the monster (also uses the written notice method above)
        public void DoSpecialAbility(List<int> changes, string what)  
        {
            DoSpecialAbility($"INCREASED {what.ToUpper()}\n    For ONE TURN");

            if (what.ToUpper().Contains("BASEDAMAGE"))
            {
                BaseDamage += changes[0];
            }
            if (what.ToUpper().Contains("CRITDAMAGE"))
            {
                CRITDamage += changes[1];
            }
            if (what.ToUpper().Contains("CRITRATE"))
            {
                CRITRate += changes[2];
            }
        }
    }
}
