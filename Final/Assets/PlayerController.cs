using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public enum HandType { Left, Right }
    public HandType activeHand = HandType.Left;

    public Hand leftHand;
    public Hand rightHand;

    public Transform anchorS, anchorD, anchorA, anchorW;
    private Transform holdW, holdA, holdS, holdD;


    public GameObject labelPrefab;
    private GameObject labelW, labelA, labelS, labelD;

    private int handIndex;

    void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb.sKey.wasPressedThisFrame)
        {
            UseRightHand(anchorS);
        }

        if (kb.dKey.wasPressedThisFrame)
        {
            UseRightHand(anchorD);
        }

        if (kb.aKey.wasPressedThisFrame)
        {
            UseLeftHand(anchorA);
        }

        if (kb.wKey.wasPressedThisFrame)
        {
            UseLeftHand(anchorW);
        }
    }

    void UseLeftHand(Transform target)
    {
        if (activeHand == HandType.Left)
        {
            leftHand.SetTarget(target);
            SwitchHand();
        }

        
    }

    void UseRightHand(Transform target)
    {
        if (activeHand == HandType.Right)
        {
            rightHand.SetTarget(target);
            SwitchHand();
        }
    }

   void SwitchHand()
    {
        if (activeHand == HandType.Left)
        {
            HighlightHand(leftHand.transform, Color.white);

            activeHand = HandType.Right;
            HighlightHand(rightHand.transform, Color.yellow);
        }
        else
        {
            HighlightHand(rightHand.transform, Color.white);

            activeHand = HandType.Left;
            HighlightHand(leftHand.transform, Color.yellow);
        }
    }

    void HighlightHand(Transform hand, Color color)
    {
        Renderer r = hand.GetComponent<Renderer>();

        if (r != null)
        {
            r.material.color = color;
        }
    }

    void SetLabel(ref GameObject label, Transform hold, string text)
    {
        if (label == null)
        {
            label = Instantiate(labelPrefab);
        }

        label.transform.position = hold.position + Vector3.up * 0.5f;

        var tmp = label.GetComponent<TMPro.TextMeshPro>();
        tmp.text = text;
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsHoldAlreadyUsed(other.transform)){
            return;
        }

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
                    holdW = other.transform;
                    anchorW.position = holdW.position;
                    SetLabel(ref labelW, holdW, "W");
                    break;

                case 'A':
                    holdA = other.transform;
                    anchorA.position = holdA.position;
                    SetLabel(ref labelA, holdA, "A");
                    break;

                case 'S':
                    holdS = other.transform;
                    anchorS.position = holdS.position;
                    SetLabel(ref labelS, holdS, "S");
                    break;

                case 'D':
                    holdD = other.transform;
                    anchorD.position = holdD.position;
                    SetLabel(ref labelD, holdD, "D");
                    break;
            }
        }
    }

    bool IsHoldAlreadyUsed(Transform hold)
    {
        return hold == holdW || hold == holdA || hold == holdS || hold == holdD;
    }   
}