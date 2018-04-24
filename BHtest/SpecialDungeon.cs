using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class SpecialDungeon
{

    public Character[] heroes;
    public Character[] enemies;
    public int progressionBar = 0;
    //private Slider slider;
    public int redirectCount = 0;
    private int DifficultyModifier;
    public float winRate;
    public int Games = 1000;
    Random rand;
    private bool isNotHero = false;
    private bool isHero = true;

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



    public SpecialDungeon(int difficultyModifier)
    {
        rand = new Random(Guid.NewGuid().GetHashCode());
        DifficultyModifier = difficultyModifier;
    }

    public void Simulation(int boss)
    {
        //slider = UnityEngine.GameObject.Find("Progress").GetComponent<Slider>();
        int p;
        redirectCount = 0;
        heroes[0].InitialiseHero();
        heroes[1] = ReturnWalogdr();
        heroes[2] = ReturnWalogdr();

        int games = 1000;
        int gameDivider = Convert.ToInt32(games / 100);
        float win = 0;
        float lose = 0;

        heroes[1].InitialiseMobs();
        heroes[2].InitialiseMobs();

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
        Console.WriteLine("winrate is = " + winRate.ToString() + "%");
    }

    private void SetupEnemies(int boss)
    {
        //int bossType = rand.Next(3);
        Character bossPlaceholder;
        if (boss == 0)
        {
            Character placeHolder1 = GetZoneTrash(rand.Next(6));
            Character placeHolder2 = GetZoneTrash(rand.Next(6));
            bossPlaceholder = GetZoneBoss(rand.Next(2));
            List<Character> enemyList = new List<Character>() { placeHolder1, placeHolder2, bossPlaceholder };
            enemies = enemyList.OrderByDescending(mob => mob.priority).ToArray();
        }

        foreach (Character mob in enemies)
        {
            mob.InitialiseMobs();
        }
    }
    private Character GetZoneBoss(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(4.5f, 16, 3.5f, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2.9, isNotHero, "DesertScorpion");
            default:
                return new Character(3, 16, 5, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2.9, isNotHero, "ForestSpirit");
        }
    }
    private Character GetZoneTrash(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(2.5f, 1, 2.5f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 0, isNotHero, "DesertFood");
            case 1:
                return new Character(1.6f, 2.6f, 1.8f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2, isNotHero, "DesertCactus");
            case 2:
                return new Character(2, 1.8f, 2.2f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 1, isNotHero, "DesertVulture");
            case 3:
                return new Character(3f, 1.3f, 1.7f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 0, isNotHero, "ForestGoo");
            case 4:
                return new Character(1, 4, 1, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 2, isNotHero, "ForestTurtle");
            default:
                return new Character(1.2f, 1.8f, 3, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, DifficultyModifier, 1, isNotHero, "ForestRabbit");
        }
    }

    private Character ReturnWalogdr()
    {
        Character Walogdr = new Character(3.1f, 1, 1.3f, 10f, 50f, 20f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, 1, 0, true, "Walogdr");
        Walogdr.GetTsFromHero(Convert.ToInt32(heroes[0].GetTotalTs()));
        return Walogdr;
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
