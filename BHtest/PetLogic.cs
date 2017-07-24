using System;
using System.Collections.Generic;
using System.Text;

namespace BHtest
{
    class PetLogic
    {

        public static void offPetProc(int l)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int attackModifier = Convert.ToInt32(0.54 * Simulation.hero[l].power);
            int attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + Simulation.hero[l].power * 0.63);


            bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
            bool petRoll = Logic.RNGroll((float)20);

            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
            }
            if (petRoll)
            {
                Simulation.hpDummy -= attackValue;
                //Console.WriteLine("\npet proc successful\n");
            }

        }

		public static void offPetProc1v1(int l)
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			int attackModifier = Convert.ToInt32(0.54 * OneVsOne.hero[l].power);
			int attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + OneVsOne.hero[l].power * 0.63);
			int target = 0;
			if (l == 0) {
				target = 1;
			}


			bool critRoll = Logic.RNGroll(OneVsOne.hero[l].critChance);
			bool petRoll = Logic.RNGroll((float)20);

			if (critRoll)
			{
				attackValue = Convert.ToInt32(attackValue * OneVsOne.hero[l].critDamage);
			}
			if (petRoll)
			{
				Console.WriteLine ("{0}'s pet proced dealing {1} damage!\n", OneVsOne.hero[l].heroName, attackValue);
				OneVsOne.hero[target].hp -= attackValue;
			}

		}

        public static void superOffPetProc(int l)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int attackModifier = Convert.ToInt32(Simulation.hero[l].power * 0.37);
            int attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + Simulation.hero[l].power * 1.668);


            bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
            bool petRoll = Logic.RNGroll((float)10);

            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * Simulation.hero[l].critDamage);
            }
            if (petRoll)
            {
                Simulation.hpDummy -= attackValue;
            }

        }

		public static void superOffPetProc1v1(int l)
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			int attackModifier = Convert.ToInt32(OneVsOne.hero[l].power * 0.37);
			int attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + OneVsOne.hero[l].power * 1.668);
			int target = 0;
			if (l == 0) {
				target = 1;
			}

			bool critRoll = Logic.RNGroll(OneVsOne.hero[l].critChance);
			bool petRoll = Logic.RNGroll((float)10);

			if (critRoll)
			{
				attackValue = Convert.ToInt32(attackValue * OneVsOne.hero[l].critDamage);
			}
			if (petRoll)
			{
				Console.WriteLine ("{0}'s pet proced dealing {1} damage!\n", OneVsOne.hero[l].heroName, attackValue);
				OneVsOne.hero[target].hp -= attackValue;
			}

		}

        public static void spreadHealPet(int l)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int i;
            int target = 0;
            int healModifier = Convert.ToInt32(Simulation.hero[l].power * 0.14);
            int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.66 * Simulation.hero[l].power);

            bool critRoll = Logic.RNGroll(Simulation.hero[l].critChance);
            bool petRoll = Logic.RNGroll((float)20);

            if (critRoll)
            {
                healValue = Convert.ToInt32(healValue * Simulation.hero[l].critDamage);
            }
            if (petRoll)
            {
                for (i = 0; i < healValue; i++)
                {
                    target = Logic.healLogic();
                    Simulation.hero[target].hp++;
                    if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
                    {
                        Simulation.hero[target].hp = Simulation.hero[target].maxHp;
                    }
                }
            }
        }

		public static void spreadHealPet1v1(int l)
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			//int i;
			//int target = 0;
			int healModifier = Convert.ToInt32(OneVsOne.hero[l].power * 0.14);
			int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.66 * OneVsOne.hero[l].power);

			bool critRoll = Logic.RNGroll(OneVsOne.hero[l].critChance);
			bool petRoll = Logic.RNGroll((float)20);

			if (critRoll)
			{
				healValue = Convert.ToInt32(healValue * OneVsOne.hero[l].critDamage);
			}
			if (petRoll)
			{
				Console.WriteLine ("{0}'s pet proced healing for {1} hp!\n", OneVsOne.hero[l].heroName, healValue);
				OneVsOne.hero[l].hp += healValue;
				if (OneVsOne.hero[l].hp > OneVsOne.hero[l].maxHp)
				{
					OneVsOne.hero[l].hp = OneVsOne.hero[l].maxHp;
				}
				
			}
		}

		public static void superSpreadHealPet1v1(int l)
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			//int i;
			//int target = 0;
			int healModifier = Convert.ToInt32(OneVsOne.hero[l].power * 0.30);
			int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 1.30 * OneVsOne.hero[l].power);

			bool critRoll = Logic.RNGroll(OneVsOne.hero[l].critChance);
			bool petRoll = Logic.RNGroll((float)10);

			if (critRoll)
			{
				healValue = Convert.ToInt32(healValue * OneVsOne.hero[l].critDamage);
			}
			if (petRoll)
			{
				Console.WriteLine ("{0}'s pet proced healing for {1} hp!\n", OneVsOne.hero[l].heroName, healValue);
				OneVsOne.hero[l].hp += healValue;
				if (OneVsOne.hero[l].hp > OneVsOne.hero[l].maxHp)
				{
					OneVsOne.hero[l].hp = OneVsOne.hero[l].maxHp;
				}

			}
		}

		public static void superSelfHealPet1v1(int l)
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			//int i;
			//int target = 0;
			int healModifier = Convert.ToInt32(OneVsOne.hero[l].power * 0.56);
			int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.89 * OneVsOne.hero[l].power);

			bool critRoll = Logic.RNGroll(OneVsOne.hero[l].critChance);
			bool petRoll = Logic.RNGroll((float)10);

			if (critRoll)
			{
				healValue = Convert.ToInt32(healValue * OneVsOne.hero[l].critDamage);
			}
			if (petRoll)
			{
				Console.WriteLine ("{0}'s pet proced healing for {1} hp!\n", OneVsOne.hero[l].heroName, healValue);
				OneVsOne.hero[l].hp += healValue;
				if (OneVsOne.hero[l].hp > OneVsOne.hero[l].maxHp)
				{
					OneVsOne.hero[l].hp = OneVsOne.hero[l].maxHp;
				}

			}
		}

        // strcmp to find what pet Simulation.hero is using
        public static void petSelection(int k)
        {
            bool petCheck;
            petCheck = string.Equals(Simulation.hero[k].pet, "gemmi");
            if (petCheck)
            {
                Logic.teamHeal(k);
            }
            petCheck = string.Equals(Simulation.hero[k].pet, "nelson");
            if (petCheck)
            {
                offPetProc(k);
            }
            petCheck = string.Equals(Simulation.hero[k].pet, "boogie");
            if (petCheck)
            {
                spreadHealPet(k);
            }
            petCheck = string.Equals(Simulation.hero[k].pet, "nemo");
            if (petCheck)
            {
                superOffPetProc(k);
            }

        }

		public static void petSelection1v1(int k)
		{
			bool petCheck;
			petCheck = string.Equals(OneVsOne.hero[k].pet, "gemmi");
			if (petCheck)
			{
				Logic.teamHeal1v1(k);
			}
			petCheck = string.Equals(OneVsOne.hero[k].pet, "nelson");
			if (petCheck)
			{
				offPetProc1v1(k);
			}
			petCheck = string.Equals(OneVsOne.hero[k].pet, "boogie");
			if (petCheck)
			{
				spreadHealPet1v1(k);
			}
			petCheck = string.Equals(OneVsOne.hero[k].pet, "nemo");
			if (petCheck)
			{
				superOffPetProc1v1(k);
			}
			petCheck = string.Equals(OneVsOne.hero[k].pet, "crem");
			if (petCheck)
			{
				superSpreadHealPet1v1(k);
			}
			petCheck = string.Equals(OneVsOne.hero[k].pet, "nerder");
			if (petCheck)
			{
				superSelfHealPet1v1(k);
			}



		}


    }
}
