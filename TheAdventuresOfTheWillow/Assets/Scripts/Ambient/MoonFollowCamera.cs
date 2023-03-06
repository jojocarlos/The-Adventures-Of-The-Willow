using UnityEngine;

public class MoonFollowCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public float distanceFromCamera = 10f;
    public bool followOnlyOnXAxis = true;

    private void Update()
    {
        Vector3 newPosition = cameraTransform.position + Vector3.back * distanceFromCamera;

        if (followOnlyOnXAxis)
        {
            newPosition.y = transform.position.y;
        }

        transform.position = newPosition;
    }
}
