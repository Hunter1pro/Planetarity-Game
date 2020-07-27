using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private Material planetMaterial;
    public Material PlanetMaterial => planetMaterial;

    [SerializeField]
    private List<RocketData> rocketDatas;
    public List<RocketData> RocketData => rocketDatas;
}
