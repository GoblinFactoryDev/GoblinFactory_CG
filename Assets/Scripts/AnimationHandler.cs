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
    /// public string hand_[what the animation is doing]_AnimeName = "The name of the animation in the animator";
    ///////////////////////////////////

    // Animation everyone use
    public string hand_Idle_AnimName = "Hand_Armature_Hands_DecidingIdle";
    public string hand_Inspect_AnimName = "Hand_Armature_Hands_Inspect";

    // Dragon Animations

    // Dwarf Animations

    #endregion

    // The string variables that store the names of the body animations. These should be set to match the names of the animations defined in the body animator.
    #region Body Animation Names
    ///////////////////////////////////
    /// THE FORMAT FOR THE STRING NAMES OF THE ANIMATIONS SHOULD BE AS FOLLOWS:
    /// public string body_[what the animation is doing]_AnimeName = "The name of the animation in the animator";
    ///////////////////////////////////
    // Animation everyone use
    public string bodyIdle_AnimName = "";

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
        bodyAnimator.Play(bodyIdle_AnimName);
    }

    /// <summary>
    /// Returns the hand animator to its idle state, stopping any currently playing hand animation.
    /// </summary>
    public void StopHandAnimation()
    {
        handAnimator.Play(hand_Idle_AnimName);
    }

    /// <summary>
    /// Returns both animators to their idle states, stopping any currently playing animations. 
    /// This is useful for resetting the character's pose after an action is completed or when transitioning between states.
    /// </summary>
    public void StopAllAnimations()
    {
        bodyAnimator.Play(bodyIdle_AnimName);
        handAnimator.Play(hand_Idle_AnimName);
    }

}
