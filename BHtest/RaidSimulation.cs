using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class RaidSimulation : Simulation
{
    public RaidSimulation(int _difficultyModifier, int playerNumber, HeroPanel[] heroPanel, int _instanceNumber)
    {
        instanceNumber = _instanceNumber;
        difficultyModifier = _difficultyModifier;
        heroes = new Character[playerNumber];
        for (int i = 0; i < playerNumber; i++)
        {
            heroes[i] = heroPanel[i].GetHero();
        }
    }


    public void Simulation(int boss)
    {
        int p;
        float win = 0;
        int games = 1000;
        foreach (Character hero in heroes) hero.InitialiseHero();
        
        for (p = 0; p < games; p++)
        {
            float trCounter = 0;
            SetupEnemies(boss);
            Character[] charArray = InitCharacterArray(ref trCounter);
            while (heroesAlive && enemiesAlive)
            {
                foreach (Character character in charArray)
                {
                    if (character.alive)
                    {
                        character.IncCounter();
                        if (character.counter > trCounter)
                        {
                            if (character._isHero)
                            {
                                character.IncSp(heroes);
                                character.ActivateOnTurnPassives(heroes, enemies);
                                if (character.pet != null) character.pet.PetSelection(character, heroes, enemies, PetProcType.PerTurn);
                                if (simOver) break;
                                character.ChooseSkill(heroes, enemies);
                            }
                            else
                            {
                                character.IncSp(enemies);
                                if (character.pet != null) character.pet.PetSelection(character, enemies, heroes, PetProcType.PerTurn);
                                if (simOver) break;
                                character.ChooseSkill(enemies, heroes);
                            }
                            character.SubCount(trCounter);
                        }
                    }
                }
            }
            if (heroesAlive) win++;
        }
        winRate = (win / p) * 100;
        Console.WriteLine("SingleThread with loops inside sim method results: ");
        Console.WriteLine("winrate is =" + winRate.ToString());
    }

    protected override void SetupEnemies(int boss)
    {
        enemies = new Character[1];
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
            case 2:
                return new Character(10, 18, 4, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1.9, isNotHero, "Robomech");
            default:
                return new Character(8, 23, 5, 10f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 0.9, isNotHero, "Zol"); // prio subject to change
        }
    }

    private Character GetRaidTrash(int index)
    {
        switch (index)
        {
            case 0:
                return new Character(3.4f, 4, 4.6f, 10f, 50f, 10f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "ArcherOrc");
            case 1:
                return new Character(6.2f, 2f, 1.8f, 10f, 50f, 15f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 1, isNotHero, "MageOrc");
            default:
                return new Character(5.8f, 3.8f, 2.4f, 30f, 50f, 0f, 0f, 0f, 2.5f, 0f, 0f, 0f, 0f, 0f, 0f, difficultyModifier, 2, isNotHero, "AssassinOrc");
        }
    }

    public void Simulation1(int boss, Action<float> callback, Func<bool, bool> stopSim, Action<bool> simOutcome)
    {
        if (stopSim(false)) return;
        int safetyNet = 2000;

        foreach (var hero in heroes) hero.InitialiseHero();
        SetupEnemies(boss);
        int turnCount = 0;
        float trCounter = 0;

        Character[] charArray = InitCharacterArray(ref trCounter);
        //UnityEngine.Debug.Log("player stamine  " + heroes[0].hp.ToString());
        while (heroesAlive && enemiesAlive)
        {
            foreach (var character in charArray)
            {
                if (character.alive)
                {
                    character.IncCounter();
                    if (character.counter > trCounter)
                    {
                        if (character._isHero)
                        {
                            character.IncSp(heroes);
                            character.ActivateOnTurnPassives(heroes, enemies);
                            if (character.pet != null) character.pet.PetSelection(character, heroes, enemies, PetProcType.PerTurn);
                            if (simOver) break;
                            character.ChooseSkill(heroes, enemies);
                            turnCount++;
                        }
                        else
                        {
                            character.IncSp(enemies);
                            if (character.pet != null) character.pet.PetSelection(character, enemies, heroes, PetProcType.PerTurn);
                            if (simOver) break;
                            character.ChooseSkill(enemies, heroes);
                        }
                        character.SubCount(trCounter);
                    }
                }
            }
            if (turnCount > safetyNet)
            {
                //UnityEngine.Debug.Log("turncount is " + turnCount.ToString() + " and safetyNet is " + safetyNet.ToString());
                return;
            }
        }
        simOutcome(heroesAlive);
        callback(0);
    }

}