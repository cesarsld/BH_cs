using System;
using System.Linq;

namespace BHtest
{
    class Program
    {

        static void Main(string[] args)
        {
            Character[] array = new Character[4];

            array[0] = new Character { hp = 2, maxHp = 100 };
            array[1] = new Character { hp = 1, maxHp = 10 };
            array[2] = new Character { hp = 50, maxHp = 100 };
            array[3] = new Character { hp = 600, maxHp = 1000 };

            int[] returnarray = getSpreadValues(array, array[0], 0);
            
            SpecialDungeon SD = new SpecialDungeon(750);
            SD.heroes = new Character[3];
            for (int i = 0; i < 100; i++)
            {
                SD.heroes[0] = new Character
                {
                    // Base Stats
                    //power = 3087,
                    //stamina = 900,
                    //agility = 250,
                    power = 2670,
                    stamina = 1345,
                    agility = 232,
                    // Specials
                    critChance = 10,
                    critDamage = 50f,
                    dsChance = 0,
                    empowerChance = 0,
                    blockChance = 0f,
                    evadeChance = 5f,
                    deflectChance = 1.5f,
                    absorbChance = 8.25f,
                    // Runes
                    powerRunes = 0f,
                    staminaRunes = 0f,
                    agilityRunes = 4f,
                    damageReduction = 33.5f,
                    // Pet
                    petName = PetType.Urgoff,
                    petProcType = PetProcType.GetHit,
                    PetLevel = 3,
                    weapon = Character.Weapon.ShieldStaff,
                    metaRune = Character.MetaRune.Redirect,
                    //hoodBonus = true,
                    obliterationBonus = Character.ObliterationBonus.Bonus_4_of_4,
                    _isHero = true
                };
                

                

                
                //wb.difficultyModifier = 120;
                SD.Simulation(0);
            }
            
            //foreach (int ints in returnarray)
            //{
                //Console.WriteLine(ints.ToString() + "\r");
                //}
            TankSim();
            Console.ReadLine();
        }




        private static int[] getSpreadValues(Character[] party, Character author, int value)
        {
            int[] values = new int[party.Length];
            //order by ascending HPPerc of alive characters
            Character[] needHealingArray = party.Where(hero => hero.alive).OrderBy(hero => hero.hpPerc).ToArray();
            int healValue = 1000;

            // If this is a heal
            if (healValue > 0)
            {
                healValue = 1000;

                // Even out all the health pools before adding to each individual
                int currentIndex = 0;
                while (healValue != 0 && currentIndex < needHealingArray.Length - 1)
                {
                    int nextIndex = currentIndex + 1;

                    Character currentchar = needHealingArray[currentIndex];
                    Character nextChar = needHealingArray[nextIndex];

                    float currentPerc = currentchar.hpPerc;
                    float nextPerc = nextChar.hpPerc;

                    // If nobody needs a heal
                    if (currentPerc == 1f)
                    {
                        healValue = 0;
                        break;
                    }

                    float neededPerc = nextPerc - currentPerc;
                    int neededHealth = Convert.ToInt32(((float)currentchar.maxHp * neededPerc) * (float)nextIndex);

                    // If they need more health than we can give
                    if (neededHealth > healValue)
                        neededHealth = healValue;

                    // Spread this out to other players with even health
                    int splitHealth = Convert.ToInt32((float)neededHealth / (float)nextIndex);
                    for (int i = 0; i < nextIndex; i++)
                    {
                        if (healValue == 0) break;

                        if (splitHealth > healValue)
                            splitHealth = healValue;  //not needed? since split will never exceed total pool following previous logic

                        values[i] += splitHealth;
                        healValue -= splitHealth;
                    }
                    currentIndex++;
                }

                // If there is more health left to consume
                while (healValue != 0)
                {
                    int splitHealth = Convert.ToInt32((float)healValue / (float)needHealingArray.Length);

                    // Make sure we can comsume the rest of the pool
                    if (splitHealth == 0)
                        splitHealth = 1;

                    int consumed = 0;
                    for (int i = 0; i < needHealingArray.Length; i++)
                    {
                        if (healValue == 0)
                            continue;

                        Character entity = needHealingArray[i];

                        int gainHealth = values[i];
                        int neededHealth = entity.maxHp - entity.hp + gainHealth;
                        if (neededHealth == 0)
                            continue;

                        int addedHealth = splitHealth;
                        if (addedHealth > healValue)
                            addedHealth = healValue;

                        if (addedHealth == 0)
                            continue;

                        // Dont let them add more health than they have
                        if (addedHealth > neededHealth)
                            addedHealth = neededHealth;

                        values[i] += addedHealth;
                        healValue -= addedHealth;
                        consumed += addedHealth;
                    }

                    if (consumed == 0)
                        break;
                }
            }

            return values;
        }


