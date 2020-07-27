using System;
using UnityEngine;

public class UpdateTimer : IUpdateTimer
{
    private float timer;
    private float time;
    private bool once;
    private bool oneTime;

    public UpdateTimer (float time, bool once)
    {
        this.time = time;
        this.once = once;
    }

    public bool ExecuteUpdate ()
    {
        bool returnValue = false;

        timer += Time.deltaTime;

        if (!once)
        {
            if (timer >= time)
            {
                returnValue = true;

                Reset();
            }
        }
        else 
        {
            if (timer >= time && !oneTime)
            {
                returnValue = true;
                oneTime = true;
            }
            else
            {
                returnValue = false;
            }
        }

        return returnValue;
    }

    public void ExecuteUpdate(Action action)
    {
        if (ExecuteUpdate())
            action();
    }

    public void Reset()
    {
        timer = 0;
        oneTime = false;
    }
}

public interface IUpdateTimer
{
    bool ExecuteUpdate();

    void ExecuteUpdate(Action action);

    void Reset();
}
