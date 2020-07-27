using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupWorlUI : MonoBehaviour, IHPSlider, ICooldownSlider, ISetName, ISetAimPoint
{
    [SerializeField]
    private TextMeshProUGUI name;

    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private TextMeshProUGUI hpText;

    [SerializeField]
    private Slider cooldown;
    [SerializeField]
    private TextMeshProUGUI cooldownText;

    [SerializeField]
    private Image aimPoint;

    private Transform player;

    private Camera camera;

    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        this.camera = Camera.main;
    }

    private void Update()
    {
        // If Player Alive
        if (this.player)
        {
            this.transform.position = this.camera.WorldToScreenPoint(this.player.position + this.offset);
        }
        else
        {
            // Destroy the HP
            MonoBehaviour.Destroy(this.gameObject);
        }
    }

    public void SetupTrackPos(Transform player)
    {
        this.player = player;
    }

    public void SetupAimColor(Color aimColor)
    {
        this.aimPoint.color = aimColor;
    }

    void IHPSlider.SetHPSlider(float value)
    {
        this.hpSlider.value = value;
        this.hpText.text = $"HP: {value.ToString("F2")}";
    }

    void ICooldownSlider.SetCooldownSlider(float value)
    {
        this.cooldown.value = value;
        this.cooldownText.text = $"Cooldown: {value.ToString("F2")}";
    }

    void ISetName.SetName(string value)
    {
        this.name.text = value;
    }

    void ISetAimPoint.SetPosition(Vector3 value)
    {
        this.aimPoint.transform.position = value;
    }
}
