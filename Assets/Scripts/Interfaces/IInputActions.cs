using UnityEngine;

public interface IInputActions
{
    void SelectTarget(Vector3 pos, Vector3 screenPos);

    void Fire(bool value);
}