        //		static void accsim() {
        //			int i;
        //			int sim = 100000;
        //			//int roll1 = 0, roll2 = 0, roll3 = 0;
        //			float deflectcount = 0, evadecount = 0, blockcount = 0, absorbcount = 0;
        //			float innatedmgreduc = 0.8f;
        //			float absorb = 5f, deflect = 0f, evade = 2.5f, block = 0f;
        //			float dmgreduction = 0;
        //			int proccount = 0;
        //			float dproc, eproc, bproc, aproc;
        //			for (i = 0; i<sim; i++) 
        //			{
        //				if (!Logic.RNGroll (absorb)) 
        //				{
        //					if (!Logic.RNGroll (deflect)) 
        //					{
        //						if (!Logic.RNGroll (evade)) 
        //						{
        //							if (Logic.RNGroll (block)) 
        //							{
        //								blockcount++;
        //							}
        //						} else 
        //						{
        //							evadecount++;
        //						}
        //					} else 
        //					{
        //						deflectcount++;
        //					}
        //				} else 
        //				{
        //					absorbcount++;
        //				}

        //			}
        //			aproc = 100 * (absorbcount / sim);
        //			dproc = 100 * (deflectcount / sim);
        //			eproc = 100 * (evadecount / sim);
        //			bproc = 100 * (blockcount / sim);


        //			proccount = Convert.ToInt32(deflectcount + evadecount + blockcount + absorbcount);
        //			Console.WriteLine ("absorb = {0}/100000 \ndeflect = {1}/100000 \nevade = {2}/100000\nblock = {3}/100000\n ", absorbcount, deflectcount, evadecount, blockcount);
        //			dmgreduction = 100f * (1 - ((sim - proccount + blockcount * 0.5f) / sim) * innatedmgreduc);
        //			Console.WriteLine ("Damage mitigtion = {0}%\n", dmgreduction);

        //		}


