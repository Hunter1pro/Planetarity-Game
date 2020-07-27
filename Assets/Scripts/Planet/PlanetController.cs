using System;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour, IInputActions, IPlanetSpawnData, ISelectRocket
{
    private InputData inputData;

    private List<RocketData> rocketDatas;

    private ICooldownSlider cooldownSlider;
    private IUISubscribe uiSubscribe;
    private ISetAimPoint setAimPoint;

    private Dictionary<int, ITakeDamage> planetsDamage;

    private float spawnOffset = 1.5f;

    private RocketData selectedRocket;

    /// <summary>
    /// TODO: To Global Config
    /// </summary>
    private float cooldown;
    private float lastCooldown;

    private float maxCooldown = 100;
    private float cooldownSpeed = 5;

    private void Start()
    {
        cooldown = maxCooldown;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, inputData.mousePos, Color.red);

        if (this.inputData.fire)
        {
            this.inputData.fire = false;

            this.SpawnRocket(ref selectedRocket);
        }

        this.cooldown = Mathf.Clamp(this.cooldown + (this.cooldownSpeed * Time.deltaTime), 0, this.maxCooldown);

        if (this.cooldown != this.lastCooldown)
        {
            this.lastCooldown = this.cooldown;

            this.cooldownSlider.SetCooldownSlider(this.lastCooldown);
        }
    }

    private void SpawnRocket(ref RocketData rocketData)
    {
        if (rocketData == null)
        {
            print("Select Rocket Type");
            return;
        }

        if (cooldown >= rocketData.Cooldown)
        {
            cooldown = Mathf.Clamp(cooldown - rocketData.Cooldown, 0, maxCooldown);
        }
        else
        {
            // Wait for cooldown
            return;
        }    

        Rocket rocket = Instantiate(rocketData.RocketPrefab, this.CalcOffset(this.transform.position), Quaternion.identity);

        ((ISetupRocket)rocket).RocketData(rocketData);
        ((ISetupRocket)rocket).PlanetsDamage(planetsDamage);

        this.ShotRocket(rocket.Rigidbody, ref rocketData);
    }

    private void ShotRocket(Rigidbody rocket, ref RocketData rocketData)
    {
        Vector3 dirrection = this.inputData.mousePos - this.transform.position;

        float angle = Mathf.Atan2(dirrection.x, dirrection.z) * Mathf.Rad2Deg;

        rocket.transform.eulerAngles = Vector3.up * angle;

        rocket.AddForce(dirrection.normalized * rocketData.Speed, ForceMode.Impulse);
    }

    #region Input

    void IInputActions.Fire(bool value)
    {
        this.inputData.fire = value;
    }

    void IInputActions.SelectTarget(Vector3 pos, Vector3 screenPos)
    {
        this.inputData.mousePos = pos;
        this.setAimPoint.SetPosition(screenPos);
    }

    #endregion

    #region SpawnData

    void IPlanetSpawnData.RocketData(List<RocketData> rocketDatas)
    {
        this.rocketDatas = rocketDatas;
    }

    void IPlanetSpawnData.RocketSubsribeActions(IUISubscribe uiSubscribe)
    {
        this.uiSubscribe = uiSubscribe;
        this.uiSubscribe.Rocket1Click(() => (this as ISelectRocket).SelectRocket(0));
        this.uiSubscribe.Rocket2Click(() => (this as ISelectRocket).SelectRocket(1));
        this.uiSubscribe.Rocket3Click(() => (this as ISelectRocket).SelectRocket(2));
        this.uiSubscribe.FireClick(() => ((IInputActions)this).Fire(true));
    }

    void IPlanetSpawnData.PlanetsDamage(Dictionary<int, ITakeDamage> planetsDamage)
    {
        this.planetsDamage = planetsDamage;
    }

    void IPlanetSpawnData.CooldownSlider(ICooldownSlider cooldownSlider)
    {
        this.cooldownSlider = cooldownSlider;
    }

    #endregion

    void ISelectRocket.SelectRocket(int index)
    {
        if (this.rocketDatas.Count > index)
        {
            selectedRocket = this.rocketDatas[index];
        }
        else
        {
            print($"RocketData {index} Dont Setuped");
        }
    }

    private Vector3 CalcOffset(Vector3 position)
    {
        Vector3 dirrection = this.inputData.mousePos - this.transform.position;

        return position + (dirrection.normalized * this.spawnOffset);
    }

    public void AimPoint(ISetAimPoint aimPoint)
    {
        this.setAimPoint = aimPoint;
    }
}
