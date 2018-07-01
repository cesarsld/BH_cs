using System;
using System.Threading;
using System.Threading.Tasks;

public enum GameMode
{
    None,
    Raid,
    WB
}

public class MultiThreadSimHandler
{
    public static readonly object SyncObj = new object();

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

    public MultiThreadSimHandler(GameMode _gameMode, HeroPanel[] _heroPanels, int _bossType, int _simulationDifficulty, int _playerNumber, int _gamesToSimulate, Action<float> _callbackWinrate, Action<int> _callbackSliderValue, Action _callbackShowError, Func<bool> _getCancelButtonState)
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

    public void LaunchSingleThreadSim()
    {
        simRunning = true;
        for (int i = 0; i < simulationsToRun; i++)
        {
            if (!simRunning)
            {
                break;
            }
            switch (gameMode)
            {
                case GameMode.Raid:
                    new RaidSimulation(simulationDifficulty, playerNumber, heroPanels, i).Run(bossType, Callback, InvokeStopSim, SimulationOutcome);
                    break;
                default:
                    new WorldBossSimulation(simulationDifficulty, playerNumber, heroPanels, i).Run(bossType, Callback, InvokeStopSim, SimulationOutcome);
                    break;
            }
        }
        CallBackWinrate(winrateToShow);
        CallBackSliderValue(sliderValue);
        Console.WriteLine("SingleThread with new sim every loop result: ");
        Console.WriteLine("winrate is " + winrateToShow);
        Console.WriteLine("Progess bar : " + sliderValue + "%");
        Console.WriteLine("Simulation ended.");
    }

    public void LaunchSimulation()
    {

        simRunning = true;
        Parallel.For(0, simulationsToRun, new ParallelOptions { MaxDegreeOfParallelism = 1}, (x, state) =>
        {
            if (!simRunning)
            {
                state.Break();
            }
            switch (gameMode)
            {
                case GameMode.Raid:
                    new RaidSimulation(simulationDifficulty, playerNumber, heroPanels, x).Run(bossType, Callback, InvokeStopSim, SimulationOutcome);
                    break;
                default:
                    new WorldBossSimulation(simulationDifficulty, playerNumber, heroPanels, x).Run(bossType, Callback, InvokeStopSim, SimulationOutcome);
                    break;
            }
        });

        CallBackWinrate(winrateToShow);
        CallBackSliderValue(sliderValue);
        Console.WriteLine("MultiThread results:");
        Console.WriteLine("winrate is " + winrateToShow);
        Console.WriteLine("Progess bar : " + sliderValue + "%");
        Console.WriteLine("Simulation ended.");
    }
    public void LaunchSimulationOld()
    {

        ThreadPool.SetMaxThreads(1, 0);
        Console.WriteLine("max threads is {0}", ggggg);
        simRunning = true;
        for (int i = 0; i < simulationsToRun; i++)
        {
            Simulation simulation;
            switch (gameMode)
            {
                case GameMode.Raid:
                    simulation = new RaidSimulation(simulationDifficulty, playerNumber, heroPanels, i);
                    break;
                default:
                    simulation = new WorldBossSimulation(simulationDifficulty, playerNumber, heroPanels, i);
                    break;
            }
            ThreadPool.QueueUserWorkItem(state => simulation.Run(bossType, Callback, InvokeStopSim, SimulationOutcome));
     
        }

        while (GetActiveThreadCount() > 0 && simRunning)
        {
            try
            {
                //Console.Clear();
                Thread.Sleep(100);
                Console.WriteLine("winrate is " + winrateToShow);
                CallBackWinrate(winrateToShow);
                Console.WriteLine("Progess bar : " + sliderValue + "%");
                CallBackSliderValue(sliderValue);
                if (stopSimulation || GetCancelButtonState())
                {
                    simRunning = false;
                    CallbackShowError();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        CallBackWinrate(winrateToShow);
        CallBackSliderValue(sliderValue);
        //Console.Clear();
        Console.WriteLine("winrate is " + winrateToShow);
        Console.WriteLine("Progess bar : " + sliderValue + "%");
        Console.WriteLine("Simulation ended.");
    }
    public void Callback(float value)
    {
        //fill out later, not very important
    }
    public void SimulationOutcomeOld(bool win)
    {
        lock (SyncObj)
        {
            //Console.WriteLine("Call number [{0}] || Fight won = {1}", simulationsCompleted, win);
            simulationsCompleted++;
            if (win) simulationsWon++;
            winrateToShow = (float)simulationsWon * 100 / (float)simulationsCompleted;
            if (simulationsCompleted * 100 / simulationsToRun >= sliderValue + 1 && sliderValue < 100) sliderValue = Convert.ToInt32(simulationsCompleted * 100 / simulationsToRun);
            if (simulationsCompleted >= simulationsToRun) simRunning = false;
        }
    }
    public void SimulationOutcome(bool win)
    {
        lock (SyncObj)
        {
            //Console.WriteLine("Call number [{0}] || Fight won = {1}", simulationsCompleted, win);
            simulationsCompleted++;
            if (win) simulationsWon++;
            winrateToShow = (float)simulationsWon * 100 / (float)simulationsCompleted;
            if (simulationsCompleted * 100 / simulationsToRun >= sliderValue + 1 && sliderValue < 100)
            {
                sliderValue = Convert.ToInt32(simulationsCompleted * 100 / simulationsToRun);
                //Console.WriteLine("Progess bar : " + sliderValue + "%");
            }
            if (simulationsCompleted >= simulationsToRun) simRunning = false;
        }
    }
    private bool InvokeStopSim(bool callFromSim)
    {
        if (stopSimulation || callFromSim)
        {
            stopSimulation = true;
            return true;
        }
        return false;
    }
    private int GetActiveThreadCount()
    {
        //int availableWorkers = 0;
        //int maxWorkers = 0;
        //int availableIo = 0;
        //int maxIo = 0;
        ThreadPool.GetAvailableThreads(out int availableWorkers, out int availableIo);
        ThreadPool.GetMaxThreads(out int maxWorkers, out int maxIo);
        return maxWorkers - availableWorkers;
    }
}
