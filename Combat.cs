using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.SqlServer.Server;

namespace DungeonExplorer
{
    public class Combat
    {

        private int player_BaseDamage = 0;
        private int player_CRITDmg = 0;
        private int player_CRITRate = 0;

        private int opportunities = 0;

        private Monster MonsterObject { get; set; }

        private int barLength = 0;
        
        private string marker = "^";
        private int markerPos = 0;

        private static System.Timers.Timer refreshBarTimer;

        public static string Combat_EquippedWeapon = "";
        public static string[] Combat_EquippedWeaponImage = Inventory.InventoryEmptySlot;

        public static string Combat_EquippedBonus = "";
        public static string[] Combat_EquippedBonusImage = Inventory.InventoryEmptySlot;

        private static int weaponEaseOfUse = 10;

        private readonly string[] _barPatterns = new string[] {
        "x - - - x - - - - x - - - < + > - - x",

        "- - - - - - - - x < + > - - - - - - x",

        "x - - - < + > - - - - - x - - - - - -",

        "- - - - - - - - - x - - < + > - - - x",

        "- - < + > - - x - - - x - x - - - - -",

        "- - - - < + > - - - < + > - x - - - -",

        "x - - - - x < + > - - - x - - - - - -"
        };

        private readonly string _markerCharacter = "^";


        public Combat(Monster monster)
        {
            MonsterObject = monster;

            barLength = _barPatterns[0].Length;

            MainCombatScreen();
        }


        public void MainCombatScreen()
        {
            Program.CLEAR_CONSOLE();

            string HealthVisual = "";

            for (int i = 0; i < Player.Health; i += 10)
            {
                HealthVisual += "+ ";
            }

            string playerStats = $@"
          Vs...

    '{Program.NameTemp}'

    Weapon     Bonus
    --── ──--  --── ──--    {Player.NamePlural} Health:
    │{Combat_EquippedWeaponImage[0]}│  │{Combat_EquippedBonusImage[0]}│    ┌───────----- - - -
    │{Combat_EquippedWeaponImage[1]}│  │{Combat_EquippedBonusImage[1]}│    ║ {HealthVisual} ({Player.Health}/{Game.CurrentPlayer.MaxHealth}
    ║{Combat_EquippedWeaponImage[2]}║  ║{Combat_EquippedBonusImage[2]}║    └───────----- - - -
    │{Combat_EquippedWeaponImage[3]}│  │{Combat_EquippedBonusImage[3]}│
    │ {Combat_EquippedWeaponImage[4]}│  │ {Combat_EquippedBonusImage[4]}│
    --─ ! ─--  --─ + ─--

    My Combat Stats
    ╔══─=───────---
    │   {player_BaseDamage}     Base Damage
    ║+[ {player_CRITDmg} ]%  CRIT dmg
    │ [ {player_CRITRate} ]%  Chance of CRIT hit
    ╚══─=───────---


 > Start Attack [Space]

 > Switch Weapon [1]
 > Switch Extra Item [2]

 > Pass Your Turn [P]

 > Help [H]

";

            Console.Write(MonsterObject.MonsterInterface + playerStats);

            void playerAction()
            {
                string playerInput = Game.InputHandler.CombatMainOptions(new string[] { "Spacebar", "97", "98", "P", "H" });  // Enum keys: 97 = 1, 98 = 2

                if (playerInput != null)
                {
                    if (playerInput == "Spacebar")
                    {
                        SubCombatScreen();
                    }
                    ////////////////////////////////////////
                }
                else
                {
                    playerAction();
                }
            }

            playerAction();
            
        }

        private string GetAttackBar()
        {
            Random rand = new Random();

            string randBarPattern = _barPatterns[0];

            if (markerPos == 0)  // Get a new random bar pattern every time the marker is at pos 0
            {
                randBarPattern = _barPatterns[rand.Next(_barPatterns.Length)];
            }

            marker = "";  // Reset the spaces in the marker

            for (int i = 0; i < markerPos; i++)
            {
                marker += " ";  // Re-add a space for each position the marker has moved
            }

            marker += _markerCharacter;  // Add the character ^ at the end

            string bar = $@"
    Your turn to attack [Any Key]:
    ╔══──────────-----      -----──────────═╗
    │ {randBarPattern} │
      {marker}
    ╚══──────────-----      -----──────────═╝

    ({opportunities}/5 opportunities passed | +5% weak spot dmg)

";

            return bar;
        }
        private void SubCombatScreen()
        {
            Program.CLEAR_CONSOLE();

            Console.Write(GetAttackBar());

            //While wait for x * easness of weapon selected...

            opportunities = 0;  // Reset number of opportunities to strike

            while (opportunities < 5)
            {
                markerPos = 0;

                while (!Console.KeyAvailable && markerPos < barLength)
                {
                    Program.CLEAR_CONSOLE();

                    Console.Write(GetAttackBar());

                    System.Threading.Thread.Sleep(300);

                    markerPos += 2;  //Increment the marker's position
                }

                opportunities++;
            }

            if (opportunities >= 5)
            {
                Program.CLEAR_CONSOLE();

                Console.Write($"{opportunities} You took too long... The enemy attacks.\n\n");

                Thread.Sleep(1500);

                EnemyAttack();
            }
            else
            {
                Program.CLEAR_CONSOLE();

                Console.Write("You hit the enemy.\n\n");  /////////////// dependant

                Thread.Sleep(1000);

                PlayerAttack(false, false);  //////////// dependant
            }
        }

        private void EnemyAttack()
        {
            MonsterObject.Attack();
        }

        private void PlayerAttack(bool miss, bool hitWeakSpot)
        {
            Game.CurrentPlayer.Attack(miss, hitWeakSpot);
        }


    }
}
