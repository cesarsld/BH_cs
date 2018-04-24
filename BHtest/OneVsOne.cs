using System;
using System.Collections.Generic;
using System.Text;

namespace BHtest
{
    public class OneVsOne
    {
        public static Character[] hero = new Character[2];
        public static Character[] enemy = new Character[1];
        public const int heroCount = 2;
        public const int enemyCount = 1;


        public static void duel()
        {
            int counterMax = 100;
            int cycle;
            int i;
            bool DS;
            bool firstTurn = true;
            int p1 = 0;
            int p2 = 0;
        }
    }
}
			//for (int games = 0; games < 5; games++) {
				/*hero [0].power = 442;
				hero [0].stamina = 704;
				hero [0].agility = 111;
				hero [0].sp = 0;
				hero [0].critChance = 10f;
				hero [0].critDamage = 50f;
				hero [0].dsChance = 0f;
				hero [0].blockChance = 31f;
				hero [0].evadeChance = 14f;
				hero [0].deflectChance = 5f;
				hero [0].powerRunes = 0f;
				hero [0].agilityRunes = 0f;
				hero [0].pet = "gemmi";
				hero [0].alive = true;
				hero [0].unity = true;
				hero [0].weapon = Hero.Weapon.bow;
				hero [0].heroName = "Byleth";*/
				//Console.WriteLine ("Round {0}\n", games+1);
				// all numbers that have an "f" at the end must keep the "f"
				/*hero [0].power = 0; // power + stamina + agility must equal 1000 ts
				hero [0].stamina = 0;
				hero [0].agility = 0;
				hero [0].critChance = 10f; // if you have a total of 25% critchance, input 25f
				hero [0].critDamage = 50f; 
				hero [0].dsChance = 5f;
				hero [0].blockChance =5f; 
				hero [0].evadeChance = 5f; 
				hero [0].deflectChance = 5f;
				hero [0].powerRunes = 5f;
				hero [0].agilityRunes = 5f;
				hero [0].healthRunes = 5f;
				hero [0].lifeSteal = 5f;
				hero [0].damageReturn = 5f;
				hero [0].pet = "gemmi"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
				hero [0].weapon = Hero.Weapon.sword; // weapons available: axe, staff, sword, bow, spear
				hero [0].heroName = "Shadown";*/
                /*
				hero [0].power = 475;
				hero [0].stamina = 524;
				hero [0].agility = 1;
				hero [0].critChance = 10f;
				hero [0].critDamage = 50f; 
				hero [0].dsChance = 0f;
				hero [0].blockChance =30f; 
				hero [0].evadeChance = 2.5f; 
				hero [0].deflectChance = 15f;
				hero [0].powerRunes = 0f;
				hero [0].agilityRunes = 0f;
				hero [0].healthRunes = 0f;
				hero [0].lifeSteal = 0f;
				hero [0].damageReturn = 0f;
				hero [0].pet = "nelson"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
				hero [0].weapon = Character.Weapon.staff;
				hero [0].heroName = "etmott";

				hero [1].power = 450; 
				hero [1].stamina = 450;
				hero [1].agility = 100;
				hero [1].critChance = 10f; // 10% base
				hero [1].critDamage = 50f; // 50% base
				hero [1].dsChance = 0f;
				hero [1].blockChance = 50f; // +22% base from Mirror Wings, +8% from upgrades on Mirror Wings (4/4), +20% from 4x Block Runes
				hero [1].evadeChance = 2.5f; // 2.5% base
				hero [1].deflectChance = 10f; // +10% from Mirror Wings
				hero [1].powerRunes = 0f;
				hero [1].agilityRunes = 0f;
				hero [1].healthRunes = 0f;
				hero [1].lifeSteal = 0f;
				hero [1].damageReturn = 0f;
				hero [1].pet = "nemo"; // fully upgraded (4/4)
				hero [1].weapon = Character.Weapon.spear; 
				hero [1].heroName = "TheJoseph98"; 
			
				/*=================================================================================*/
				// Participants list
				/*
				hero [0].power = 400; // power + stamina + agility must equal 1000 ts
                hero [0].stamina = 300;
                hero [0].agility = 300;
                hero [0].critChance = 10f; // if you have a total of 25% critchance, input 25f
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 5f;
                hero [0].blockChance = 30f; 
                hero [0].evadeChance = 5f; 
                hero [0].deflectChance = 10f;
                hero [0].powerRunes = 0f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "nelson"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
                hero [0].weapon = Hero.Weapon.sword; // weapons available: axe, staff, sword, bow, spear
                hero [0].heroName = "Aaron"; // Enter your name here

				hero [0].power = 475;
				hero [0].stamina = 524;
				hero [0].agility = 1;
				hero [0].critChance = 10f;
				hero [0].critDamage = 50f; 
				hero [0].dsChance = 0f;
				hero [0].blockChance =30f; 
				hero [0].evadeChance = 2.5f; 
				hero [0].deflectChance = 15f;
				hero [0].powerRunes = 0f;
				hero [0].agilityRunes = 0f;
				hero [0].healthRunes = 0f;
				hero [0].lifeSteal = 0f;
				hero [0].damageReturn = 0f;
				hero [0].pet = "nelson"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
				hero [0].weapon = Hero.Weapon.staff;
				hero [0].heroName = "etmott";

				hero [0].power = 400; 
                hero [0].stamina = 270;
                hero [0].agility = 330;
                hero [0].critChance = 30f; 
                hero [0].critDamage = 100f; 
                hero [0].dsChance = 10f;
                hero [0].blockChance =0f; 
                hero [0].evadeChance = 2.5f; 
                hero [0].deflectChance = 0f;
                hero [0].powerRunes = 0f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 5f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "nelson"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
                hero [0].weapon = Hero.Weapon.staff; 
                hero [0].heroName = "Rakistal";

				hero [0].power = 500; 
                hero [0].stamina = 200;
                hero [0].agility = 300;
                hero [0].critChance = 10f;
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 3f;
                hero [0].blockChance =0f; 
                hero [0].evadeChance = 0f; 
                hero [0].deflectChance = 0f;
                hero [0].powerRunes = 17f;
                hero [0].agilityRunes = 17f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "boogie";
                hero [0].weapon = Hero.Weapon.spear; 
                hero [0].heroName = “Maykah";

				hero [0].power = 300; // power + stamina + agility must equal 1000 ts
                hero [0].stamina = 200;
                hero [0].agility = 500;
                hero [0].critChance = 10f; // if you have a total of 25% critchance, input 25f
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 32.5f;
                hero [0].blockChance =0f; 
                hero [0].evadeChance = 0f; 
                hero [0].deflectChance = 0f;
                hero [0].powerRunes = 0f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "gemmi"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
                hero [0].weapon = Hero.Weapon.spear; // weapons available: axe, staff, sword, bow, spear
                hero [0].heroName = "zrot"; // Enter your name here

				hero [0].power = 500; 
                hero [0].stamina = 400;
                hero [0].agility = 100;
                hero [0].critChance = 10f;
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 0f;
                hero [0].blockChance =40f; 
                hero [0].evadeChance = 10f; 
                hero [0].deflectChance = 5f;
                hero [0].powerRunes = 0f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "gemmi"; 
                hero [0].weapon = Hero.Weapon.spear; 
                hero [0].heroName = "KVNCRLS";

				hero [0].power = 351;
				hero [0].stamina = 198;
				hero [0].agility = 451;
				hero [0].critChance = 19f;             // base 10% + Max upgraded Melvin Champ 9%
				hero [0].critDamage = 50f;             // base 50%
				hero [0].dsChance = 32f;            // 4 Legendary Vex runes + Max upgraded Melvin Champ (10% + 22%)
				hero [0].blockChance = 0f; 
				hero [0].evadeChance = 2.5f;             // base 2.5% 
				hero [0].deflectChance = 0f;
				hero [0].powerRunes = 0f;
				hero [0].agilityRunes = 0f;
				hero [0].healthRunes = 0f;
				hero [0].lifeSteal = 0f;
				hero [0].damageReturn = 0f;
				hero [0].pet = "Crem";
				hero [0].weapon = Hero.Weapon.spear;
				hero [0].heroName = "Veallyx";

				hero [0].power = 570; // power + stamina + agility must equal 1000 ts
                hero [0].stamina = 160;
                hero [0].agility = 270;
                hero [0].critChance = 5f; // if you have a total of 25% critchance, input 25f
                hero [0].critDamage = 0f; 
                hero [0].dsChance = 14.5f;
                hero [0].blockChance = 0f; 
                hero [0].evadeChance = 0f; 
                hero [0].deflectChance = 0f;
                hero [0].powerRunes = 19.5f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "nelson"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
                hero [0].weapon = Hero.Weapon.spear; // weapons available: axe, staff, sword, bow, spear
                hero [0].heroName = "Mikalangelo"; // Enter your name here

				hero [0].power = 380;
                hero [0].stamina = 120;
                hero [0].agility = 500;
                hero [0].critChance = 46f;
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 2.5f;
                hero [0].blockChance =0f; 
                hero [0].evadeChance = 10f; 
                hero [0].deflectChance = 0f;
                hero [0].powerRunes = 0f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "nelson"; 
                hero [0].weapon = Hero.Weapon.spear;
                hero [0].heroName = "poct13";

				// all numbers that have an "f" at the end must keep the "f"
                hero [0].power = 350; // power + stamina + agility must equal 1000 ts
                hero [0].stamina = 500;
                hero [0].agility = 150;
                hero [0].critChance = 10f; // if you have a total of 25% critchance, input 25f
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 2.5f;
                hero [0].blockChance =20f; 
                hero [0].evadeChance = 2.5f; 
                hero [0].deflectChance = 15f;
                hero [0].powerRunes = 0f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "nelson"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
                hero [0].weapon = Hero.weapon.spear; // weapons available: axe, staff, sword, bow, spear
                hero [0].heroName = "Eritrean"; // Enter your name here

				// all numbers that have an "f" at the end must keep the "f"
                hero [0].power = 500; // power + stamina + agility must equal 1000 ts
                hero [0].stamina = 200;
                hero [0].agility = 300;
                hero [0].critChance = 19f; // if you have a total of 25% critchance, input 25f
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 32f;
                hero [0].blockChance =0f; 
                hero [0].evadeChance = 0f; 
                hero [0].deflectChance = 0f;
                hero [0].powerRunes = 10f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "nelson"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
                hero [0].weapon = Hero.Weapon.sword; // weapons available: axe, staff, sword, bow, spear
                hero [0].heroName = "alchemie"; // Enter your name here


				// all numbers that have an "f" at the end must keep the "f"
                hero [0].power = 380 ; // power + stamina + agility must equal 1000 ts ---> numbers here are shown without bonuses from runes
                hero [0].stamina = 180;
                hero [0].agility = 440; // ---> numbers here are shown without bonuses from runes
                hero [0].critChance = 19f; // if you have a total of 25% critchance, input 25f
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 24.5f;
                hero [0].blockChance =0f; 
                hero [0].evadeChance = 2.5f; 
                hero [0].deflectChance = 0f;
                hero [0].powerRunes = 2.5f;
                hero [0].agilityRunes = 2.5f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "gemmi"; // pet available : gemmi, nelson, nemo, boogie, crem, nerder
                hero [0].weapon = Hero.Weapon.spear; // weapons available: axe, staff, sword, bow, spear
                hero [0].heroName = "Abuelomoderno"; // Enter your name here

				hero [0].power = 425;
                hero [0].stamina = 125;
                hero [0].agility = 450;
                hero [0].critChance = 5f; // if you have a total of 25% critchance, input 25f
                hero [0].critDamage = 50f;
                hero [0].dsChance = 20.5f;
                hero [0].blockChance =0f;
                hero [0].evadeChance = 2.5f;
                hero [0].deflectChance = 1.25f;
                hero [0].powerRunes = 5f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 2.5f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "crem"; 
                hero [0].weapon = Hero.Weapon.spear;
                hero [0].heroName = "Jayee";


				hero [0].power = 450; 
                hero [0].stamina = 450;
                hero [0].agility = 100;
                hero [0].critChance = 10f; // 10% base
                hero [0].critDamage = 50f; // 50% base
                hero [0].dsChance = 0f;
                hero [0].blockChance = 50f; // +22% base from Mirror Wings, +8% from upgrades on Mirror Wings (4/4), +20% from 4x Block Runes
                hero [0].evadeChance = 2.5f; // 2.5% base
                hero [0].deflectChance = 10f; // +10% from Mirror Wings
                hero [0].powerRunes = 0f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "nemo"; // fully upgraded (4/4)
                hero [0].weapon = Hero.Weapon.spear; 
                hero [0].heroName = "TheJoseph98"; 

                hero [0].power = 499; // power + stamina + agility must equal 1000 ts
                hero [0].stamina = 500;
                hero [0].agility = 1;
                hero [0].critChance = 10f; // if you have a total of 25% critchance, input 25f
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 0f;
                hero [0].blockChance =50f; 
                hero [0].evadeChance = 2.5f; 
                hero [0].deflectChance = 10f;
                hero [0].powerRunes = 0f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "dug"; // if Dug it's not possible then Nelson (though likely make the build even less likely to work) 
                hero [0].weapon = Hero.Weapon.spear; // weapons available: axe, staff, sword, bow, spear
                hero [0].heroName = "rmagere"; // Enter your name here

				hero [0].power = 450;
                hero [0].stamina = 200;
                hero [0].agility = 350;
                hero [0].critChance = 19f; 
                hero [0].critDamage = 50f; 
                hero [0].dsChance = 27f;
                hero [0].blockChance =0f; 
                hero [0].evadeChance = 0f; 
                hero [0].deflectChance = 2.5f;
                hero [0].powerRunes = 0f;
                hero [0].agilityRunes = 0f;
                hero [0].healthRunes = 0f;
                hero [0].lifeSteal = 0f;
                hero [0].damageReturn = 0f;
                hero [0].pet = "crem";
                hero [0].weapon = Hero.Weapon.axe; 
                hero [0].heroName = "Steini1991";

@[0071] KVNCRLS        		   check
@Jayee 		                   check
@[CLD9] Veallyx       		   check
@thejoseph                     check
poct                           check
@[K] alchemie                  check
@Aaron (aar118)        		   check
@[Nu11] zrot				   check
 rakistal                      check
@[1up] etmott                  check
@[NB] Mikalangelo (Lvl: 116)   check
@steini                        check
@[GoH] rmagere      delayed    check
@Abuelomoderno                 check
@josiah.tang#6828              check
maykah				    	   check
				*/
				/*=================================================================================*/



				/*hero [1].power = 333;
				hero [1].stamina = 333;
				hero [1].agility = 334;
				hero [1].critChance = 10f;
				hero [1].critDamage = 50f;
				hero [1].dsChance = 2.5f;
				hero [1].blockChance = 40f;
				hero [1].evadeChance = 10f;
				hero [1].deflectChance = 5f;
				hero [1].powerRunes = 0f;
				hero [1].agilityRunes = 0f;
				hero [1].pet = "nelson";
				hero [1].unity = true;
				hero [1].weapon = Hero.Weapon.sword;
				hero [1].heroName = "Shadown";

				for (i = 0; i < 2; i++) {  //initialisation
					hero [i].drain = false;
					hero [i].unity = true;
					hero [i].alive = true;
					hero [i].sp = 0;
					hero [i].critDamage = (100 + hero[i].critDamage) / 100;
					hero [i].powerRunes = (100 + hero[i].powerRunes) / 100;
					hero [i].agilityRunes = (100 + hero[i].agilityRunes) / 100;
					hero [i].healthRunes = (100 + hero[i].healthRunes) / 100;
					hero [i].lifeSteal =  hero[i].lifeSteal / 100;
					hero [i].damageReturn =  hero[i].damageReturn / 100;
					hero [i].turnRate = Logic.turnRate (hero [i].power, hero [i].agility);
					hero [i].power = Convert.ToInt32 (hero [i].power * hero [i].powerRunes);
					hero [i].turnRate *= hero [i].agilityRunes;
					hero [i].hp = Convert.ToInt32(hero [i].stamina * 10 * hero[i].healthRunes);
					hero [i].maxHp = Convert.ToInt32(hero [i].stamina * 10 * hero[i].healthRunes);
					hero [i].interval = counterMax / hero [i].turnRate;
                
				}
				for (i = 0; i < 1; i++) {  //initialisation
					enemy [i].turnRate = Logic.turnRate (enemy [i].power, enemy [i].agility);
					enemy [i].power = Convert.ToInt32 (enemy [i].power * enemy [i].powerRunes);
					enemy [i].turnRate *= enemy [i].agilityRunes;
					enemy [i].hp = enemy [i].stamina * 10;
					enemy [i].maxHp = enemy [i].stamina * 10;
					enemy [i].interval = counterMax / enemy [i].turnRate;

				}

				while (hero [0].alive && hero [1].alive) {
					for (cycle = 1; cycle <= counterMax; cycle++) {

						for (i = 0; i < 2; i++) {
							if (hero [1].turnRate > hero [0].turnRate && firstTurn) {
								i = 1;
								firstTurn = false;
							} else {
								firstTurn = false;
							}
							hero [i].counter++;
							if (hero [i].counter >= hero [i].interval && hero [i].alive) {      //checks if it's player's turn to attack
								Console.WriteLine("{0}'s turn\n", hero[i].heroName);
								Logic.hpPerc1v1 ();
								hero [i].sp++;
								PetLogic.petSelection1v1 (i);
								DS = Logic.RNGroll (hero [i].dsChance);
								HeroLogic.weaponSelection (i, DS);
								hero [i].counter -= hero [i].interval;
								if (!hero [0].alive || !hero [1].alive) {
									i = 2;
									cycle = counterMax;
									if (!hero [0].alive) {
										p2++;
									} else if (!hero [1].alive) {
										p1++;
									}
								}
							}
						}
					}
				}
			}
			Console.WriteLine("{0} {1} : {2} {3}",hero[0].heroName, p1, hero[1].heroName, p2);
        }
    }
}*/