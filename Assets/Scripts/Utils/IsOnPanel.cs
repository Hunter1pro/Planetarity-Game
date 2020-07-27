using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class IsOnPanel
{
    public static bool Check()
    {
        bool isOnPanel = false;

        // Check Mouse
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isOnPanel = true;
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    isOnPanel = true;
                }
            }
        }

        return isOnPanel;
}
}
