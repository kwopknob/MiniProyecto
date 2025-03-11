using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienHealth : MonoBehaviour
{
    public int health = 100;
    private Animator anim;
    private bool isDead = false;
    private CapsuleCollider capsuleCollider;
    private NavMeshAgent navMeshAgent;
    public float destroyTimer = 15f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(50);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("Damage");
        }
    }

    public void Die() 
    {
        isDead = true;
        anim.SetTrigger("Death");
        capsuleCollider.enabled = false;
        navMeshAgent.isStopped = true;
        StartCoroutine(DespawnTimer());

    }

    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f); // Attacking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 15f); // Detection (Start Chasing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 18f); // Stop Chasing
    }
}
