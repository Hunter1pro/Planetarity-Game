using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;

    private Transform player;
    private Camera cameraMain;

    [SerializeField]
    private Slider slider;

    public void SetupPlayer(Transform player)
    {
        this.player = player;
    }

    private void Start()
    {
        this.cameraMain = Camera.main;

        this.slider.onValueChanged.AddListener(this.Scale);
    }

    private void Scale(float arg0)
    {
        this.offset.y = arg0;
    }

    private void Update()
    {
        if (this.player)
            this.cameraMain.transform.position = this.player.position + offset;
    }
}
