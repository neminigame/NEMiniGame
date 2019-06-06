using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class playable_door : PlayableBehaviour
{
    public PortalFX polar;
    public Animator anim;
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (polar != null)
            polar.canStartAnim = true;
        if(anim!=null)
            anim.applyRootMotion = true;

    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {

            
    }
}
