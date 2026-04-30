using UnityEngine;
using UnityEngine.InputSystem;

public class RightHand : MonoBehaviour
{

    public Vector3 goalPosition;

    public Transform anchorS, anchorD;

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

        if (Keyboard.current.eKey.isPressed)
        {
            goalPosition = anchorS.position;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            goalPosition = anchorD.position;
        }
    }
}