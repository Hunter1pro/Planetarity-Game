using System.Collections.Generic;

public interface IPlanetSpawnData
{
    void RocketData(List<RocketData> rocketDatas);

    void RocketSubsribeActions(IUISubscribe uiSubscribe);

    void CooldownSlider(ICooldownSlider cooldownSlider);

    void AimPoint(ISetAimPoint aimPoint);

    void PlanetsDamage(Dictionary<int, ITakeDamage> planetsDamage);
}
