using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    public Hand leftHand;
    public Hand rightHand;

    public Hand activeHand;

    public Transform anchorQ, anchorA, anchorE, anchorD;
    private Transform holdQ, holdA, holdE, holdD;


    public GameObject labelPrefab;
    private GameObject labelQ, labelA, labelE, labelD;

    private int handIndex;

    void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb.qKey.wasPressedThisFrame)
        {
            UseHand(leftHand, anchorQ);
        }

        if (kb.aKey.wasPressedThisFrame)
        {
            UseHand(leftHand, anchorA);
        }

        if (kb.eKey.wasPressedThisFrame)
        {
            UseHand(rightHand, anchorE);
        }

        if (kb.dKey.wasPressedThisFrame)
        {
            UseHand(rightHand, anchorD);
        }
    }

    void UseHand(Hand hand, Transform target)
    {
        if (activeHand == hand)
        {
            hand.SetTarget(target);
            SwitchHand();
        }

        
    }

   void SwitchHand()
    {
        if (activeHand == leftHand)
        {
            HighlightHand(leftHand.transform, Color.white);

            activeHand = rightHand;
            HighlightHand(rightHand.transform, Color.yellow);
        }
        else
        {
            HighlightHand(rightHand.transform, Color.white);

            activeHand = leftHand;
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
            if(!Keyboard.current.qKey.isPressed)
            {
                openHolds.Add('Q');
            }
            if (!Keyboard.current.aKey.isPressed)
            {
                openHolds.Add('A');
            }
            if (!Keyboard.current.eKey.isPressed)
            {
                openHolds.Add('E');
            }
            if (!Keyboard.current.dKey.isPressed)
            {
                openHolds.Add('D');
            }
            if (openHolds.Count == 0)
            {
                return;
            }
            char chosenHold = openHolds[handIndex % openHolds.Count];
            handIndex = (handIndex + 1) % 4;
            switch (chosenHold)
            {
                case 'Q':
                    holdQ = other.transform;
                    anchorQ.position = holdQ.position;
                    SetLabel(ref labelQ, holdQ, "Q");
                    break;

                case 'A':
                    holdA = other.transform;
                    anchorA.position = holdA.position;
                    SetLabel(ref labelA, holdA, "A");
                    break;

                case 'E':
                    holdE = other.transform;
                    anchorE.position = holdE.position;
                    SetLabel(ref labelE, holdE, "E");
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
        return hold == holdQ || hold == holdA || hold == holdE || hold == holdD;
    }   
}