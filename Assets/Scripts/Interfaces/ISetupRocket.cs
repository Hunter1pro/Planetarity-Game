using System.Collections.Generic;
using UnityEngine;

public interface ISetupRocket
{
    void RocketData(RocketData rocketData);

    void PlanetsDamage(Dictionary<int, ITakeDamage> planetsDamage);
}
