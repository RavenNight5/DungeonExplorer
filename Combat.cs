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
        private Random rand = new Random();

        private string randBarPattern = "";

        private string weaponStatus = "Choose";
        private string bonusItemStatus = "Choose";

        private int player_BaseDamage = 0;
        private int player_CRITDmg = 0;
        private int player_CRITRate = 0;

        private int attacksMade = 0;
        private int turnsPassed = 0;
        private int opportunities = 0;

        private bool notPassing = true;

        private Monster MonsterObject { get; set; }

        private int barLength = 0;
        
        private string marker = "^";
        private int markerPos = 0;

        public static bool InCombat = false;

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

            randBarPattern = _barPatterns[0];
            barLength = _barPatterns[0].Length;
            
            MainCombatScreen();
        }


        public void MainCombatScreen()
        {
            Program.CLEAR_CONSOLE();

            InCombat = true;

            notPassing = true;

            string HealthVisual = "";

            for (int i = 0; i < Player.Health; i += 10)
            {
                HealthVisual += "+ ";
            }

            if (Combat_EquippedWeapon == "")
            {
                weaponStatus = "Choose";
            }
            else
            {
                weaponStatus = "Switch";
            }

            if (Combat_EquippedBonus == "")
            {
                bonusItemStatus = "Choose";
            }
            else
            {
                bonusItemStatus = "Switch";
            }

            string playerStats = $@"
          Vs...

    '{Program.NameTemp}' | Species: Cleaner

    Weapon     Bonus
    --── ──--  --── ──--    {Program.NameTemp}{Program.TempPlural} Health:
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

";

            Console.Write(MonsterObject.MonsterInterface);

            System.Threading.Thread.Sleep(300);

            Console.WriteLine(playerStats);

            System.Threading.Thread.Sleep(150);

            Console.WriteLine($@"

 > Start Attack [Space]
   | Total Attacks Made: {attacksMade}
   | Turns Passed: {turnsPassed}

 > {weaponStatus} Weapon [1]
 > {bonusItemStatus} Bonus Item [2]

 > Pass Your Turn [P]

 > Help [H]
");

            void playerAction()
            {
                string playerInput = Game.InputHandler.CombatMainOptions(new string[] { "Spacebar", "D1", "D2", "P", "H" });

                if (playerInput == "Spacebar")
                {
                    attacksMade += 1;

                    SubCombatScreen();
                }
                else if (playerInput == "D1")
                {
                    Game.CurrentPlayer.DisplayInventory(Item.ItemTypeIndex[0]);  // Weapon
                }
                else if (playerInput == "D2")
                {
                    Game.CurrentPlayer.DisplayInventory(Item.ItemTypeIndex[1]);  // Bonus Item
                }
                else if (playerInput == "P" && notPassing == true)
                {
                    Program.CLEAR_CONSOLE();

                    notPassing = false;

                    turnsPassed += 1;

                    Console.WriteLine("You passed your turn.\n\nPress [any key] to continue.");

                    Console.ReadKey();

                    MainCombatScreen();
                }
                else if (playerInput == "H")
                {
                    HelpScreen();
                }
            }
            playerAction();
            
        }

        private string GetAttackBar()
        {
            if (markerPos <= 0)  // Get a new random bar pattern every time the marker is at pos 0
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
            
            bool attacked = false;

            while (opportunities < 5 && attacked == false)
            {
                markerPos = 0;

                while (markerPos < barLength)
                {
                    while (!Console.KeyAvailable)
                    {
                        Program.CLEAR_CONSOLE();

                        Console.Write(GetAttackBar());

                        System.Threading.Thread.Sleep(weaponEaseOfUse * 10);

                        markerPos += 2;  //Increment the marker's position
                    }

                    attacked = true;

                    result();
                }

                opportunities += 1;
            }

            void result()
            {
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
            
        }

        private void EnemyAttack()
        {
            MonsterObject.Attack();
        }

        private void PlayerAttack(bool miss, bool hitWeakSpot)
        {
            Game.CurrentPlayer.Attack(miss, hitWeakSpot);
        }

        private void HelpScreen()
        {
            Program.CLEAR_CONSOLE();

            Console.Write(@"                               Combat Help
                               -──~ + ~──-

 Before starting an attack:
 --------------------------
     > make sure you have an equipped WEAPON and BONUS ITEM (if available)
     > Check your stats (WEAPONS will affect Base Damage and CRIT Damage)

 During an attack:
 -----------------

     x - - - x - - - - x - - - < + > - - x
                 ^

     A bar (see above) will be shown with a marker  ^  displayed below.

     This marker will move across the bar at a speed relevant to the ease of use of the selected WEAPON.
     

     When the marker reaches the end of the bar a different bar will be shown and you will have missed
     an OPPORTUNITY.

     Press [Space] to stop the marker and attack the opponent.

     Characters the marker can hit:
     ------------------------------

         x   Miss

         -   Regular Hit

         +   WEAK SPOT Hit (+10% Damage which gets less each OPPORTUNITY missed)

        < >  Regular Hit

 After an attack:
 ----------------
     Provided you have hit the opponent, Base Damage will be applied, followed by CRIT Damage (if the hit 
     was critical - 30% chance, see your CRIT Rate), then finally WEAK SPOT Damage if the hit was
     at the weak spot.

     This is all calculated and shown as a final Damage TOTAL. Which is taken off the opponent's health.


 Opponent attack:
 ----------------
     Similar to a player attack with only the Base Damage, CRIT Damage, CRIT Rate
     and a BONUS ACTION (such as steal one turn from the player) that occur at random
     intervals in the combat session.


 > Press [any key] to close.
");

            Console.ReadKey();
            
            MainCombatScreen();
        }
        
        private void ExitCombat()
        {

        }
    }
}
