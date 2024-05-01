using System.Collections.Generic;

public class UpdateManager : MonoLoopFunctionsManager<IUpdatable>
{
    public override void UpdateElement(HashSet<IUpdatable>.Enumerator e)
    {
        e.Current.OnUpdate();
    }

    public void Update()
    {
        LaunchLoop();
    }
}