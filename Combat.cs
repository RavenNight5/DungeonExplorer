using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using DungeonExplorer.Item_Types;
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

        private string whosTurn = "Player";  // The current turn holder (Player or Monster)

        private string slotBonusUses = " ";

        private int player_BaseDamage = 0;
        private int player_CRITDmg = 0;
        private int player_CRITRate = 0;
        
        private bool notPassing = true;

        private int bonusItemBaseDamage = 0;
        private int bonusItemCRITDamage = 0;
        private int bonusItemCRITRate = 0;
        private int bonusItemWeakSpotDamage = 0;
        private int bonusItemWeaponEaseOfUse = 0;

        private int currentHealthDefense = 0;
        private bool currentLifeShield = false;

        //private bool currentItemPerishable = false;  // When implemented and the item was removed from the player's inventory, the slots no longer worked. Commented out for a later implementation

        private int weakSpotDamage = 10;  // The % damage added to the attack if the weak spot is hit

        private int attacksMade = 0;
        private int turnsPassed = 0;
        private int opportunities = 0;

        private Monster MonsterObject { get; set; }

        private int currentMonsterSpecialAbilityUses = 0;
        private bool monsterJustUsedSpecialAbility = false;  // Used to reset the monster's stats after using a special ability (if applicable)

        private List<Tuple<string, int>> bonusItemUsesThisSession = new List<Tuple<string, int>>();  // String = item name, int = uses left

        public static bool InCombat = false;

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

        private int barLength = 0;

        private string marker = "^";
        private int markerPos = 0;

        public static string Combat_EquippedWeapon = "";
        public static string[] Combat_EquippedWeaponImage = Inventory.InventoryEmptySlot;

        public static string Combat_EquippedBonus = "";
        public static string[] Combat_EquippedBonusImage = Inventory.InventoryEmptySlot;

        private static int weaponEaseOfUse = 20;

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

            string attackStatus = "Start";

            if (whosTurn == "Monster")
            {
                attackStatus = "Recieve";
            }

            if (Player.Health <= 0)
            {
                Console.WriteLine("You have been defeated...\n\n");

                Thread.Sleep(750);

                ExitCombat(false);
            }
            else if (MonsterObject.Health <= 0)
            {
                Console.WriteLine($"You have defeated {MonsterObject.Name}!\n\n");

                Thread.Sleep(1000);

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

                slotBonusUses = Item.GetItemUses(Combat_EquippedBonus).ToString();  // Defaults to the total uses of the item

                if (slotBonusUses == "0" || slotBonusUses == "")  // Infinite
                {
                    slotBonusUses = " ";
                }
                else  // Check how many uses left
                {
                    foreach (var item in bonusItemUsesThisSession)
                    {
                        if (item.Item1 == Combat_EquippedBonus)  // If the bonus item is in the list of bonusItemUsesThisSession
                        {
                            slotBonusUses = (int.Parse(slotBonusUses) - item.Item2).ToString();  // Calculate the uses left of the item then assign that to the slotBonusUses
                        }
                    }
                }
            }

            // Check the equipped items and adjust the combat stats

            // Bonus Item
            if (Combat_EquippedBonus != "" && Combat_EquippedBonus != null)
            {
                string[] bonusStats = Item.GetItemStats(Combat_EquippedBonus);

                try
                {
                    bonusItemBaseDamage = int.Parse(bonusStats[3]);
                    bonusItemCRITDamage = int.Parse(bonusStats[4]);
                    bonusItemCRITRate = int.Parse(bonusStats[5]);
                    bonusItemWeaponEaseOfUse = int.Parse(bonusStats[6]);

                    //if (bonusStats[6] == "" || bonusStats[6] == "#")  // Item is NOT a defense item
                    //{
                    //    currentLifeShield = false;
                    //    currentHealthDefense = 0;
                    //}
                }
                catch
                {
                    Debug.WriteLine($"Error parsing Bonus Item stats.");

                    //currentLifeShield = false;
                    //currentHealthDefense = 0;
                }
            }
            else
            {

                ResetBonusItemSlot();

                ResetBonusItemStats();

            }

            // Weapon
            if (Combat_EquippedWeapon != "" && Combat_EquippedWeapon != null)
            {
                string[] weaponStats = Item.GetItemStats(Combat_EquippedWeapon);

                try
                {
                    player_BaseDamage = int.Parse(weaponStats[3]) + bonusItemBaseDamage;
                    player_CRITDmg = int.Parse(weaponStats[4]) + bonusItemCRITDamage;
                    player_CRITRate = int.Parse(weaponStats[5]) + bonusItemCRITRate;
                    weaponEaseOfUse = int.Parse(weaponStats[6]) + bonusItemWeaponEaseOfUse;
                }
                catch
                {
                    Debug.WriteLine($"Error parsing Weapon stats.");
                }
            }
            else
            {
                player_BaseDamage = Player.BaseDamage;
                player_CRITDmg = Player.CRITDamage;
                player_CRITRate = Player.CRITRate;
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
    |  {Combat_EquippedWeaponImage[4]} |   │ {slotBonusUses}{Combat_EquippedBonusImage[4]} │     ╔══─=───────---
    --── ! ──--   --── + ──--     │   {player_BaseDamage}     Base Damage
                                  │ ------
                                  │+[ {player_CRITDmg} ]%  CRIT dmg
                                  │ [ {player_CRITRate} ]%  Chance of CRIT hit
                                  ╚══─=───────---";

            Console.Write(MonsterObject.MonsterInterface);

            Thread.Sleep(200);

            Console.WriteLine(playerStats);

            Thread.Sleep(100);

            Console.WriteLine($@" > {attackStatus} Attack [Space]
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
                    if (whosTurn == "Player")
                    {
                        Random rand = new Random();
                        int abilityChance = rand.Next(1, 11);  // 1 in 10 chance for the special ability (10 = ability)
                        
                        // If the monster's special ability is steal a turn, the random chance out of 10 is met and the use is below the max
                        if (MonsterObject.SpecialAbility == "StealTurn" && abilityChance == 10 && currentMonsterSpecialAbilityUses < MonsterObject.SpecialAbilityUses)
                        {
                            Program.CLEAR_CONSOLE();

                            currentMonsterSpecialAbilityUses += 1;

                            MonsterObject.DoSpecialAbility("Stolen your turn");

                            Thread.Sleep(200);

                            Console.WriteLine("\nPress [any key] to continue");
                            Console.ReadKey();

                            whosTurn = "Monster";

                            EnemyAttack();
                        }
                        else
                        {
                            attacksMade += 1;

                            attacking = true;

                            SubCombatScreen();
                        }
                       
                    }
                    else
                    {
                        EnemyAttack();
                    }
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

                    whosTurn = "Monster";

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

                                    if (weakSpotDamage > 0)
                                    {
                                        weakSpotDamage -= 2;
                                    }

                                    if (opportunities >= 5)
                                    {
                                        attacking = false;

                                        cts.Cancel(); // Stop the thread  

                                        attackedResult();

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

        private void EnemyAttack()
        {
            if (monsterJustUsedSpecialAbility)
            {
                MonsterObject.ResetAllDamageStats();
            }

            Random rand = new Random();
            int abilityChance = rand.Next(1, 6);  // 1 in 5 chance for the special ability (5 = ability)

            if (abilityChance == 5 && currentMonsterSpecialAbilityUses < MonsterObject.SpecialAbilityUses)
            {
                if (MonsterObject.SpecialAbility.Contains("Damage"))
                {
                    monsterJustUsedSpecialAbility = true;

                    Program.CLEAR_CONSOLE();

                    currentMonsterSpecialAbilityUses += 1;

                    // Will double the monster's stats based on the special ability
                    MonsterObject.DoSpecialAbility(new List<int> { MonsterObject.BaseDamage, MonsterObject.CRITDamage, MonsterObject.CRITRate }, $"{MonsterObject.SpecialAbility}");

                    Thread.Sleep(200);

                    Console.WriteLine("\nPress [any key] to continue");
                    Console.ReadKey();
                }
                else
                {
                    monsterJustUsedSpecialAbility = false;
                }
            }
            else
            {
                monsterJustUsedSpecialAbility = false;
            }

            UpdateStatsWithBonusItem();
            
            whosTurn = "Player";

            int mosterDamageDealt = MonsterObject.Attack(false, false, new List<int>() { MonsterObject.BaseDamage, MonsterObject.CRITDamage, MonsterObject.CRITRate });

            CurrentPlayer.DamagePlayer(mosterDamageDealt, currentHealthDefense, currentLifeShield);

            MainCombatScreen();

        }

        private int PlayerAttack(bool miss, bool hitWeakSpot)
        {

            UpdateStatsWithBonusItem();

            whosTurn = "Monster";

            return CurrentPlayer.Attack(miss, hitWeakSpot, new List<int>() { 
                player_BaseDamage + bonusItemBaseDamage,
                player_CRITDmg + bonusItemCRITDamage,
                player_CRITRate + bonusItemCRITRate,
                weakSpotDamage  + bonusItemWeakSpotDamage,
            });
        }

        private void UpdateStatsWithBonusItem()
        {
            List<string> bonusItemEffect = Item.GetItemSpecialEffects(Combat_EquippedBonus);
            int bonusItemEffectUses = Item.GetItemUses(Combat_EquippedBonus);

            if (bonusItemEffect == null || bonusItemEffect.Count == 0)
            {
                return;
            }
            else
            {
                if (bonusItemEffect[0] == "WeakSpotDmg")
                {
                    bonusItemWeakSpotDamage = (weakSpotDamage * int.Parse(bonusItemEffect[1]) / 100);  // Make the modification number a percentage of the existing weakspot damage
                }

                if (bonusItemEffect[0] == "Shield")
                {
                    currentHealthDefense = int.Parse(bonusItemEffect[1]);
                }
                else if (bonusItemEffect[0] == "LifeShield")
                {
                    currentLifeShield = true;
                    currentHealthDefense = int.Parse(bonusItemEffect[1]);
                }
                else
                {
                    currentHealthDefense = 0;
                    currentLifeShield = false;
                }

                //if (bonusItemEffect.Contains("#"))  // Means it is a perishable item and will be removed from inventory after uses = 0
                //{
                //    currentItemPerishable = true;
                //}
                //else
                //{
                //    currentItemPerishable = false;
                //}

                if (bonusItemEffectUses > 0)
                {
                    bool found = false;  // Keeping bool, could be useful at later date

                    List<Tuple<string, int>> updates = new List<Tuple<string, int>>();

                    if (bonusItemUsesThisSession.Count <= 0)  // If the list is initially empty add a new tuple
                    {
                        bonusItemUsesThisSession.Add(new Tuple<string, int>(Combat_EquippedBonus, 1));
                    }

                    foreach (var item in bonusItemUsesThisSession)
                    {
                        if (item.Item1 == Combat_EquippedBonus)  // If the bonus item is in the list of bonusItemUsesThisSession
                        {
                            found = true;

                            if (item.Item2 < bonusItemEffectUses)  // If the uses are less than the max
                            {
                                //if (item.Item2 == bonusItemEffectUses - 1)  // One more use and it's 0
                                //{
                                //    if (currentItemPerishable)
                                //    {
                                //        currentItemPerishable = false;

                                //        CurrentPlayer.RemoveItemFromInventory(Combat_EquippedBonus);

                                //        ResetBonusItemSlot();

                                //    }
                                //}

                                if ((currentHealthDefense > 0 || currentLifeShield == true) && whosTurn == "Monster")  // Bonus item is a health defense item and it's also the monster's turn
                                {
                                    updates.Add(new Tuple<string, int>(item.Item1, item.Item2 + 1));  // Create a new tuple with updated value (since they are readonly)
                                }
                                else if ((currentHealthDefense <= 0 || currentLifeShield == false) && whosTurn == "Player")  // Any other item (currentHealthDefense <= 0) requires it to be the player's turn for the item to be used
                                {
                                    updates.Add(new Tuple<string, int>(item.Item1, item.Item2 + 1));  // Create a new tuple with updated value (since they are readonly)
                                }

                                // No item uses are updated otherwise

                            }
                            else  // Item already used up so reset all the values associated with it
                            {
                                ResetBonusItemStats();
                            }
                        }
                        else
                        {
                            found = false;

                            bonusItemUsesThisSession.Add(new Tuple<string, int>(Combat_EquippedBonus, 1));
                           
                        }
                    }

                    bonusItemUsesThisSession.RemoveAll(item => updates.Any(update => update.Item1 == item.Item1)); // Removes all items from bonusItemUsesThisSession that == the current item (calling it the 'update')
                    bonusItemUsesThisSession.AddRange(updates);  // Re-add the range of updated items to the list

                }
            }

        }

        private void ResetBonusItemStats()
        {
            currentHealthDefense = 0;
            currentLifeShield = false;

            bonusItemBaseDamage = 0;
            bonusItemCRITDamage = 0;
            bonusItemCRITRate = 0;
            bonusItemWeakSpotDamage = 0;
            bonusItemWeaponEaseOfUse = 0;
        }

        private void ResetBonusItemSlot()
        {
            slotBonusUses = " ";
            Combat_EquippedBonus = "";
            Combat_EquippedBonusImage = Inventory.InventoryEmptySlot;
        }

        private void HelpScreen()
        {
            Program.CLEAR_CONSOLE();

            Console.Write(@"                               Combat Help
                               -──~ + ~──-

 Before starting an attack:
 --------------------------
     > Make sure you have an equipped WEAPON and BONUS ITEM if available (most bonus items have limited uses 
       per combat session, shown by the bottom left number in the bonus item slot)
     > Check your stats (most items will affect your Base Damage, CRIT Damage or CRIT Rate)

 During an attack:
 -----------------

     x - - - x - - - - x - - - < + > - - x
                 ^
     A bar (as above) will be shown with a marker  ^  displayed below.

     This marker will move across the bar at a speed relevant to the ease of use of the selected WEAPON.
     
     When the marker reaches the end of the bar a different bar will be shown and you will have missed
     an OPPORTUNITY.

     Press [Space] to stop the marker and attack the opponent.

     Characters the marker can hit:
     ------------------------------
         x   Miss

      -  or  < >   Regular Hit

         +   WEAK SPOT Hit (+10% Damage which gets less each OPPORTUNITY missed)

 After an attack:
 ----------------
     Provided you have hit the opponent, Base Damage will be applied, followed by CRIT Damage (if the hit 
     was critical - a changeable % chance: see your CRIT Rate), WEAK SPOT Damage if the hit was at the weak 
     spot, then finally the values of two 6-sided die.

     The TOTAL is then taken off the opponent's health.


 Opponent attack:
 ----------------
     Similar to a player attack with: Base Damage, CRIT Damage, CRIT Rate, 1d10 Damage Roll,
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

            if (Tests.InTestingMode && victorious)
            {
                Console.WriteLine("As testing mode is enabled, the program will need to restart.\n\nPress [any key] to continue.\n");

                Console.ReadKey();

                Process.Start(AppDomain.CurrentDomain.FriendlyName);  // Restarts console app

                Environment.Exit(0);
            }
            else
            {
                if (victorious)
                {
                    Console.WriteLine($"You have defeated {MonsterObject.Name}!\n\nPress [any key] to continue.\n");

                    Console.ReadKey();

                    Game.RoomHandler.ReturnToLevel();
                }
                else
                {
                    Program.GameOver();
                }
            }
            
            // Automatically returns the player to the room or testing menu if not defeated
        }

    }
}
