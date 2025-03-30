using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    private BoxCollider boxCol;
    PlayerHealth pHealth;
    WeaponAmmo wAmmo;
    public AudioClip healSound;
    public AudioClip ammoSound;
    [SerializeField] private int pickUpType; //0 vida 1 muniicon
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float clipLength = 0f;

            if (pickUpType == 0)
            {
                pHealth = other.GetComponent<PlayerHealth>();
                pHealth.GiveHealth(35);
                Debug.Log("Healed");

                if (healSound)
                {
                    audioSource.PlayOneShot(healSound);
                    clipLength = healSound.length;
                }
            }
            else if (pickUpType == 1)
            {
                wAmmo = other.GetComponentInChildren<WeaponAmmo>();
                wAmmo.GiveAmmo(50);

                if (ammoSound)
                {
                    audioSource.PlayOneShot(ammoSound);
                    clipLength = ammoSound.length;
                }
            }

           
            Destroy(gameObject, clipLength);
        }
    }
}
