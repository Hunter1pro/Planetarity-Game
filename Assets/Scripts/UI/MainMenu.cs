using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button startBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(StartBtnClick);
    }

    private void StartBtnClick()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
