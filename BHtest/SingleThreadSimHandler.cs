using System;
using System.Collections.Generic;
using System.Text;

class SingleThreadSimHandler
{
    private GameMode gameMode;
    private HeroPanel[] heroPanels;
    private int simulationDifficulty;
    private int playerNumber;
    private int bossType;

    private bool simRunning;
    private bool stopSimulation;

    private int simulationsToRun;
    private int simulationsCompleted;
    private int simulationsWon;
    private int sliderValue;

    private float winrateToShow;

    private Action<float> CallBackWinrate;
    private Action<int> CallBackSliderValue;
    private Action CallbackShowError;

    private Func<bool> GetCancelButtonState;

    public SingleThreadSimHandler(GameMode _gameMode, HeroPanel[] _heroPanels, int _bossType, int _simulationDifficulty, int _playerNumber, int _gamesToSimulate, Action<float> _callbackWinrate, Action<int> _callbackSliderValue, Action _callbackShowError, Func<bool> _getCancelButtonState)
    {
        gameMode = _gameMode;
        heroPanels = _heroPanels;
        simulationDifficulty = _simulationDifficulty;
        playerNumber = _playerNumber;
        bossType = _bossType;

        simRunning = false;
        stopSimulation = false;

        simulationsToRun = _gamesToSimulate;
        sliderValue = 0;
        simulationsWon = 0;
        simulationsCompleted = 0;
        winrateToShow = 0;

        CallBackWinrate = _callbackWinrate;
        CallBackSliderValue = _callbackSliderValue;
        CallbackShowError = _callbackShowError;
        GetCancelButtonState = _getCancelButtonState;
    }

}
