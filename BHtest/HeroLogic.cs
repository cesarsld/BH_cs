using System;
using System.Collections.Generic;
using System.Text;

namespace BHtest
{
    class HeroLogic
    {

        public static void spreadHealingSkill(int k)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int i;
            int target = 0;
            int healingValue = 0;
            int healingModifier = Convert.ToInt32(0.365 * Simulation.hero[k].power);

            healingValue = Convert.ToInt32(rnd.Next(0, healingModifier) + 0.73 * Simulation.hero[k].power);

            bool critRoll = Logic.RNGroll(Simulation.hero[k].critChance);
            if (critRoll)
            {
                healingValue = Convert.ToInt32(healingValue * Simulation.hero[k].critDamage);
            }

            for (i = 0; i < healingValue; i++)
            {
                target = Logic.healLogic();
                Simulation.hero[target].hp++;
                if (Simulation.hero[target].hp > Simulation.hero[target].maxHp)
                {
                    Simulation.hero[target].hp = Simulation.hero[target].maxHp;
                }
            }
        }

        public static void heroAttack(int k, bool dual)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int skillSelection;
            int attackValue = 0;
            bool hasHealed = false;
            int attackModifier = Convert.ToInt32(0.2 * Simulation.hero[k].power);
            attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.9f * Simulation.hero[k].power);
            if (Simulation.hero[k].sp >= 2)
            {
                skillSelection = rnd.Next(0, 100);
                if (skillSelection < 20 && (Simulation.hero[0].hpPerc < 0.85f || Simulation.hero[4].hpPerc < 0.85f))
                {
                    spreadHealingSkill(k);
                    hasHealed = true;
                    if (!dual)
                    {
                        Simulation.hero[k].sp -= 2;
                    }
                }
                else
                {
                    float skillModifier = rnd.Next(0, 50) + 110;
                    skillModifier /= 100;
                    attackValue = Convert.ToInt32(Simulation.hero[k].power * skillModifier);
                    if (!dual)
                    {
                        Simulation.hero[k].sp -= 2;
                    }
                }
            }
            bool critRoll = Logic.RNGroll(Simulation.hero[k].critChance);
            if (critRoll)
            {
                attackValue = Convert.ToInt32(attackValue * Simulation.hero[k].critDamage);
            }
            bool evadeRoll = Logic.RNGroll(2.5f);
            if (!evadeRoll && hasHealed == false)
            {
                Simulation.hpDummy -= attackValue;
                PetLogic.petSelection(k);
            }
        }

        public static void heroAttackTarget(int k, bool dual, int target)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int skillSelection;
            int attackValue = 0;
            bool hasHealed = false;
			int attackModifier = Convert.ToInt32(0.2 * OneVsOne.hero[k].power);
			attackValue = Convert.ToInt32(rnd.Next(0, attackModifier) + 0.9f * OneVsOne.hero[k].power);
			if (OneVsOne.hero[k].sp >= 2)
            {
                skillSelection = rnd.Next(0, 100);
				if (skillSelection < 20 && (OneVsOne.hero[0].hpPerc < 0.85f || OneVsOne.hero[4].hpPerc < 0.85f))
                {
                    spreadHealingSkill(k);
                    hasHealed = true;
                    if (!dual)
                    {
						OneVsOne.hero[k].sp -= 2;
                    }
                }
                else
                {
                    float skillModifier = rnd.Next(0, 50) + 110;
                    skillModifier /= 100;
					attackValue = Convert.ToInt32(OneVsOne.hero[k].power * skillModifier);
                    if (!dual)
                    {
						OneVsOne.hero[k].sp -= 2;
                    }
                }
            }
			bool critRoll = Logic.RNGroll(OneVsOne.hero[k].critChance);
            if (critRoll)
            {
				attackValue = Convert.ToInt32(attackValue * OneVsOne.hero[k].critDamage);
            }
            bool evadeRoll = Logic.RNGroll(2.5f);
            if (!evadeRoll && hasHealed == false)
            {
                target -= attackValue;
                PetLogic.petSelection1v1(k);
            }
        }

		public static void swordSkillSelection(int k, int sp, bool unity, bool DS) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			int attackValue = 0;
			int skillRoll = 0;
			//int targetMethod = 0;

			if (sp < 2)
			{
				attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
				Logic.combatExecution (k, attackValue);
				if (DS) {
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
			}
			else if (sp < 4)
			{
				// 1 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 20)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 2440 && skillRoll < 0)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 20)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sTarget_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sTarget_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}
			else if (sp < 6)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 20)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 1445 && skillRoll < 55)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 20 && skillRoll < 1000)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sTarget_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sTarget_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 995)
				{
					OneVsOne.hero [k].sp -= 4;
					attackValue = SkillList.sAoe2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sAoe2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}
			else if (sp < 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 20)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 555 && skillRoll < 50)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 20 && skillRoll < 80)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sTarget_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sTarget_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 80)
				{
					OneVsOne.hero [k].sp -= 6;
					OneVsOne.hero [k].drain = true;
					attackValue = SkillList.sClosest3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sClosest3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero [k].drain = false;
				}
			}
			else if (sp == 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 0)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 110 && skillRoll < 45)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 0 && skillRoll < 70)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sTarget_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sTarget_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 70)
				{
					OneVsOne.hero [k].sp -= 6;
					OneVsOne.hero [k].drain = true;
					attackValue = SkillList.sClosest3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sClosest3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero [k].drain = false;
				}
			}
		}

		public static void spearSkillSelection(int k, int sp, bool unity, bool DS) { 
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			int attackValue = 0;
			int skillRoll = 0;
			//int targetMethod = 0;

			if (sp < 2)
			{
				attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
				Logic.combatExecution (k, attackValue);
				if (DS) {
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
			}
			else if (sp < 4)
			{
				// 1 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 20)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 20 && skillRoll < 101)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.spBack1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.spBack1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 600)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}
			else if (sp < 6)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 10)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 10 && skillRoll < 60)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.spBack1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.spBack1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 5555 && skillRoll < 95)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.sPierce3_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 60)
				{
					OneVsOne.hero [k].sp -= 4;
					attackValue = SkillList.spTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.spTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}
			else if (sp < 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 10)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 10 && skillRoll < 30)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.spBack1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.spBack1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 30 && skillRoll < 60)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.spTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.spTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 60)
				{
					OneVsOne.hero [k].sp -= 6;
					attackValue = SkillList.spClosest3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.spClosest3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}
			else if (sp == 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 0)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 0 && skillRoll < 20)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.spBack1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.spBack1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 20 && skillRoll < 60)
				{
					OneVsOne.hero [k].sp -= 4;
					attackValue = SkillList.spTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.spTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 60)
				{
					OneVsOne.hero [k].sp -= 6;
					attackValue = SkillList.spClosest3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.spClosest3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}

		}

		public static void bowSkillSelection(int k, int sp, bool unity, bool DS) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			int attackValue = 0;
			int skillRoll = 0;
			//int targetMethod = 0;
			if (sp < 2)
			{
				attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
				Logic.combatExecution (k, attackValue);

				if (DS) {
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
			}
			else if (sp < 4)
			{
				// 1 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 20)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 20 && skillRoll < 100)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.bTarget1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bTarget1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 2;
				}
				else if (skillRoll == 600)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.bAoe1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bAoe1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 2;
				}
			}
			else if (sp < 6)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 15)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 15 && skillRoll < 80)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.bTarget1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bTarget1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 2;
				}
				else if (skillRoll >= 505 && skillRoll < 95)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.bAoe1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bAoe1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 2;
				}
				else if (skillRoll >= 80)
				{
					OneVsOne.hero [k].sp -= 4;
					attackValue = SkillList.bBack2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bBack2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 4;
				}
			}
			else if (sp < 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 5)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 5 && skillRoll < 80)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.bTarget1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bTarget1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 2;
				}
				else if (skillRoll >= 500 && skillRoll < 95)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.bAoe1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bAoe1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 2;
				}
				else if (skillRoll >= 80)
				{
					OneVsOne.hero [k].sp -= 4;
					attackValue = SkillList.bBack2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bBack2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 4;
				}
			}
			else if (sp == 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 0)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 900 && skillRoll < 45)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.bAoe1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bAoe1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 2;
				}
				else if (skillRoll >= 0 && skillRoll < 60)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.bTarget1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bTarget1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 2;
				}
				else if (skillRoll >= 60)
				{
					OneVsOne.hero [k].sp -= 4;
					attackValue = SkillList.bBack2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.bBack2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
					OneVsOne.hero[k].sp -= 4;
				}
			}
		}

		public static void staffSkillSelection(int k, int sp, bool unity, bool DS) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			int attackValue = 0;
			int skillRoll = 0;
			//int targetMethod = 0;

			if (sp < 2)
			{
				attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
				Logic.combatExecution (k, attackValue);
			}
			else if (sp < 4)
			{
				// 1 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 20)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution (k, attackValue);
					OneVsOne.hero [k].sp -= 2;
				}
				else if (skillRoll >= 20 && skillRoll < 60)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.stClosest1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
				else if (skillRoll >= 60)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.stClosest1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
			}
			else if (sp < 6)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 15)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
				}
				else if (skillRoll >= 15 && skillRoll < 55)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.stClosest1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
				else if (skillRoll >= 55 && skillRoll < 95)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.stClosest1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
				else if (skillRoll >= 95)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.stClosest1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
			}
			else if (sp < 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 5)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
				}
				else if (skillRoll >= 5 && skillRoll < 50)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.stClosest1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
				else if (skillRoll >= 50 && skillRoll < 70)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.stClosest1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
				else if (skillRoll >= 70)
				{
					OneVsOne.hero [k].sp -= 6;
					attackValue = SkillList.stTarget3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
			}
			else if (sp == 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 0)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
				}
				else if (skillRoll >= 0 && skillRoll < 45)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.stClosest1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
				else if (skillRoll >= 45 && skillRoll < 50)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.stTarget3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
				else if (skillRoll >= 50)
				{
					OneVsOne.hero [k].sp -= 6;
					attackValue = SkillList.stTarget3sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
			}
		}

		public static void axeSkillSelection(int k, int sp, bool unity, bool DS) {
			Random rnd = new Random(Guid.NewGuid().GetHashCode());
			int attackValue = 0;
			int skillRoll = 0;
			//int targetMethod = 0;

			if (sp < 2)
			{
				attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
				Logic.combatExecution (k, attackValue);
				if (DS) {
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution (k, attackValue);
				}
			}
			else if (sp < 4)
			{
				// 1 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 20)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 200 && skillRoll < 60)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.aAoeDrain1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aAoeDrain1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 20)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.aClosest_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aClosest_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}
			else if (sp < 6)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 15)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 105 && skillRoll < 55)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.aAoeDrain1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aAoeDrain1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 15 && skillRoll < 70)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.aClosest_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aClosest_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 70)
				{
					OneVsOne.hero [k].sp -= 4;
					attackValue = SkillList.aTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}
			else if (sp < 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 5)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 500 && skillRoll < 50)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.aAoeDrain1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aAoeDrain1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 5 && skillRoll < 60)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.aClosest_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aClosest_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 60)
				{
					OneVsOne.hero [k].sp -= 4;
					attackValue = SkillList.aTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}
			else if (sp == 8)
			{
				// 1 - 2 sp skill AI
				skillRoll = rnd.Next(0, 100);
				if (skillRoll < 0)
				{
					attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
					Logic.combatExecution(k, attackValue);
					if (DS) {
						attackValue = SkillList.normalAttack(OneVsOne.hero[k].power, OneVsOne.hero[k].critChance, OneVsOne.hero[k].critDamage);
						Logic.combatExecution(k, attackValue);
					}
				}
				else if (skillRoll >= 1110 && skillRoll < 45)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.aAoeDrain1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aAoeDrain1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 0 && skillRoll < 50)
				{
					OneVsOne.hero [k].sp -= 2;
					attackValue = SkillList.aClosest_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aClosest_1sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
				else if (skillRoll >= 50)
				{
					OneVsOne.hero [k].sp -= 4;
					attackValue = SkillList.aTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
					Logic.combatExecution (k, attackValue);
					if (DS) {
						attackValue = SkillList.aTarget2sp(OneVsOne.hero [k].power, OneVsOne.hero [k].critChance, OneVsOne.hero [k].critDamage);
						Logic.combatExecution (k, attackValue);
					}
				}
			}
		}

		public static void weaponSelection(int k, bool DS)
        {
			if ((int)OneVsOne.hero[k].weapon == 0) {
				bowSkillSelection(k, OneVsOne.hero[k].sp, OneVsOne.hero[k].unity, DS);
			} else if ((int)OneVsOne.hero[k].weapon == 1) {
				spearSkillSelection(k, OneVsOne.hero[k].sp, OneVsOne.hero[k].unity, DS);
			} else if ((int)OneVsOne.hero[k].weapon == 2) {
				swordSkillSelection(k, OneVsOne.hero[k].sp, OneVsOne.hero[k].unity, DS);
			} else if ((int)OneVsOne.hero[k].weapon == 3) {
				staffSkillSelection(k, OneVsOne.hero[k].sp, OneVsOne.hero[k].unity, DS);
			} else if ((int)OneVsOne.hero[k].weapon == 4) {
				axeSkillSelection (k, OneVsOne.hero[k].sp, OneVsOne.hero[k].unity, DS);
			}
        }
    }
}



 /*deflectRoll = Logic.RNGroll (OneVsOne.hero [target].deflectChance);
				while (deflectRoll) {
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
							//Console.WriteLine("\n block successful!\n");
							OneVsOne.hero[target].hp -= Convert.ToInt32(0.5 * attackValue);
							if (OneVsOne.hero[target].hp <= 0)
							{
								OneVsOne.hero[target].alive = false;
							}
							else
							{
								PetLogic.petSelection1v1(target);
							}
						}
						else
						{
							OneVsOne.hero[target].hp -= attackValue;
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
					{ //Console.WriteLine("\n evade successful!\n"); 
					}
				}*/
