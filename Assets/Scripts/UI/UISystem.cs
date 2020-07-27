using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class RocketBtn
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Color selectedColor;

    private Color originalColor;

    public void Init()
    {
        this.originalColor = button.image.color;
    }
    
    public void Subcribe(UnityAction unityAction)
    {
        this.button.onClick.AddListener(unityAction);
    }

    public void SetChouseColor()
    {
        this.button.image.color = selectedColor;
    }

    public void ResetColor()
    {
        this.button.image.color = originalColor;
    }

}

public class UISystem : MonoBehaviour, IUISubscribe, IUIGameResults
{
    [SerializeField]
    private List<RocketBtn> rocketBtns;
    private RocketBtn lastRocketBtn;

    [SerializeField]
    private Button fireBtn;

    [SerializeField]
    private Transform slidersSpawnPoint;

    [SerializeField]
    private SetupWorlUI sliderPrefab;

    [Header("Start Panel")]
    [SerializeField]
    private GameObject startPanel;

    [SerializeField]
    private TMP_Dropdown dropDown;

    [SerializeField]
    private Button playBtn;

    [Header("Pause Panel")]
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private Button pauseBtn;

    [SerializeField]
    private Button resumeBtn;

    [SerializeField]
    private Button restartBtn;

    [Header("Win Panel")]
    [SerializeField]
    private GameObject WinPanel;

    [SerializeField]
    private Button restartWinBtn;

    [Header("Lost Panel")]
    [SerializeField]
    private GameObject LostPanel;

    [SerializeField]
    private Button restartLostBtn;

    [SerializeField]
    private GameObject selectWeaponText;

    private int playersCount;

    private Action<int> startPlayers;

    private void Start()
    {
        this.rocketBtns.ForEach(x => x.Init());

        this.dropDown.onValueChanged.AddListener(this.SelectPlayersCount);

        this.playBtn.onClick.AddListener(this.PlayBtnClick);

        this.pauseBtn.onClick.AddListener(this.PauseBtnClick);
        this.resumeBtn.onClick.AddListener(this.ResumeBtnClick);
        this.restartBtn.onClick.AddListener(this.RestartBtnClick);

        this.restartWinBtn.onClick.AddListener(this.RestartBtnClick);
        this.restartLostBtn.onClick.AddListener(this.RestartBtnClick);

        // Default 2 Players
        this.SelectPlayersCount(0);
    }

    private void RestartBtnClick()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResumeBtnClick()
    {
        this.pauseBtn.gameObject.SetActive(true);
        this.pausePanel.SetActive(false);

        Time.timeScale = 1;
    }

    private void PauseBtnClick()
    {
        this.pauseBtn.gameObject.SetActive(false);
        this.pausePanel.SetActive(true);

        Time.timeScale = 0;
    }

    private void PlayBtnClick()
    {
        startPanel.SetActive(false);
        startPlayers?.Invoke(playersCount);

        Time.timeScale = 1;
    }

    private void SelectPlayersCount(int arg0)
    {
        playersCount = arg0 + 2;
    }

    public void SubscribePlay(Action<int> startPlayers)
    {
        this.startPlayers = startPlayers;
    }

    void IUISubscribe.Rocket1Click(UnityAction rocket1A)
    {
        this.rocketBtns[0].Subcribe(() =>
        {
            rocket1A?.Invoke();

            this.SelectChousenColor(0);
        });
    }

    void IUISubscribe.Rocket2Click(UnityAction rocket2A)
    {
        this.rocketBtns[1].Subcribe(() =>
        {
            rocket2A?.Invoke();

            this.SelectChousenColor(1);
        });
    }

    void IUISubscribe.Rocket3Click(UnityAction rocket3A)
    {
        this.rocketBtns[2].Subcribe(() =>
        {
            rocket3A?.Invoke();

            this.SelectChousenColor(2);
        });
    }

    void IUISubscribe.FireClick(UnityAction fireClick)
    {
        this.fireBtn.onClick.AddListener(fireClick);
    }

    private void SelectChousenColor(int index)
    {
        if (this.lastRocketBtn != null)
            this.lastRocketBtn.ResetColor();

        this.rocketBtns[index].SetChouseColor();

        this.lastRocketBtn = this.rocketBtns[index];

        this.OnceSelectText();
    }

    public SetupWorlUI SpawnWorldHP()
    {
        return Instantiate(sliderPrefab, slidersSpawnPoint);
    }

    void IUIGameResults.PlayerWin(bool value)
    {
        if (value)
        {
            this.WinPanel.SetActive(true);
        }
        else
        {
            this.LostPanel.SetActive(true);
        }
    }

    private void OnceSelectText()
    {
        if (selectWeaponText.activeSelf)
            selectWeaponText.SetActive(false);
    }
}
