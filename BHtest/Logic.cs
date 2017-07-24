using System;
using System.Collections.Generic;
using System.Text;

namespace BHtest
{
    class Logic
    {

        public static bool RNGroll(float a)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            bool outcome;
            float chance = a * 10f;
            float roll = rnd.Next(0, 1000);
            if (roll <= chance)
            {
                outcome = true;
            }
            else
            {
                outcome = false;
            }
            return outcome;
        }

        public static float turnRate(int b, int a)
        {
            float tr = 0f;
            tr = ((a + b) / 2f);
            tr = (float)Math.Pow(tr, 2);
            tr = tr / (100f * b);
            return tr;
        }

        public static void teamHeal(int l)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int i;
            int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.072);
            float healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.324 * Simulation.hero[l].power);

            bool critroll = RNGroll(Simulation.hero[l].critChance);
            bool petRoll = RNGroll(20f);

            if (critroll)
            {
                healValue *= Simulation.hero[l].critDamage;
            }
            if (petRoll)
            {
                for (i = 0; i < 5; i++)
                {
                    if (Simulation.hero[i].hp > 0)
                    {
                        Simulation.hero[i].hp += Convert.ToInt32(healValue);
                        if (Simulation.hero[i].hp >= Simulation.hero[i].maxHp)
                        {
                            Simulation.hero[i].hp = Simulation.hero[i].maxHp;
                        }
                    }
                }
            }
        }

		public static void teamHeal1v1(int l)
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			int healModifier = Convert.ToInt32(OneVsOne.hero[l].power * 0.072);
			float healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.324 * OneVsOne.hero[l].power);

			bool critroll = RNGroll(OneVsOne.hero[l].critChance);
			bool petRoll = RNGroll(20f);

			if (critroll)
			{
				healValue *= OneVsOne.hero[l].critDamage;
			}
			if (petRoll)
			{
				Console.WriteLine ("{0}'s pet proced healing for {1} hp!\n", OneVsOne.hero[l].heroName, healValue);
				if (OneVsOne.hero[l].hp > 0)
					{
					OneVsOne.hero[l].hp += Convert.ToInt32(healValue);
					if (OneVsOne.hero[l].hp >= OneVsOne.hero[l].maxHp)
					{
						Simulation.hero[l].hp = Simulation.hero[l].maxHp;
					}
				}
			}
		}

        public static void hpPerc()
        {
            int i;
            for (i = 0; i < 5; i++)
            {

                Simulation.hero[i].hpPerc = (float)(Simulation.hero[i].hp) / (float)(Simulation.hero[i].maxHp);

            }
        }

		public static void hpPerc1v1() {
			int i;
			for (i = 0; i < 2; i++)
			{

				OneVsOne.hero[i].hpPerc = (float)(OneVsOne.hero[i].hp) / (float)(OneVsOne.hero[i].maxHp);

			}
		}


        public static int healLogic()
        {
            int i;
            int lowest = 0;
            hpPerc();
            for (i = 0; i < 4; i++)
            {
                if (Simulation.hero[lowest].hpPerc >= Simulation.hero[i + 1].hpPerc)
                {
                    if (Simulation.hero[i + 1].alive)
                    {
                        lowest = i + 1;
                    }
                    else {
                        if (!Simulation.hero[lowest].alive) {
                            lowest = i + 1;
                        }
                    }
                }
            }
            return lowest;
        }
			

        public static int targetSelection(int method)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int target = 0;
            int i = 0;
            bool targetLocked = false;
            if (method == 1)
            {
                while (!targetLocked)
                {
                    if (Simulation.hero[i].alive)
                    {
                        target = i;
                        targetLocked = true;
                    }
                    i++;
                }
            }
            if (method == 2)
            {
                i = 4;
                while (!targetLocked)
                {
                    if (Simulation.hero[i].alive)
                    {
                        target = i;
                        targetLocked = true;
                    }
                    i--;
                }
            }
            if (method == 3)
            {
                while (!targetLocked)
                {
                    i = rnd.Next(0, 5);
                    if (Simulation.hero[i].alive)
                    {
                        target = i;
                        targetLocked = true;
                    }
                }
            }
            return target;
        }


        public static int bossSkillSelection(int sp, out int finalAttack)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int attackValue = 0;
            int skillRoll = 0;
            int attackModifier = 0;
            int targetMethod = 0;

            if (sp < 2)
            {
                //normal attack
                attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
                attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.9 * Simulation.dummyPower);
                targetMethod = 1;
            }
            else if (sp < 4)
            {
                // 1 sp skill AI
                skillRoll = rnd.Next(0, 100);
                if (skillRoll < 20)
                {
                    attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
                    attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.9 * Simulation.dummyPower);
                    targetMethod = 1;
                }
                else if (skillRoll >= 20 && skillRoll < 60)
                {
                    float skillModifier = (rnd.Next(0, 126) + 94);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 2;
                    targetMethod = 1;
                }
                else if (skillRoll >= 60)
                {
                    float skillModifier = (rnd.Next(0, 132) + 99);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 2;
                    targetMethod = 2;
                }
            }
            else if (sp < 6)
            {
                // 1 - 2 sp skill AI
                skillRoll = rnd.Next(0, 100);
                if (skillRoll < 15)
                {
                    attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
                    attackValue = Convert.ToInt32(rnd.Next(1, attackModifier) + 0.9 * Simulation.dummyPower);
                    targetMethod = 1;
                }
                else if (skillRoll >= 15 && skillRoll < 55)
                {
                    float skillModifier = (rnd.Next(0, 126) + 94);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 2;
                    targetMethod = 1;
                }
                else if (skillRoll >= 55 && skillRoll < 95)
                {
                    float skillModifier = (rnd.Next(0, 132) + 99);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 2;
                    targetMethod = 2;
                }
                else if (skillRoll >= 95)
                {
                    float skillModifier = (rnd.Next(0, 136) + 102);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 4;
                    targetMethod = 3;
                }
            }
            else if (sp < 8)
            {
                // 1 - 2 sp skill AI
                skillRoll = rnd.Next(0, 100);
                if (skillRoll < 5)
                {
                    attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
                    attackValue = Convert.ToInt32(rnd.Next(1, attackModifier) + 0.9 * Simulation.dummyPower);
                    targetMethod = 1;
                }
                else if (skillRoll >= 5 && skillRoll < 50)
                {
                    float skillModifier = (rnd.Next(0, 126) + 94);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 2;
                    targetMethod = 1;
                }
                else if (skillRoll >= 50 && skillRoll < 95)
                {
                    float skillModifier = (rnd.Next(0, 132) + 99);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 2;
                    targetMethod = 2;
                }
                else if (skillRoll >= 95)
                {
                    float skillModifier = (rnd.Next(0, 136) + 102);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 4;
                    targetMethod = 3;
                }
            }
            else if (sp == 8)
            {
                // 1 - 2 sp skill AI
                skillRoll = rnd.Next(0, 100);
                if (skillRoll < 0)
                {
                    attackModifier = Convert.ToInt32(0.2 * Simulation.dummyPower);
                    attackValue = Convert.ToInt32(rnd.Next(1, attackModifier) + 0.9 * Simulation.dummyPower);
                    targetMethod = 1;
                }
                else if (skillRoll >= 0 && skillRoll < 45)
                {
                    float skillModifier = (rnd.Next(0, 126) + 94);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 2;
                    targetMethod = 1;
                }
                else if (skillRoll >= 45 && skillRoll < 95)
                {
                    float skillModifier = (rnd.Next(0, 132) + 99);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 2;
                    targetMethod = 2;
                }
                else if (skillRoll >= 95)
                {
                    float skillModifier = (rnd.Next(0, 136) + 102);
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.dummyPower * skillModifier);
                    Simulation.spDummy -= 4;
                    targetMethod = 3;
                }
            }
            finalAttack = attackValue;
            return targetMethod;
        }


		public static void combatExecution(int k, int attackValue) {
			//int targetMethod = 0;
			int initialTarget = 0;
			int target = 0;
			bool blockRoll, evadeRoll, deflectRoll;
			if (target == k) {
				target = 1;
				initialTarget = 1;
			}


			deflectRoll = Logic.RNGroll (OneVsOne.hero [target].deflectChance);
			//while loop that takes in account potentially infinite delfect loops
			while (deflectRoll) {
				Console.WriteLine ("{0} deflected the attack!\n", OneVsOne.hero[target].heroName);
				if (target != k) {
					target = k;
				} else {
					target = initialTarget;
				}
				deflectRoll = Logic.RNGroll (OneVsOne.hero [target].deflectChance);
			}
			//

			//following IFs statements to take account of defensive stats of OneVsOne.hero
			if (!deflectRoll)
			{
				evadeRoll = Logic.RNGroll(OneVsOne.hero[target].evadeChance);
				if (!evadeRoll)
				{
					blockRoll = Logic.RNGroll(OneVsOne.hero[target].blockChance);
					if (blockRoll)
					{
						Console.WriteLine("block successful! {0} dealt {1} on {2}\n", OneVsOne.hero[k].heroName, Convert.ToInt32(0.5 * attackValue), OneVsOne.hero[target].heroName);
						OneVsOne.hero[target].hp -= Convert.ToInt32(0.5 * attackValue);
						PetLogic.petSelection1v1(k);
						if (OneVsOne.hero [target].damageReturn > 0f) {
							OneVsOne.hero [k].hp = OneVsOne.hero [k].hp - Convert.ToInt32(0.5 * attackValue * OneVsOne.hero [target].damageReturn);
						}
						if (OneVsOne.hero [k].lifeSteal > 0f) {
							OneVsOne.hero [k].hp = OneVsOne.hero [k].hp + Convert.ToInt32 (attackValue * OneVsOne.hero [k].lifeSteal);
						}
						if (OneVsOne.hero [k].drain) {
							Console.WriteLine ("{0} has drained for {1} hp\n", OneVsOne.hero [k].heroName, Convert.ToInt32(0.5 * attackValue));
							OneVsOne.hero [k].hp += Convert.ToInt32(0.5 * attackValue);
						}
						if (OneVsOne.hero[target].hp <= 0)
						{
							OneVsOne.hero[target].alive = false;
							//Console.WriteLine ("{0} died\n", OneVsOne.hero [target].heroName);
						}
						else
						{
							PetLogic.petSelection1v1(target);
						}
					}
					else
					{
						Console.WriteLine("{0} dealt {1} on {2}\n", OneVsOne.hero[k].heroName, attackValue, OneVsOne.hero[target].heroName);
						OneVsOne.hero[target].hp -= attackValue;
						PetLogic.petSelection1v1(k);
						if (OneVsOne.hero [target].damageReturn > 0f) {
							OneVsOne.hero [k].hp = OneVsOne.hero [k].hp - Convert.ToInt32(attackValue * OneVsOne.hero [target].damageReturn);
						}
						if (OneVsOne.hero [k].drain) {
							Console.WriteLine ("{0} has drained for {1} hp\n", OneVsOne.hero [k].heroName, attackValue);
							OneVsOne.hero [k].hp += attackValue;
						}
						if (OneVsOne.hero [k].lifeSteal > 0f) {
							OneVsOne.hero [k].hp = OneVsOne.hero [k].hp + Convert.ToInt32 (attackValue * OneVsOne.hero [k].lifeSteal);
						}
						if (OneVsOne.hero[target].hp <= 0)
						{
							OneVsOne.hero[target].alive = false;
						}
						else
						{
							PetLogic.petSelection1v1(target);
						}
					}
				}
				else
				{ 
					Console.WriteLine("evade successful for {0}!\n", OneVsOne.hero[target].heroName); 
				}
			}
			for (int i = 0; i < 2; i++) {
				if (OneVsOne.hero [i].hp <= 0) {
					OneVsOne.hero[i].alive = false;
					Console.WriteLine ("{0} died\n", OneVsOne.hero [i].heroName);
				}
			}
			Console.WriteLine ("{0} hp = {1} ; {2} hp = {3}\n", OneVsOne.hero [0].heroName, OneVsOne.hero [0].hp, OneVsOne.hero [1].heroName, OneVsOne.hero [1].hp);
		}
    }
}
