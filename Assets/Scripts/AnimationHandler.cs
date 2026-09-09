using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField]
    Animator bodyAnimator;
    [SerializeField]
    Animator handAnimator;

    // The string variables that store the names of the hand animations. These should be set to match the names of the animations defined in the hand animator.
    #region Hand Animation Names
    ///////////////////////////////////
    /// THE FORMAT FOR THE STRING NAMES OF THE ANIMATIONS SHOULD BE AS FOLLOWS:
    /// public string hand_[what the animation is doing]_Who it is for;
    ///////////////////////////////////
    [Header("Hand Animations")]
    // Animation everyone use
    [Header("Everyones Hand Animations")]
    [SerializeField] public string hand_Inspect_All;
    [SerializeField] public string hand_SelfInspect_All;
    [SerializeField] public string hand_ReadyUp_All;
    [SerializeField] public string[] hand_Casting_All;
    [SerializeField] public string[] hand_Hurt_All;

    // Dragon Animations
    [Header("Dragon Hand Animations")]
    [SerializeField] public string hand_Idle_Dragon;

    // Dwarf Animations
    [Header("Dwarf Hand Animations")]
    [SerializeField] public string hand_Idle_Dwarf;

    #endregion

    // The string variables that store the names of the body animations. These should be set to match the names of the animations defined in the body animator.
    #region Body Animation Names
    ///////////////////////////////////
    /// THE FORMAT FOR THE STRING NAMES OF THE ANIMATIONS SHOULD BE AS FOLLOWS:
    /// public string hand_[what the animation is doing]_Who it is for;
    //////////////////////////////////////
    // Animation everyone use
    private string body_Idle_All;

    // Dragon Animations

    // Dwarf Animations
    #endregion


    /// <summary>
    /// Plays the specified body animation using the body animator.
    /// </summary>
    /// <param name="animationName">The name of the animation to play. This must match the name of an animation defined in the body animator.</param>
    public void PlayBodyAnimation(string animationName)
    {
        bodyAnimator.Play(animationName);
    }

    /// <summary>
    /// Plays the specified hand animation using the hand animator.
    /// </summary>
    /// <param name="animationName">The name of the animation to play. This must match the name of an animation defined in the hand animator.</param>
    public void PlayHandAnimation(string animationName)
    {
        handAnimator.Play(animationName);
    }

    /// <summary>
    /// Lets both animators play an animation at the same time
    /// </summary>
    /// <param name="bodyAnimationName"> The name of the body animation being played </param>
    /// <param name="handAnimationName"> The name of the hand animation being played </param>
    public void PlayBothAnimations(string bodyAnimationName, string handAnimationName)
    {
        PlayBodyAnimation(bodyAnimationName);
        PlayHandAnimation(handAnimationName);
    }

    /// <summary>
    /// Returns the body animator to its idle state, stopping any currently playing hand animation.
    /// </summary>
    public void StopBodyAnimation()
    {
        bodyAnimator.Play(body_Idle_All);
    }

    /// <summary>
    /// Returns the hand animator to its idle state, stopping any currently playing hand animation.
    /// </summary>
    public void StopHandAnimation()
    {
        //handAnimator.Play(hand_Idle_AnimName);
    }

    /// <summary>
    /// Returns both animators to their idle states, stopping any currently playing animations. 
    /// This is useful for resetting the character's pose after an action is completed or when transitioning between states.
    /// </summary>
    public void StopAllAnimations()
    {
       // bodyAnimator.Play(bodyIdle_AnimName);
       // handAnimator.Play(hand_Idle_AnimName);
    }

}
