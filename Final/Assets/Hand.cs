using UnityEngine;

public class Hand : MonoBehaviour
{
    public Vector3 targetPosition;
    public float moveSpeed = 12f;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.fixedDeltaTime * moveSpeed
        );
    }

    public void SetTarget(Vector3 pos)
    {
        targetPosition = pos;
    }
}