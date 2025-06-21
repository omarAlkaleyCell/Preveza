using UnityEngine;
using UnityEngine.Events;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private GameObject waterCollisionEffectPrefab;
    [SerializeField] private GameObject[] shipCollisionEffectPrefab;
    [SerializeField] UnityEvent onHitTarget;
    [SerializeField] UnityEvent onHitWater;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            Health shipHealth = other.GetComponent<Health>();
            if (shipHealth != null)
            {
                shipHealth.TakeDamage(10);
            }
            SpawnShipCollisionEffect();
            onHitTarget?.Invoke();
            Destroy(gameObject, 4f);
        }
        else if (other.CompareTag("Water"))
        {
            SpawnWaterCollisionEffect();
            onHitWater?.Invoke();
            Destroy(gameObject, 4f);
        }
    }
    private void SpawnWaterCollisionEffect()
    {
        if (waterCollisionEffectPrefab != null)
        {
            GameObject waterCollisionVFXInstance = Instantiate(waterCollisionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(waterCollisionVFXInstance, 3f);
        }
    }
    private void SpawnShipCollisionEffect()
    {
        if (shipCollisionEffectPrefab != null && shipCollisionEffectPrefab.Length > 0)
        {
            int randomIndex = Random.Range(0, shipCollisionEffectPrefab.Length);
            GameObject shipCollisionVFXInstance = Instantiate(shipCollisionEffectPrefab[randomIndex], transform.position, Quaternion.identity);
            Destroy(shipCollisionVFXInstance, 3f);
        }
    }
}
