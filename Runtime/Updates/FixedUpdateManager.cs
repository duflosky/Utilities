using System.Collections.Generic;

public class FixedUpdateManager : MonoLoopFunctionsManager<IFixedUpdate>
{
    public override void UpdateElement(HashSet<IFixedUpdate>.Enumerator e)
    {
        e.Current.OnFixedUpdate();
    }

    private void FixedUpdate()
    {
        LaunchLoop();
    }
}