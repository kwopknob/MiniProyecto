using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    private Animator anim;
    private bool isDead = false;
    private CapsuleCollider capsuleCollider;
   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    TakeDamage(50);
        //}
    }
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        playerHealth -= damageAmount;
        if (playerHealth <= 0)
        {
            Die();
        }
        else
        {
            //anim.SetTrigger("Damage");
        }
    }

    public void Die()
    {
        isDead = true;
        //capsuleCollider.enabled = false;

        //anim.SetTrigger("Death");

        Debug.Log("Muerte");

    }
}
