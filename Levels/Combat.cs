using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer.Levels
{
    public class Combat
    {
        public static string Combat_EquippedWeapon = "";
        public static string[] Combat_EquippedWeaponImage = Inventory.InventoryEmptySlot;

        public static string Combat_EquippedBonus = "";
        public static string[] Combat_EquippedBonusImage = Inventory.InventoryEmptySlot;

        private int playerBaseDamage = 0;
        private int playerCRITDmg = 0;
        private int playerCRITRate = 0;

        private Monster MonsterObject { get; set; }

        private int markerPosition = 0;

        private int opportunities = 0;
        private int weakSpotModifier = 5;

        private readonly string[] _barPatterns = new string[] {  // x = miss, - - = regular hit, <+> = weak spot
        "x - - - - - - - - x - - - < + > - - x",

        "- - < + > - x - x - - - x - - - - - -",

        "x - - - x < + > - - - - x - - - - - x",

        "- - - x - - - - - x - - - x - < + > -",

        "- - - - - - - < + > - x - - - x - - -",

        "x - - < + > - - - - - < + > - - x - -",

        "- - x - - - - - - x < + > - - - - - x"
        };

        private readonly string _marker = "^";


        public Combat(Monster monster)
        {
            MonsterObject = monster;

            MainCombatScreen();
        }


        public void MainCombatScreen()
        {
            string PlayerHealthVisual = "";

            for (int i = 0; i < Player.Health; i += 10)
            {
                PlayerHealthVisual += "+ ";
            }


            string playerStats = $@"
          Vs...

    '{Game.CurrentPlayer.Name}'

    Weapon     Bonus
    --── ──--  --── ──--    {Player.NamePlural} Health:
    │{Combat_EquippedWeaponImage[0]}│  │{Combat_EquippedBonusImage[0]}│    ┌───────----- - - -
    │{Combat_EquippedWeaponImage[1]}│  │{Combat_EquippedBonusImage[1]}│    ║ {PlayerHealthVisual} ({Player.Health}/{Game.CurrentPlayer.MaxHealth}
    ║{Combat_EquippedWeaponImage[2]}║  ║{Combat_EquippedBonusImage[2]}║    └───────----- - - -
    │{Combat_EquippedWeaponImage[3]}│  │{Combat_EquippedBonusImage[3]}│
    │ {Combat_EquippedWeaponImage[4]}│  │ {Combat_EquippedBonusImage[4]}│
    --─ ! ─--  --─ + ─--

    My Combat Stats
    ╔══─=───────---
    │   {playerBaseDamage}     Base Damage
    ║+[ {playerCRITDmg} ]%  CRIT dmg
    │ [ {playerCRITRate} ]%  Chance of CRIT hit
    ╚══─=───────---


 > Start Attack [Space]

 > Switch Weapon [1]
 > Switch Extra Item [2]

 > Help [H]

 > Pass Your Turn [P]

";

            Console.Write(MonsterObject.MonsterInterface + playerStats);

            void GetInput()
            {
                string playerInput = Game.InputHandler.CombatMainOptions(new string[] { "Spacebar", "97", "98", "H", "P" });  //Key enums: 97 = 1, 98 = 2

                if (playerInput != null)
                {
                    GetInput();
                }
                else
                {
                    if (playerInput == "Spacebar")
                    {
                        SubCombatScreen();  // Start the attack by showing the sub combat screen that contains the bar
                    }
                    else if (playerInput == "97" || playerInput == "98")  // 1 or 2
                    {
                        if (playerInput == "97")
                        {
                            Game.CurrentPlayer.DisplayInventory(true, "Weapon");
                        }
                        else  // 98
                        {
                            Game.CurrentPlayer.DisplayInventory(true, "Bonus");
                        }
                    }
                }
            }
            
            GetInput();
        }



        private string GetCombatBar()
        {
            Random rand = new Random();

            string newBar = _barPatterns[rand.Next(_barPatterns.Length)];
            string newMarker = $"{_marker}";

            // Use a random pattern from the bar patterns array
            string bar = $@"
    Your turn to attack [Space]:
    ╔══──────────-----      -----──────────═╗
    │ {newBar} │
      {_marker}
    ╚══──────────-----      -----──────────═╝

    ({opportunities}/5 opportunities passed | +{weakSpotModifier}% weak spot dmg)
";
            return bar;
        }

        public void SubCombatScreen()
        {
            Program.CLEAR_CONSOLE();

            Console.WriteLine(GetCombatBar());
            Console.WriteLine("\n > Attack [Space]\n\n");

            Game.InputHandler.WaitOnKey("Spacebar");
        }

    }
}
