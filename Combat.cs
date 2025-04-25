using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Combat
    {
        public static string Combat_EquippedWeapon = "";
        public static string[] Combat_EquippedWeaponImage;

        public static string Combat_EquippedBonus = "";
        public static string[] Combat_EquippedBonusImage;

        private int player_BaseDamage = 0;
        private int player_CRITDmg = 0;
        private int player_CRITRate = 0;

        private Monster MonsterObject { get; set; }


        private readonly string[] _barPatterns = new string[] { 
        "",
        "",
        ""
        }; 

        private readonly string _marker = "^";


        public Combat(Monster monster)
        {
            MonsterObject = monster;

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

    {Game.CurrentPlayer.Name}

    Weapon     Bonus
    --── ──--  --── ──--    {Player.NamePlural} Health:
    │       │  │       │    ┌───────----- - - -
    │       │  │       │    ║ {HealthVisual} ({Player.Health}/{Game.CurrentPlayer.MaxHealth}
    ║       ║  ║       ║    └───────----- - - -
    │       │  │       │
    │       │  │       │
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

 > Help [H]

 > Pass Your Turn [P]
";

            Console.Write(MonsterObject.MonsterInterface + playerStats);

            void playerAction()
            {
                string playerInput = Game.InputHandler.CombatMainOptions(new string[] { "Spacebar", "97", "98", "H", "P" });  // Enum keys: 97 = 1, 98 = 2

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

            string randBarPattern = _barPatterns[rand.Next(_barPatterns.Length)];

            string bar = $@"
    Your turn to attack [Space]:
    ╔══──────────-----      -----──────────═╗
    │ {randBarPattern} │
      {_marker}
    ╚══──────────-----      -----──────────═╝

    (3/5 opportunities passed | +5% weak spot dmg)

";

            return bar;
        }
        private void SubCombatScreen()
        {
            Program.CLEAR_CONSOLE();


            //While wait for x * easness of weapon selected...

            Console.Write(GetAttackBar());
        }
    }
}
