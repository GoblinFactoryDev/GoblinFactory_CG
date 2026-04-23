using UnityEngine;

public class FingerSelect : MonoBehaviour
{
    private int segmentTotal = 3;

    public void ChangeAllSegments(Player playerRef, Vector4 selectColor, HandType hand, FingerType finger)
    {
        for (int i = 0; i < segmentTotal; i++)
        {
            playerRef.hands[(int)hand].BoneFingers[(int)finger].boneSegments[i].GetComponent<Renderer>().material.SetVector("_OutlineColour", selectColor);
            if(finger == FingerType.Thumb && i == 1)
            {
                break;
            }
        }
    }
}
