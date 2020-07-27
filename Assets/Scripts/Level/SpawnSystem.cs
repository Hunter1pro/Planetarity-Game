using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField]
    private List<PlayerData> planets;

    [SerializeField]
    private List<Transform> panetsSpawn;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject planetPrefab;

    [SerializeField]
    private GameObject justPlanetPrefab;

    [SerializeField]
    private PlanetController playerPrefab;

    [SerializeField]
    private PlanetController enemyPrefab;

    [SerializeField, Header("Systems")]
    private PlayerFollow playerFollow;

    [SerializeField]
    private UISystem uiSystem;

    [SerializeField]
    private GameStats gameStats;

    private List<IPlanetSpawnData> playerSpawnData = new List<IPlanetSpawnData>();

    private Dictionary<int, ITakeDamage> damagePlanets = new Dictionary<int, ITakeDamage>();

    private int playerSpawnID;

    private void Start()
    {
        uiSystem.SubscribePlay(StartPlayers);
    }

    public void StartPlayers(int value)
    {
        if (value > 4 || value < 2)
        {
            print($"You value is {value} minimal from 2 to 4");
            value = Mathf.Clamp(value, 2, 4);
        }

        planets.RemoveRange(value -1, planets.Count - value);

        this.playerSpawnID = UnityEngine.Random.Range(0, this.planets.Count);

        int counter = 0;

        int[] randomIndexes = this.GetRandomRange(panetsSpawn.Count);

        // Spawn and Setup planets for players

        List<GameObject> enemies = new List<GameObject>();

        this.planets.ForEach(x =>
        {
            Transform randomSpawnPoint = panetsSpawn[randomIndexes[counter]];

            GameObject planetInst = Instantiate(this.planetPrefab, randomSpawnPoint.position, Quaternion.identity, randomSpawnPoint);

            planetInst.GetComponent<MeshRenderer>().material = x.PlanetMaterial;

            PlanetController player;
            SetupWorlUI worldUI = this.uiSystem.SpawnWorldHP();

            if (this.playerSpawnID == counter)
            {
                player = Instantiate(this.playerPrefab, planetInst.transform.position, Quaternion.identity, planetInst.transform);
                this.playerFollow.SetupPlayer(planetInst.transform);

                ((IPlanetSpawnData)player).RocketSubsribeActions(this.uiSystem);
                ((ISetName)worldUI).SetName("Player");

                (this.gameStats as IGameStatsSetup).Player(player.gameObject);
            }
            else
            {
                player = Instantiate(this.enemyPrefab, planetInst.transform.position, Quaternion.identity, planetInst.transform);
                ((ISetName)worldUI).SetName("Enemy");
                enemies.Add(player.gameObject);
            }

            ((IPlanetSpawnData)player).RocketData(x.RocketData);

            ((IPlanetSpawnData)player).AimPoint(worldUI);

            this.playerSpawnData.Add(player);

            this.damagePlanets.Add(planetInst.GetInstanceID(), planetInst.GetComponent<ITakeDamage>());

            planetInst.GetComponent<IHPSpawnData>().SetupHPSlider(worldUI);
            ((IPlanetSpawnData)player).CooldownSlider(worldUI);

            worldUI.SetupTrackPos(planetInst.transform);
            worldUI.SetupAimColor(x.PlanetMaterial.color);

            counter++;
        });

        this.playerSpawnData.ForEach(x => x.PlanetsDamage(this.damagePlanets));

        (this.gameStats as IGameStatsSetup).Enemies(enemies);

        (this.gameStats as IGameStatsSetup).UICallBack(uiSystem);

        // Spawn Just Planets
        while (counter < panetsSpawn.Count)
        {
            Instantiate(justPlanetPrefab, panetsSpawn[randomIndexes[counter]]);
            counter++;
        }

        gameStats.GameStart(true);
    }

    private int[] GetRandomRange(int count)
    {
        int[] planetsIndexs = new int[count];

        for (int i = 0; i < planetsIndexs.Length; i++)
        {
            planetsIndexs[i] = i;
        }

        return planetsIndexs.Shuffle().Take(count).ToArray();
    }
}
