using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHand : MonoBehaviour
{
    public Vector3 goalPosition;

    public Transform anchorW, anchorA;

    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
        goalPosition = initialPosition;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, goalPosition, 0.1f);
    }

    private void Update()
    {

        if (Keyboard.current.wKey.isPressed)
        {
            goalPosition = anchorW.position;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            goalPosition = anchorA.position;
        }
    }
}