        static void TankSim()
        {
            int turnsToSurvive = 100;
            int baseDamage = 5;
            int damageMultiplier = 0;
            int levelDamage;
            int i;
            int j;
            bool blockRoll, evadeRoll, deflectRoll, absorbRoll;
            int runs = 100;
            int multiplierSum = 0;

            int countAbsorbs = 0;
            int countDeflects = 0;
            int countEvades = 0;
            int countBlocks = 0;
            int countHits = 0;

            Character hero = new Character();
            hero.power = 3087;
            hero.stamina = 900;
            hero.agility = 250;
            hero.sp = 0;
            hero.shield = 0;
            hero.critChance = 10f;
            hero.critDamage = 50f;
            hero.dsChance = 0f;
            hero.blockChance = 100f;
            hero.evadeChance = 3.5f;
            hero.deflectChance = 6f;
            hero.absorbChance = 12.5f;
            hero.damageReduction = 0f;
            hero.powerRunes = 0f;
            hero.agilityRunes = 0f;
            hero.petName = PetType.Urgoff;
            hero.PetLevel = 4;
            hero.petProcType = PetProcType.GetHit;

            hero.InitialiseHero();

            for (j = 0; j < runs; j++)
            {
                hero.Revive();
                damageMultiplier = 0;
                while (hero.alive)
                {
                    damageMultiplier++;
                    levelDamage = baseDamage * damageMultiplier;

                    hero.hp = hero.maxHp;
                    hero.shield = 0;

                    for (i = 0; i < turnsToSurvive; i++)
                    {
                        int currentDamage = levelDamage;
                        blockRoll = Logic.RNGroll(hero.blockChance);
                        evadeRoll = Logic.RNGroll(hero.evadeChance);
                        deflectRoll = Logic.RNGroll(hero.deflectChance);
                        absorbRoll = Logic.RNGroll(hero.absorbChance);
                        if (!absorbRoll)
                        {
                            if (!deflectRoll)
                            {
                                if (!evadeRoll)
                                {
                                    if (!blockRoll)
                                    {
                                        countHits++;
                                        if (hero.shield > 0)
                                        {
                                            if (currentDamage * hero.damageReduction > hero.shield)
                                            {
                                                currentDamage -= Convert.ToInt32(hero.shield / hero.damageReduction);
                                                hero.shield = 0;
                                            }
                                            else
                                            {
                                                hero.shield -= Convert.ToInt32(currentDamage * hero.damageReduction);
                                                currentDamage = 0;
                                            }
                                        }
                                        hero.hp -= Convert.ToInt32(currentDamage * hero.damageReduction);
                                        if (!hero.alive)
                                        {
                                            multiplierSum += damageMultiplier;
                                            break;
                                        }
                                        else
                                        {
                                            hero.pet.PetSelection(hero, new Character[] { hero }, new Character[] { }, PetProcType.GetHit);
                                        }
                                    }
                                    else
                                    {
                                        countBlocks++;
                                        if (hero.shield > 0)
                                        {
                                            if (currentDamage * hero.damageReduction > hero.shield)
                                            {
                                                currentDamage -= Convert.ToInt32(hero.shield / hero.damageReduction);
                                                hero.shield = 0;
                                            }
                                            else
                                            {
                                                hero.shield -= Convert.ToInt32(currentDamage * hero.damageReduction);
                                                currentDamage = 0;
                                            }
                                        }

                                        currentDamage = Convert.ToInt32(currentDamage * hero.damageReduction / 2);
                                        hero.hp -= currentDamage;
                                        if (hero.hp <= 0)
                                        {
                                            multiplierSum += damageMultiplier;
                                            break;
                                        }
                                        else
                                        {
                                            hero.pet.PetSelection(hero, new Character[] { hero }, new Character[] { }, PetProcType.GetHit);
                                        }
                                    }
                                }
                                else
                                {
                                    countEvades++;
                                    hero.pet.PetSelection(hero, new Character[] { hero }, new Character[] { }, PetProcType.GetHit);
                                }
                            }
                            else
                            {
                                countDeflects++;
                            }
                        }
                        else
                        {
                            countAbsorbs++;
                            hero.shield += currentDamage;
                            if (hero.shield > hero.maxShield) hero.shield = hero.maxShield;
                            hero.pet.PetSelection(hero, new Character[] { hero }, new Character[] { }, PetProcType.GetHit);
                        }
                    }
                }
            }
            int floor;
            floor = Convert.ToInt32(multiplierSum / j);
            float mitigation = (1 - (hero.maxHp / ((floor) * baseDamage * (float)turnsToSurvive))) * 100f;
            Console.WriteLine("On average, hero can survive {1} hits at level {0}.", floor, turnsToSurvive);
            Console.WriteLine("Total mitigation is {0}%.", mitigation);

            int totalIncomingAttacks = countAbsorbs + countDeflects + countEvades + countBlocks + countHits;
            float absorbPercentage = ((float)countAbsorbs / totalIncomingAttacks) * 100f;
            float deflectPercentage = ((float)countDeflects / totalIncomingAttacks) * 100f;
            float evadePercentage = ((float)countEvades / totalIncomingAttacks) * 100f;
            float blockPercentage = ((float)countBlocks / totalIncomingAttacks) * 100f;
            float hitPercentage = ((float)countHits / totalIncomingAttacks) * 100f;

            Console.WriteLine("Total incoming attacks: {0}.", totalIncomingAttacks);
            Console.WriteLine("Total absorbs: {0}. Actual percentage: {1}%", countAbsorbs, absorbPercentage);
            Console.WriteLine("Total deflects: {0}. Actual percentage: {1}%", countDeflects, deflectPercentage);
            Console.WriteLine("Total evades: {0}. Actual percentage: {1}%", countEvades, evadePercentage);
            Console.WriteLine("Total blocks: {0}. Actual percentage: {1}%", countBlocks, blockPercentage);
            Console.WriteLine("Total hits: {0}. Actual percentage: {1}%", countHits, hitPercentage);
        }

