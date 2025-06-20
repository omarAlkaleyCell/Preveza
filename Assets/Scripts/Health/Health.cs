using System;
using UnityEngine;

public class Health : MonoBehaviour,IDamagable
{
    public Action onDie;
    public float CurrentHealth { get; set; } = 100f;
    public float MaxHealth { get; set; } = 100f;
    [SerializeField] Animator anime;
    public void Die()
    {
        anime.SetTrigger("Die");
        onDie?.Invoke();
    }

    public void Heal(int amount)
    {
        if (CurrentHealth >= MaxHealth) return;
        CurrentHealth += amount;
        
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0) return;
        CurrentHealth -= damage;
        if (IsDead())
        {
            Die();
            Destroy(gameObject, 5f);
        }
        
    }
}
