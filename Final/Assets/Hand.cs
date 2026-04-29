using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 12f;
    public Rigidbody rb;

    float targetTimer;
    float holdTime = 5f;

    void FixedUpdate()
    {
        if (target == null)
            return;

        // countdown timer
        targetTimer -= Time.fixedDeltaTime;

        if (targetTimer <= 0f)
        {
            target = null;
            return;
        }

        rb.linearVelocity = Vector3.zero;

        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            Time.fixedDeltaTime * moveSpeed
        );
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        this.targetTimer = holdTime;
    }
}