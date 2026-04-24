using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    private int handIndex;

    void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb.sKey.wasPressedThisFrame)
        {
            UseHand(anchorS);
        }

        if (kb.dKey.wasPressedThisFrame)
        {
            UseHand(anchorD);
        }

        if (kb.aKey.wasPressedThisFrame)
        {
            UseHand(anchorA);
        }

        if (kb.wKey.wasPressedThisFrame)
        {
            UseHand(anchorW);
        }
    }

    void UseHand(Transform target)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PotentialHold"))
        {
            List<char> openHolds = new List<char>();
            if(!Keyboard.current.sKey.isPressed)
            {
                openHolds.Add('S');
            }
            if (!Keyboard.current.aKey.isPressed)
            {
                openHolds.Add('A');
            }
            if (!Keyboard.current.dKey.isPressed)
            {
                openHolds.Add('D');
            }
            if (!Keyboard.current.wKey.isPressed)
            {
                openHolds.Add('W');
            }
            if (openHolds.Count == 0)
            {
                return;
            }
            char chosenHold = openHolds[handIndex % openHolds.Count];
            handIndex = (handIndex + 1) % 4;
            switch (chosenHold)
            {
                case 'W':
                    anchorW.position = other.transform.position;
                    break;
                case 'D':
                    anchorD.position = other.transform.position;
                    break;
                case 'S':
                    anchorS.position = other.transform.position;
                    break;
                case 'A':
                    anchorA.position = other.transform.position;
                    break;
            }
        }
    }
}