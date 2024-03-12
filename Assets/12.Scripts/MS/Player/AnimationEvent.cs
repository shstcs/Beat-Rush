using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void FirstFootStep()
    {
        Managers.Sound.PlaySFX(SFX.FirstWalk);
    }

    public void SecondFootStep()
    {
        Managers.Sound.PlaySFX(SFX.SecondWalk);
    }
}
