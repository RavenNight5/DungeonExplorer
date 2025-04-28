using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using DungeonExplorer.Testing;
using Microsoft.SqlServer.Server;

namespace DungeonExplorer
{
    public class Combat : Game
    {
        private Random rand = new Random();

        private string randBarPattern = "";

        private string weaponStatus = "Choose";
        private string bonusItemStatus = "Choose";

        private string player_BonusEffect = "";  // The bonus effect of the equipped bonus item (if any)

        private int player_BaseDamage = 0;
        private int player_CRITDmg = 0;
        private int player_CRITRate = 0;

        private int weakSpotDamage = 5;  // The % damage added to the attack if the weak spot is hit

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

        private static int weaponEaseOfUse = 20;

        private string[] barPatterns = new string[] {  // Using the patterns would require a loop and same user input to stop the marker. (For a later date)
        "x - - - x - - - - x - - - < + > - - x",

        "- - - - - - - - x < + > - - - - - - x",

        "x - - - < + > - - - - - x - - - - - -",

        "- - - - - - - - - x - - < + > - - - x",

        "- - < + > - - x - - - x - x - - - - -",

        "- - - - < + > - - - < + > - x - - - -",

        "x - - - - x < + > - - - x - - - - - -"
        };

        private string currentBarPattern = "";

        private readonly string _markerCharacter = "^";


        // Used for the threads that handle player input and console output simultaneously
        
        static CancellationTokenSource cts = new CancellationTokenSource();
        private static bool attacking = false;

        //

        public Combat(Monster monster)
        {
            MonsterObject = monster;

            randBarPattern = barPatterns[0];
            barLength = barPatterns[0].Length;

            player_BaseDamage = Player.BaseDamage;
            player_CRITDmg = Player.CRITDamage;
            player_CRITRate = Player.CRITRate;

            MainCombatScreen();
        }

