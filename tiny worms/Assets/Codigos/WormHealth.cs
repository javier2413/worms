using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    private WormIdentity identity;

    void Start()
    {
        identity = GetComponent<WormIdentity>();
        currentHealth = maxHealth;
    }

    //lamar cuando el worm recibe daño
    public void TakeDamage(int damage)
    {
        if (!identity.isAlive) return;

        currentHealth -= damage;

        Debug.Log($"{gameObject.name} recibió {damage} de daño");

        //Termina e turn inmediatamente
        TurnManager.Instance.ForceEndTurn();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        identity.isAlive = false;
        currentHealth = 0;

        Debug.Log($"{gameObject.name} murió");

        // Opcional: desactivar controles y colisiones
        GetComponent<WormMovement>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Opcional: animación / sonido / partículas
        // Destroy(gameObject, 2f);
    }
}