        /*
        static void TankSim()
        {
            int baseDamage = 5;
            int damageMultiplier = 0;
            int levelDamage;
            int i;
            int j;
            bool blockRoll, evadeRoll, deflectRoll, absorbRoll;
            int runs = 100;
            int multiplierSum = 0;

            Character hero = new Character();
            hero.power = 1211;
            hero.stamina = 1570;
            hero.agility = 323;
            hero.sp = 0;
            hero.shield = 0;
            hero.critChance = 10f;
            hero.critDamage = 50f;
            hero.dsChance = 0f;
            hero.blockChance = 25f;
            hero.evadeChance = 20f;
            hero.deflectChance = 15f;
            hero.absorbChance = 0f;
            hero.damageReduction = 5f;
            hero.powerRunes = 0f;
            hero.agilityRunes = 0f;
            //hero.petName = PetType.Urgoff;
            hero.PetLevel = 3;
            hero.petProcType = PetProcType.GetHit;

            hero.InitialiseHero();

            for (j = 0; j < runs; j++)
            {
                hero.Revive();
                damageMultiplier = 0;
                while (hero.alive)
                {
                    damageMultiplier++;
                    levelDamage = baseDamage * damageMultiplier;

                    hero.hp = hero.maxHp;
                    hero.shield = 0;

                    for (i = 0; i < 100; i++)
                    {
                        int currentDamage = levelDamage;
                        blockRoll = Logic.RNGroll(hero.blockChance);
                        evadeRoll = Logic.RNGroll(hero.evadeChance);
                        deflectRoll = Logic.RNGroll(hero.deflectChance);
                        absorbRoll = Logic.RNGroll(hero.absorbChance);
                        if (!absorbRoll)
                        {
                            if (!deflectRoll)
                            {
                                if (!evadeRoll)
                                {
                                    if (!blockRoll)
                                    {
                                        if (hero.shield > 0)
                                        {
                                            if (currentDamage * hero.damageReduction > hero.shield)
                                            {
                                                currentDamage -= Convert.ToInt32(hero.shield / hero.damageReduction);
                                                hero.shield = 0;
                                            }
                                            else
                                            {
                                                hero.shield -= Convert.ToInt32(currentDamage * hero.damageReduction);
                                                currentDamage = 0;
                                            }
                                        }
                                        hero.hp -= Convert.ToInt32(currentDamage * hero.damageReduction);
                                        if (!hero.alive)
                                        {
                                            multiplierSum += damageMultiplier;
                                            break;
                                        }
                                        else
                                        {
                                            hero.pet.PetSelection(hero, new Character[] { hero }, new Character[] { }, PetProcType.GetHit);
                                        }
                                    }
                                    else
                                    {

                                        if (hero.shield > 0)
                                        {
                                            if (currentDamage * hero.damageReduction > hero.shield)
                                            {
                                                currentDamage -= Convert.ToInt32(hero.shield / hero.damageReduction);
                                                hero.shield = 0;
                                            }
                                            else
                                            {
                                                hero.shield -= Convert.ToInt32(currentDamage * hero.damageReduction);
                                                currentDamage = 0;
                                            }
                                        }

                                        currentDamage = Convert.ToInt32(currentDamage * hero.damageReduction / 2);
                                        hero.hp -= currentDamage;
                                        if (hero.hp <= 0)
                                        {
                                            multiplierSum += damageMultiplier;
                                            break;
                                        }
                                        else
                                        {
                                            hero.pet.PetSelection(hero, new Character[] { hero }, new Character[] { }, PetProcType.GetHit);
                                        }
                                    }
                                }
                                else
                                {
                                    hero.pet.PetSelection(hero, new Character[] { hero }, new Character[] { }, PetProcType.GetHit);
                                }
                            }
                        }
                        else
                        {
                            hero.shield += currentDamage;
                            if (hero.shield > hero.maxShield) hero.shield = hero.maxShield;
                            hero.pet.PetSelection(hero, new Character[] { hero }, new Character[] { }, PetProcType.GetHit);
                        }
                    }
                }
            }
            int floor;
            floor = Convert.ToInt32(multiplierSum / j);
            float mitigation = (1 - (hero.maxHp / ((floor) * baseDamage * 100f))) * 100f;
            Console.WriteLine("On average, hero died at level {0}.", floor);
            Console.WriteLine("Total mitigation is {0}%.", mitigation);
        }

        */

