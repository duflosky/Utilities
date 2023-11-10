using System;

public class TickTimer
{
    public event Action InitiateEvent;
    public event Action TickEvent;
    public event Action CancelEvent;
    public event Action UpdateEvent;
    public float Time;
    private float timer;
    private TickManager tickManager;

    public TickTimer(float time, TickManager tickManager)
    {
        Time = time;
        this.tickManager = tickManager;
    }

    public void Initiate()
    {
        InitiateEvent?.Invoke();
        tickManager.TickEvent += Tick;
        timer = Time;
    }

    private void Tick()
    {
        timer -= tickManager.GetTickTime;
        UpdateEvent?.Invoke();
        if (timer <= 0)
        {
            tickManager.TickEvent -= Tick;
            TickEvent?.Invoke();
        }
    }

    public void Cancel()
    {
        tickManager.TickEvent -= Tick;
        CancelEvent?.Invoke();
    }

    public void ResetCancelEvent()
    {
        CancelEvent = null;
    }

    public void ResetTickEvent()
    {
        TickEvent = null;
    }

    public void ResetInitiateEvent()
    {
        InitiateEvent = null;
    }

    public void ResetEvents()
    {
        ResetInitiateEvent();
        ResetCancelEvent();
        ResetTickEvent();
    }
}