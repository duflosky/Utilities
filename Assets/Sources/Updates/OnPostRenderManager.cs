using System.Collections.Generic;

public class OnPostRenderManager : MonoLoopFunctionsManager<IOnPostRenderable>
{
    public override void UpdateElement(HashSet<IOnPostRenderable>.Enumerator e)
    {
        e.Current.OnPostRender();
    }

    private void OnPostRender()
    {
        LaunchLoop();
    }
}