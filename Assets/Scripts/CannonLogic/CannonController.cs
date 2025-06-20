using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;
public class CannonController : MonoBehaviour
{
    public UnityEvent OnFireCannon;
    [SerializeField] private float fireCooldown = 1f;
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private GameObject cannonBallVFXPrefab;
    [SerializeField] private float forcePower;

    public void FireCannon()
    {
        Invoke(nameof(SpawnCannonBall), fireCooldown);
    }

    private void SpawnCannonBall()
    {
        if (cannonBallPrefab == null || cannonTransform == null)
        {
            return;
        }
        GameObject cannonBall = Instantiate(cannonBallPrefab, cannonTransform.position, cannonTransform.rotation);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(cannonTransform.forward * forcePower, ForceMode.Impulse); // Adjust force as needed
        }
        SpawnCannonBallVFX();
        OnFireCannon?.Invoke();
    }

    private void SpawnCannonBallVFX()
    {
        if (cannonBallVFXPrefab == null) return;
        GameObject cannonBallVFX = Instantiate(cannonBallVFXPrefab, cannonTransform.position, cannonTransform.rotation);
        Destroy(cannonBallVFX, 3f);
    }
}
