using System;
using System.Collections.Generic;
using System.Text;

namespace BHtest
{
    class OneVsOne
    {
        public static Hero[] hero = new Hero[2];

        void duel()
        {
            int counterMax = 100;
            int cycle;
            int i;
            bool DS;

            hero[0].power = 452;
            hero[0].stamina = 704;
            hero[0].agility = 101;
            hero[0].sp = 0;
            hero[0].critChance = 10f;
            hero[0].critDamage = 1.5f;
            hero[0].dsChance = 0f;
            hero[0].blockChance = 31f;
            hero[0].evadeChance = 14f;
            hero[0].deflectChance = 5f;
            hero[0].powerRunes = 1f;
            hero[0].agilityRunes = 1f;
            hero[0].pet = "gemmi";
            hero[0].alive = true;

            hero[1].power = 452;
            hero[1].stamina = 704;
            hero[1].agility = 101;
            hero[1].sp = 0;
            hero[1].critChance = 10f;
            hero[1].critDamage = 1.5f;
            hero[1].dsChance = 0f;
            hero[1].blockChance = 31f;
            hero[1].evadeChance = 14f;
            hero[1].deflectChance = 5f;
            hero[1].powerRunes = 1f;
            hero[1].agilityRunes = 1f;
            hero[1].pet = "gemmi";
            hero[1].alive = true;

            for (i = 0; i < 2; i++)
            {  //initialisation
                hero[i].turnRate = Logic.turnRate(hero[i].power, hero[i].agility);
                hero[i].power = Convert.ToInt32(hero[i].power * hero[i].powerRunes);
                hero[i].turnRate *= hero[i].agilityRunes;
                hero[i].hp = hero[i].stamina * 10;
                hero[i].maxHp = hero[i].stamina * 10;
                hero[i].interval = counterMax / hero[i].turnRate;
                
            }

            while (hero[0].alive && hero[1].alive)
            {
                for (cycle = 1; cycle <= counterMax; cycle++)
                {

                    for (i = 0; i < 2; i++)
                    {
                        hero[i].counter++;
                        if (hero[i].counter >= hero[i].interval && hero[i].alive)
                        {      //checks if it's player's turn to attack
                            Logic.hpPerc();
                            hero[i].sp++;
                            PetLogic.petSelection(i);
                            DS = Logic.RNGroll(hero[i].dsChance);
                            if (DS)
                            {
                                HeroLogic.heroAttack(i, DS);
                                DS = false;
                                HeroLogic.heroAttack(i, DS);
                            }
                            else { HeroLogic.heroAttack(i, DS); }
                            hero[i].counter -= hero[i].interval;
                            
                        }
                    }

                }
            }

        }
    }
}
