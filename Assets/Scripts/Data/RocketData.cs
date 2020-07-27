using UnityEngine;

[CreateAssetMenu(fileName ="RocketData", menuName = "Weapons/RocketData")]
public class RocketData : ScriptableObject
{
    [SerializeField, Range(1, 20)]
    private float speed;
    public float Speed => speed;

    [SerializeField, Range(10, 80)]
    private float damage;
    public float Damage => damage;

    [SerializeField]
    private Rocket rocketPrefab;
    public Rocket RocketPrefab => rocketPrefab;

    [SerializeField, Range(1, 100)]
    private float cooldown;
    public float Cooldown => cooldown;
}
