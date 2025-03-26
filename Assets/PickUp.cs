using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    private BoxCollider boxCol;
    PlayerHealth pHealth;
    WeaponAmmo wAmmo;
    [SerializeField] private int pickUpType; //0 vida 1 muniicon

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickUpType == 0)
            {
                pHealth = other.GetComponent<PlayerHealth>();
                pHealth.GiveHealth(35);
                Debug.Log("Healed");
            }
            else if (pickUpType == 1)
            {
                
                wAmmo = other.GetComponentInChildren<WeaponAmmo>();
                wAmmo.GiveAmmo(50);


            }
            Destroy(gameObject);
            
        }

    }
}
