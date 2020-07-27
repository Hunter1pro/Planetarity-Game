
using System.Collections.Generic;
using UnityEngine;

public interface IGameStatsSetup
{
    void UICallBack(IUIGameResults uiGameResults);

    void Player(GameObject palyer);

    void Enemies(List<GameObject> enemies);
}
