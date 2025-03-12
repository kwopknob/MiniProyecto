using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    float timer;
    [SerializeField] private int bulletDamage;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy) Destroy(this.gameObject);

    }

    //private void OnCollisionEnter(Collision collision)
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AlienHealth>() != null) 
        {
            other.GetComponent<AlienHealth>().TakeDamage(bulletDamage);
        }
        Destroy(this.gameObject);
    }
}
