using System;

namespace BHtest
{
	public class SkillList
	{
		public static int attackModifier;
		public static int attackValue;
	

		//0 sp attack

		public static int normalAttack(int power, float critChance, float critDamage){

			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.2 * power);
			attackValue = Convert.ToInt32(rnd.Next(1, attackModifier) + 0.9 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}


		//Spear skill set


		public static int spBack1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.6 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.3 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int spPierce3_1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.3 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.7 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int spTarget2sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.73 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.46 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int spClosest3sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(1.07 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 2.12 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}

		//Bow skill set


		public static int bTarget1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.545 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.095 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int bAoe1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.25 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.48 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int bBack2sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.85 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.70 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int bAoeDraint3sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.2 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.4 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}

		//Sword skill set

		public static int sTarget_1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.27 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.23 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int sPierce3_1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.16 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.74 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int sAoe2sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.16 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.73 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int sClosest3sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.25 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.14 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}

		//Axe skill set

		public static int aAoeDrain1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.18 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.21 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int aClosest_1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.96 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.11 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int aTarget2sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(1.095 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.28 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int aSpreadHealt3sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			//add spread healing
			return 0;
		}

		//Staff skill set

		public static int stClosest1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.31 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.41 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int stHeal1sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			//heal
			return 0;
		}
		public static int stAOE2sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(1.095 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 1.28 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}
		public static int stTarget3sp (int power, float critChance, float critDamage) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			attackModifier = Convert.ToInt32(0.45 * power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 2.02 * power);
			bool critroll = Logic.RNGroll (critChance);
			if (critroll) {
				attackValue = Convert.ToInt32 (attackValue * critDamage);
			}
			return attackValue;
		}

	}
}

