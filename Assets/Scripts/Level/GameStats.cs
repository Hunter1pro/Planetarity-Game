using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour, IGameStatsSetup
{
    private IUpdateTimer updateTimer;

    private List<GameObject> enemies;

    private GameObject player;

    private IUIGameResults uiGameResults;

    private bool gameFinish;

    private bool gameStart;

    private void Start()
    {
        this.updateTimer = new UpdateTimer(3f, false);
    }

    public void GameStart(bool value)
    {
        this.gameStart = value;
    }

    void IGameStatsSetup.Enemies(List<GameObject> enemies)
    {
        this.enemies = enemies;
    }

    void IGameStatsSetup.Player(GameObject player)
    {
        this.player = player;
    }

    void IGameStatsSetup.UICallBack(IUIGameResults uiGameResults)
    {
        this.uiGameResults = uiGameResults;
    }

    private void Update()
    {
        if (!this.gameFinish && gameStart)
        {
            this.updateTimer.ExecuteUpdate(this.CheckGameStats);
        }
    }

    private void CheckGameStats()
    {
        if (this.player == null)
        {
            this.uiGameResults.PlayerWin(false);
            this.gameFinish = true;
        }

        bool enemiesKilled = true;

        this.enemies.ForEach(x =>
        {
            if (x)
                enemiesKilled = false;
        });

        if (enemiesKilled)
        {
            this.uiGameResults.PlayerWin(true);
            this.gameFinish = true;
        }

    }
}
