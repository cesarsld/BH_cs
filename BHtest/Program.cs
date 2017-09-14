using System;

namespace BHtest
{
    class Program
    {
		
        static void Main(string[] args)
        {
        
            /*int i=0, a=0, b=0;
            bool te;
            int s;
            for (s = 0; s < 10; s++) {
                a = 0;
                b = 0;
                for (i = 0; i < 1000000; i++)
                {
                    te = Logic.RNGroll(10f);
                    if (te)
                    {
                        a++;
                    }
                    else { b++; }
                }
                Console.WriteLine(a);
                Console.WriteLine(b); }*/
            
            //Console.WriteLine("Hello World!");
			//OneVsOne.duel();
			TankSim();
            //Simulation.hellosim();
            Console.ReadLine();
        }

		static void accsim() {
			int i;
			int sim = 100000;
			//int roll1 = 0, roll2 = 0, roll3 = 0;
			float deflectcount = 0, evadecount = 0, blockcount = 0, absorbcount = 0;
			float innatedmgreduc = 0.8f;
			float absorb = 5f, deflect = 0f, evade = 2.5f, block = 0f;
			float dmgreduction = 0;
			int proccount = 0;
			float dproc, eproc, bproc, aproc;
			for (i = 0; i<sim; i++) 
			{
				if (!Logic.RNGroll (absorb)) 
				{
					if (!Logic.RNGroll (deflect)) 
					{
						if (!Logic.RNGroll (evade)) 
						{
							if (Logic.RNGroll (block)) 
							{
								blockcount++;
							}
						} else 
						{
							evadecount++;
						}
					} else 
					{
						deflectcount++;
					}
				} else 
				{
					absorbcount++;
				}

			}
			aproc = 100 * (absorbcount / sim);
			dproc = 100 * (deflectcount / sim);
			eproc = 100 * (evadecount / sim);
			bproc = 100 * (blockcount / sim);


			proccount = Convert.ToInt32(deflectcount + evadecount + blockcount + absorbcount);
			Console.WriteLine ("absorb = {0}/100000 \ndeflect = {1}/100000 \nevade = {2}/100000\nblock = {3}/100000\n ", absorbcount, deflectcount, evadecount, blockcount);
			dmgreduction = 100f * (1 - ((sim - proccount + blockcount * 0.5f) / sim) * innatedmgreduc);
			Console.WriteLine ("Damage mitigtion = {0}%\n", dmgreduction);

		}


