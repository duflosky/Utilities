using Cysharp.Threading.Tasks;
using UnityEngine;

public class TickManager
{
    private float tickRate;
    private float tickTimer;
    private static float tickTime;
    public event System.Action TickEvent;

    public TickManager(float tickRate)
    {
        this.tickRate = tickRate;
        tickTime = 1 / tickRate;
        tickTimer = 0;
        Tick();
    }

    public float GetTickTime
    {
        get => tickTime;
    }

    public async void Tick()
    {
        while (true)
        {
            if (tickTimer >= tickTime)
            {
                tickTimer -= tickTime;
                TickEvent?.Invoke();
            }
            else tickTimer += Time.deltaTime;

            await UniTask.DelayFrame(0);
        }
    }
}