        static void PetProc(Character hero)
        {
            //switch (hero.currentPet)
            //{
            //    case Character.Pet.Boiguh:
            //        BoiguhProc(hero);
            //        break;
            //    case Character.Pet.Gemmi:
            //        GemmiProc(hero);
            //        break;
            //    case Character.Pet.Boogie:
            //        BoogieProc(hero);
            //        break;
            //    default:
            //        break;
            //}
        }

        //static bool LuvboiProc(Character hero)
        //{
        //    hero.hp = 0;
        //    if (hero.currentPet != Character.Pet.Luvboi)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        bool petRoll = Logic.RNGroll(40f);
        //        if (petRoll)
        //        {
        //            hero.hp = hero.power * 2;
        //        }
        //        return petRoll;
        //    }
        //}

        static void BoiguhProc(Character hero)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            int shieldModifier = Convert.ToInt32(hero.power * 0.06);
            float shieldValue = Convert.ToInt32(rnd.Next(0, shieldModifier) + 0.27 * hero.power);

            bool petRoll = Logic.RNGroll(20f);
            bool critRoll = Logic.RNGroll(10f);
            if (critRoll)
            {
                shieldValue *= 1.5f;
            }
            if (petRoll)
            {
                if (hero.hp > 0)
                {
                    hero.shield += Convert.ToInt32(shieldValue);
                    if (hero.shield >= hero.maxShield)
                    {
                        hero.shield = hero.maxShield;
                    }
                }
            }
        }

        static void GemmiProc(Character hero)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int i;
            int healModifier = Convert.ToInt32(hero.power * 0.072);
            float healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.324 * hero.power);

            bool petRoll = Logic.RNGroll(22f);

            bool critoll = Logic.RNGroll(10f);
            if (critoll)
            {
                healValue *= 1.5f;
            }

            if (petRoll)
            {
                for (i = 0; i < 5; i++)
                {
                    if (hero.hp > 0)
                    {
                        hero.hp += Convert.ToInt32(healValue);
                        if (hero.hp >= hero.maxHp)
                        {
                            hero.hp = hero.maxHp;
                        }
                    }
                }
            }
        }

        static void BoogieProc(Character hero)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            int healModifier = Convert.ToInt32(hero.power * 0.14);
            int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.66 * hero.power);

            bool petRoll = Logic.RNGroll(20f);
            bool critoll = Logic.RNGroll(10f);
            if (critoll)
            {
                healValue = Convert.ToInt32(healValue * 1.5f);
            }

            if (petRoll)
            {
                hero.hp += healValue;
                if (hero.hp > hero.maxHp)
                {
                    hero.hp = hero.maxHp;
                }

            }
        }
    }
}