        public void MainCombatScreen(bool incorrectInput = false)
        {
            Program.CLEAR_CONSOLE();

            InCombat = true;

            notPassing = true;

            if (Player.Health <= 0)
            {
                Console.WriteLine("You have been defeated...\n\nPress [any key] to continue.\n");

                Console.ReadKey();

                ExitCombat(false);
            }
            else if (MonsterObject.Health <= 0)
            {
                Console.WriteLine($"You have defeated {MonsterObject.Name}!\n\nPress [any key] to continue.\n");

                Console.ReadKey();

                ExitCombat(true);
            }

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

            if (Combat_EquippedWeapon != "" && Combat_EquippedWeapon != null)
            {
                string[] weaponStats = Item.GetItemStats(Combat_EquippedWeapon);

                try
                {
                    player_BaseDamage = Player.BaseDamage + int.Parse(weaponStats[3]);
                    player_CRITDmg = Player.BaseDamage + int.Parse(weaponStats[4]);
                    player_CRITRate = Player.BaseDamage + int.Parse(weaponStats[5]);
                    weaponEaseOfUse = int.Parse(weaponStats[6]);
                }
                catch
                {
                    Debug.WriteLine($"Error parsing Weapon stats.");
                }
            }

            if (Combat_EquippedBonus != "" && Combat_EquippedBonus != null)
            {
                string[] bonusStats = Item.GetItemStats(Combat_EquippedBonus);

                try
                {
                    player_BonusEffect = bonusStats[5];
                }
                catch
                {
                    Debug.WriteLine($"Error parsing Bonus Item stats.");
                }
            }

            string playerStats = $@"
          Vs...

    '{Program.NameTemp}' | Species: Cleaner

      Weapons        Bonus        {Program.NameTemp}{Program.TempPlural} Health:
    --── ! ──--   --── + ──--     ┌───────----- - - -
    | {Combat_EquippedWeaponImage[0]} |   │ {Combat_EquippedBonusImage[0]} │     ║ {HealthVisual} ({Player.Health}/{Game.CurrentPlayer.MaxHealth}
    │ {Combat_EquippedWeaponImage[1]} │   | {Combat_EquippedBonusImage[1]} |     └───────----- - - -
    ! {Combat_EquippedWeaponImage[2]} !   + {Combat_EquippedBonusImage[2]} +
    │ {Combat_EquippedWeaponImage[3]} │   | {Combat_EquippedBonusImage[3]} |     My Combat Stats:
    |  {Combat_EquippedWeaponImage[4]} |   │  {Combat_EquippedBonusImage[4]} │     ╔══─=───────---
    --── ! ──--   --── + ──--     │   {player_BaseDamage}     Base Damage
                                  │ ------
                                  │+[ {player_CRITDmg} ]%  CRIT dmg
                                  │ [ {player_CRITRate} ]%  Chance of CRIT hit
                                  ╚══─=───────---";

            Console.Write(MonsterObject.MonsterInterface);

            System.Threading.Thread.Sleep(200);

            Console.WriteLine(playerStats);

            System.Threading.Thread.Sleep(100);

            Console.WriteLine($@" > Start Attack [Space]
   | Total Attacks Made: {attacksMade}
   | Turns Passed: {turnsPassed}

 > {weaponStatus} Weapon(s) [1]
 > {bonusItemStatus} Bonus Item [2]

 > Pass Your Turn [P]

 > Help [H]
");

            if (incorrectInput)
            {
                Console.WriteLine($"Option entered was invalid. Please choose again.");
            }

            void playerAction()
            {
                string playerInput = InputHandler.CombatMainOptions(new string[] { "Spacebar", "D1", "D2", "P", "H" });

                if (playerInput == "Spacebar")
                {
                    attacksMade += 1;

                    attacking = true;

                    SubCombatScreen();
                }
                else if (playerInput == "D1")
                {
                    CurrentPlayer.DisplayInventory(Item.ItemTypeIndex[0]);  // Weapon
                }
                else if (playerInput == "D2")
                {
                    CurrentPlayer.DisplayInventory(Item.ItemTypeIndex[1]);  // Bonus Item
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
                else
                {
                    MainCombatScreen(true);
                }
            }

            playerAction();
            
        }

        private string GetAttackBar()
        {
            if (markerPos <= 0)  // Get a new random bar pattern every time the marker is at pos 0  
            {
                randBarPattern = barPatterns[rand.Next(barPatterns.Length)];
                currentBarPattern = randBarPattern;
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
 │ {currentBarPattern} │  
     {marker}  
 ╚══──────────-----      -----──────────═╝  

 ({opportunities}/5 opportunities passed | +{weakSpotDamage}% weak spot dmg)  

        ";

            return bar;
        }

        private void SubCombatScreen()
        {
            Program.CLEAR_CONSOLE();

            Console.Write(GetAttackBar());

            // Reset number of opportunities and the marker position
            opportunities = 0;
            markerPos = 0;

            cts = new CancellationTokenSource(); // Reset the cancellation token source  

            Thread attackThread = new Thread(AttackThread);
            attackThread.Start();

            Thread inputThread = new Thread(InputThread);
            inputThread.Start();

            // Wait for the threads to finish before continuing
            attackThread.Join();
            inputThread.Join();

            void AttackThread()
            {
                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        if (attacking)
                        {
                            while (attacking)
                            {
                                Program.CLEAR_CONSOLE();
                                Console.Write(GetAttackBar());

                                markerPos += 2; // Increment the marker's position  

                                Thread.Sleep(weaponEaseOfUse * 10);

                                if (markerPos >= barLength)
                                {
                                    opportunities++;
                                    markerPos = 0;

                                    if (opportunities >= 5)
                                    {
                                        Console.WriteLine("You took too long... The enemy attacks.\n\n");
                                        Thread.Sleep(1500);

                                        attacking = false;

                                        cts.Cancel(); // Stop the thread  

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Debug.WriteLine("Attack thread cancelled.");
                }
            }

            void InputThread()
            {
                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        if (Console.KeyAvailable)
                        {
                            var key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Spacebar)
                            {
                                attacking = false;

                                cts.Cancel();

                                attackedResult();
                            }
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Debug.WriteLine("Input thread cancelled.");
                }
            }

            void attackedResult()
            {
                if (opportunities >= 5)
                {
                    Program.CLEAR_CONSOLE();

                    Console.Write($"You took too long... The enemy attacks.\n\n");

                    Thread.Sleep(750);

                    EnemyAttack();
                }
                else
                {
                    Program.CLEAR_CONSOLE();

                    string hitCharacter = "";

                    if (!(currentBarPattern[markerPos] == ' '))
                    {
                        hitCharacter = randBarPattern[markerPos].ToString();
                    }

                    int attackResult = 0;

                    if (hitCharacter == "x")
                    {
                        Console.Write("You missed.\n\n");
                        Thread.Sleep(400);
                        attackResult = PlayerAttack(true, false);
                    }
                    else if (hitCharacter == "-" || hitCharacter == "<" || hitCharacter == ">")
                    {
                        attackResult = PlayerAttack(false, false);
                    }
                    else
                    {
                        attackResult = PlayerAttack(false, true);
                    }

                    MonsterObject.DamageMonster(MonsterObject, attackResult);
                }

                MainCombatScreen();
            }
        }

        private int EnemyAttack()
        {
            return MonsterObject.Attack();
        }

        private int PlayerAttack(bool miss, bool hitWeakSpot)
        {
            return CurrentPlayer.Attack(miss, hitWeakSpot, new List<int>() { player_BaseDamage, player_CRITDmg, player_CRITRate, weakSpotDamage });
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
        
        private void ExitCombat(bool victorious)
        {
            InCombat = false;

            Program.CLEAR_CONSOLE();

            if (Tests.InTestingMode)
            {
                Console.WriteLine("As testing mode is enabled, the program will need to restart.\n\nPress [any key] to continue.\n");

                Console.ReadKey();

                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);

                Environment.Exit(0);
            }
            
            // Automatically returns the player to the room or testing menu
        }


    }
}
