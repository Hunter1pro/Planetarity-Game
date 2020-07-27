using UnityEngine;
using UnityEngine.Events;

public interface IUISubscribe
{
    void Rocket1Click(UnityAction rockt1A);

    void Rocket2Click(UnityAction rocket2A);

    void Rocket3Click(UnityAction rocket3A);

    void FireClick(UnityAction fireClick);
}
