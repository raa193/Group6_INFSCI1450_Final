using UnityEngine;
using UnityEngine.InputSystem;

public class RightHand : MonoBehaviour
{

    private float holdTime = 0f;
    public float timeToLock = 0.5f;

    private bool isLocked = false;

    public Vector3 goalPosition;
    
    public Transform anchorS, anchorD;

    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, goalPosition, 0.1f);
    }

    private void Update()
    {
        Keyboard kb = Keyboard.current;

        if (isLocked)
        {
            // stay locked until explicitly changed later
            return;
        }

        if (kb.sKey.isPressed || kb.dKey.isPressed)
        {
            holdTime += Time.deltaTime;

            if (holdTime >= timeToLock)
            {
                LockPosition();
            }
        }
        else
        {
            holdTime = 0f;
            goalPosition = initialPosition;
        }

        if (kb.sKey.wasPressedThisFrame)
            goalPosition = anchorS.position;

        if (kb.dKey.wasPressedThisFrame)
            goalPosition = anchorD.position;
    }

    private void LockPosition()
    {
        isLocked = true;
        transform.position = goalPosition;
    }
}
