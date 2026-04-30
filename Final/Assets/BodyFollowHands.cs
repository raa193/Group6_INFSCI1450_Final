using UnityEngine;

public class BodyFollowHands : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    public float followSpeed = 10f;

    void LateUpdate()
    {
        Vector3 midpoint = (leftHand.position + rightHand.position) * 0.5f;

        transform.position = Vector3.Lerp(
            transform.position,
            midpoint,
            Time.deltaTime * followSpeed
        );
    }
}