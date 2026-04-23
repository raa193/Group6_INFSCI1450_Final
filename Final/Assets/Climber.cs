using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum HandType { Left, Right }
    public HandType activeHand = HandType.Left;

    public Hand leftHand;
    public Hand rightHand;

    public Transform anchorS;
    public Transform anchorD;
    public Transform anchorA;
    public Transform anchorW;

    void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb.sKey.wasPressedThisFrame)
        {
            UseHand(anchorS.position);
        }

        if (kb.dKey.wasPressedThisFrame)
        {
            UseHand(anchorD.position);
        }

        if (kb.aKey.wasPressedThisFrame)
        {
            UseHand(anchorA.position);
        }

        if (kb.wKey.wasPressedThisFrame)
        {
            UseHand(anchorW.position);
        }
    }

    void UseHand(Vector3 target)
    {
        if (activeHand == HandType.Left)
        {
            leftHand.SetTarget(target);
        }
        else
        {
            rightHand.SetTarget(target);
        }

        SwitchHand();
    }

    void SwitchHand()
    {
        activeHand = (activeHand == HandType.Left)
            ? HandType.Right
            : HandType.Left;
    }
}