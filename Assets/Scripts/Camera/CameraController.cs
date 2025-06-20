using UnityEngine;
using System.Collections;
using System;
using UnityEngine.TextCore.Text;
public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Transform cameraObject;
    [SerializeField] private float moveSpeed = 1f;
    Coroutine cameraMoveCoroutine;
    public void MoveCameraToTargetLocation(Transform target, Action onReachTarget = null)
    {
        if (cameraMoveCoroutine != null)
        {
            StopCoroutine(cameraMoveCoroutine);
        }
        cameraMoveCoroutine = StartCoroutine(MoveCameraCoroutine(target, moveSpeed, onReachTarget));
    }

    private IEnumerator MoveCameraCoroutine(Transform target, float speed, Action onReachTarget = null)
    {
        cameraObject.GetComponent<CharacterController>().enabled = false;
        Camera.main.transform.LookAt(target);

        Vector3 velocity = Vector3.zero;
        float rotationSpeed = speed;
        
        while (true)
        {
            float distance = Vector3.Distance(cameraObject.position, target.position);

            // Smoothly move position
            cameraObject.position = Vector3.SmoothDamp(
                cameraObject.position,
                target.position,
                ref velocity,
                0.3f // Adjust this value for faster or slower deceleration
            );

            // Smoothly rotate forward
            cameraObject.forward = Vector3.Slerp(
                cameraObject.forward,
                target.forward,
                rotationSpeed * Time.deltaTime
            );

            if (distance < 0.01f && Vector3.Angle(cameraObject.forward, target.forward) < 0.1f)
            {
                break;
            }

            yield return null;
        }

        cameraObject.GetComponent<CharacterController>().enabled = true;
        onReachTarget?.Invoke();
        cameraObject.position = target.position;
        cameraObject.forward = target.forward;
    }
}
