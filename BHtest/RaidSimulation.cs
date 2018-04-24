using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class RaidSimulation
{
    public Character[] heroes = new Character[5];
    public Character[] enemies = new Character[1];
    private bool isNotHero = false;
    public int difficultyModifier;
    public float winRate;
    public int progressionBar = 0;

    public int redirectCount = 0;
    public int aliveCount = 5;
    public int games = 1000;//number of times fight will run.
    private bool heroesAlive
    {
        get
        {
            return GetPartyCount(heroes) > 0;
        }
    }
    private bool enemiesAlive
    {
        get
        {
            return GetPartyCount(enemies) > 0;
        }
    }
    private bool matchOver
    {
        get
        {
            return !heroesAlive || !enemiesAlive;
        }
    }

    public void Simulation(int boss)
    {

        int p;
        redirectCount = 0;

        float win = 0;
        float lose = 0;

        int games = 1000;//number of times fight will run.
        int gameDivider = Convert.ToInt32(games / 100);
        progressionBar = 0;

        foreach (Character hero in heroes)
        {  //initialisation
            hero.InitialiseHero();
        }
        for (p = 0; p < games; p++)
        {
            float trCounter = 0;
            SetupEnemies(boss);
            Character[] charArray = new Character[heroes.Length + enemies.Length];
            int charIndex = 0;
            foreach (Character hero in heroes)
            {
                hero.Revive();
                trCounter += hero.turnRate;
                charArray[charIndex] = hero;
                charIndex++;
            }
            foreach (Character enemy in enemies)
            {
                trCounter += enemy.turnRate;
                charArray[charIndex] = enemy;
                charIndex++;
            }
            charArray = charArray.OrderByDescending(chr => chr.turnRate).ToArray();
            while (heroesAlive && enemiesAlive)
            {
                foreach (Character character in charArray)
                {
                    if (character.alive)
                    {
                        character.IncrementCounter();
                        if (character.counter > trCounter)
                        {
                            character.IncrementSp();
                            if (character._isHero)
                            {
                                if (character.pet != null) character.pet.PetSelection(character, heroes, enemies, PetProcType.PerTurn);
                                if (matchOver) break;
                                character.ChooseSkill(heroes, enemies);
                            }
                            else
                            {
                                if (character.pet != null) character.pet.PetSelection(character, enemies, heroes, PetProcType.PerTurn);
                                if (matchOver) break;
                                character.ChooseSkill(enemies, heroes);
                            }
                            character.SubstractCounter(trCounter);
                        }
                    }
                }
                //if (matchOver) break;
                //foreach (Character enemy in enemies)
                //{
                //    if (enemy.alive)
                //    {
                //        enemy.IncrementCounter();
                //        if (enemy.counter > trCounter)
                //        {
                //            Logic.HpPerc(enemies);
                //            Logic.HpPerc(heroes);
                //            enemy.IncrementSp();
                //            if (enemy.pet != null) enemy.pet.PetSelection(enemy, enemies, heroes, PetProcType.PerTurn);
                //            if (matchOver) break;
                //            enemy.ChooseSkill(enemies, heroes);
                //            enemy.SubstractCounter(trCounter);
                //        }
                //    }
                //}
                //if (matchOver) break;

            }
            if (heroesAlive)
            {
                win++;
            }
            else
            {
                lose++;
            }
            if ((float)p % gameDivider == 0 && p > 0)
            {
                progressionBar += 1;

                winRate = (win / p) * 100;

            }
        }


        winRate = (win / p) * 100;
        Console.WriteLine("winrate is =" + winRate.ToString());
    }

    private void SetupEnemies(int boss)
    {
        //int bossType = rand.Next(3);
        enemies[0] = GetRaidBoss(boss);
        enemies[0].InitialiseMobs();
    }




    private Character GetRaidBoss(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "Kaleido");
            case 1:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9, isNotHero, "Woodbeard");
            default:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "Robomech");
        }
    }
    public static int GetPartyCount(Character[] opponents)
    {
        return opponents.Count(member => member.alive);
    }
    public static Boolean IsAoeEnabled(Character[] opponents)
    {
        if (opponents.Count(member => member.alive) > 2) return Boolean.True;
        else return Boolean.False;
    }
}