		static void TankSim()
		{
			int counterMax = 100;
			int baseDamage = 50;
			int damageMultiplier = 0;
			int currentDamage;
			int i;
			int j;
			bool blockRoll, evadeRoll, deflectRoll, absorbRoll;
			int runs = 100;
			int multiplierSum = 0;

			Hero hero = new Hero ();
			hero.power = 480;
			hero.stamina = 1080;
			hero.agility = 142;
			hero.sp = 4;
			hero.shield = 0;
			hero.critChance = 10f;
			hero.critDamage = 1.5f;
			hero.dsChance = 0f;
			hero.blockChance = 32.8f;
			hero.evadeChance = 21.7f;
			hero.deflectChance = 8.9f;
			hero.absorbChance = 6f;
			hero.damageReduction = 1f;
			hero.powerRunes = 0f;
			hero.agilityRunes = 0f;
			hero.pet = "gemmi";
			hero.currentPet = Hero.Pet.Gemmi;
			hero.alive = true;

			hero.powerRunes = (100f + hero.powerRunes) / 100f;
			hero.agilityRunes = (100f + hero.agilityRunes) / 100f;
			hero.damageReduction = (100f - hero.damageReduction) / 100f;
			hero.turnRate = Logic.turnRate(hero.power, hero.agility);
			hero.power = Convert.ToInt32(hero.power * hero.powerRunes);
			hero.turnRate *= hero.agilityRunes;
			hero.maxHp = hero.stamina * 10;
			hero.maxShield = hero.stamina * 5;
			hero.interval = counterMax / hero.turnRate;

			for (j = 0; j < runs; j++) {
				hero.alive = true;
				damageMultiplier = 0;
				while (hero.alive) {
					damageMultiplier++;
					currentDamage = baseDamage * damageMultiplier;

					hero.hp = hero.maxHp;
					hero.shield = 0;

					for (i = 0; i < 100; i++) {
						currentDamage = baseDamage * damageMultiplier;
						blockRoll = Logic.RNGroll (hero.blockChance);
						evadeRoll = Logic.RNGroll (hero.evadeChance);
						deflectRoll = Logic.RNGroll (hero.deflectChance);
						absorbRoll = Logic.RNGroll (hero.absorbChance);
						if (!absorbRoll) {
							if (!deflectRoll) {
								if (!evadeRoll) {
									if (!blockRoll) {
										if (hero.shield > 0) {
											if (currentDamage * hero.damageReduction > hero.shield) {
												currentDamage -= Convert.ToInt32 (hero.shield / hero.damageReduction);
												hero.shield = 0;
											} else {
												hero.shield -= Convert.ToInt32 (currentDamage * hero.damageReduction);
												currentDamage = 0;
											}
										}
										hero.hp -= Convert.ToInt32 (currentDamage * hero.damageReduction);
										if (hero.hp <= 0) {
											if (!LuvboiProc (hero)) {
												hero.alive = false;
												multiplierSum += damageMultiplier;
												break;
											}
										} else {
											PetProc (hero);
										}
									} else {

										if (hero.shield > 0) {
											if (currentDamage * hero.damageReduction > hero.shield) {
												currentDamage -= Convert.ToInt32 (hero.shield / hero.damageReduction);
												hero.shield = 0;
											} else {
												hero.shield -= Convert.ToInt32 (currentDamage * hero.damageReduction);
												currentDamage = 0;
											}
										}

										currentDamage = Convert.ToInt32 (currentDamage * hero.damageReduction / 2);
										hero.hp -= currentDamage;
										if (hero.hp <= 0) {
											if (!LuvboiProc (hero)) {
												hero.alive = false;
												multiplierSum += damageMultiplier;
												break;
											}
										} else {
											PetProc (hero);
										}
									}
								} else {
									PetProc (hero);
								}
							} else 
							{
								PetProc (hero);
							}
						} else {
							hero.shield += currentDamage;
							PetProc (hero);
						}
					}
				}
			}
			int floor;
			floor = Convert.ToInt32(multiplierSum / j);
			Console.WriteLine ("On average, hero died at level {0}.", floor);
		}

		static void PetProc(Hero hero)
		{
			switch (hero.currentPet) 
			{
			case Hero.Pet.Boiguh:
				BoiguhProc (hero);
				break;
			case Hero.Pet.Gemmi:
				GemmiProc (hero);
				break;
			case Hero.Pet.Boogie:
				BoogieProc (hero);
				break;
			default:
				break;
			}
		}

		static bool LuvboiProc (Hero hero)
		{
			hero.hp = 0;
			if (hero.currentPet != Hero.Pet.Luvboi) {
				return false;
			} else {
				bool petRoll = Logic.RNGroll (40f);
				if (petRoll) {
					hero.hp = hero.power * 2;
				}
				return petRoll;
			}
		}

		static void BoiguhProc (Hero hero)
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());

			int shieldModifier = Convert.ToInt32(hero.power * 0.06);
			float shieldValue = Convert.ToInt32(rnd.Next(0, shieldModifier) + 0.27 * hero.power);

			bool petRoll = Logic.RNGroll(20f);


			if (petRoll)
			{
				if (hero.hp > 0) 
				{
					hero.shield += Convert.ToInt32 (shieldValue);
					if (hero.shield >= hero.maxShield) 
					{
						hero.shield = hero.maxShield;
					}
				}
			}
		}

		static void GemmiProc (Hero hero)
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			int i;
			int healModifier = Convert.ToInt32(hero.power * 0.072);
			float healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.324 * hero.power);
			bool critoll = Logic.RNGroll (10f);
			bool petRoll = Logic.RNGroll(22f);

			if (critoll) {
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

		static void BoogieProc (Hero hero)
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());

			int healModifier = Convert.ToInt32(hero.power * 0.14);
			int healValue = Convert.ToInt32(rnd.Next(0, healModifier) + 0.66 * hero.power);

			bool petRoll = Logic.RNGroll(20f);

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