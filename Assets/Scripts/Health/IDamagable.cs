using UnityEngine;

public interface IDamagable
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    
    void TakeDamage(int damage);
    void Heal(int amount);
    bool IsDead();
    void Die();
}
