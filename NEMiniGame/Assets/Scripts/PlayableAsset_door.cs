using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[System.Serializable]
public class PlayableAsset_door : PlayableAsset
{
    public ExposedReference<PortalFX> polar;
    
    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<playable_door>.Create(graph);
        playable.GetBehaviour().polar = polar.Resolve(graph.GetResolver());

        
        return playable;
    }
   
}