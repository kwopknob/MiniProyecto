using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public AudioClip ReloadSound;
    


    /* [HideInInspector]*/
    public int currentAmmo;
    // Start is called before the first frame update

    
    void Start()
    {
        currentAmmo = clipSize;
    }
   


    public void Reload()
    {
        if (extraAmmo >= clipSize)
        {
            int ammoToRelado = clipSize - currentAmmo;
            extraAmmo -= ammoToRelado;
            currentAmmo += ammoToRelado;
        }
        else if (extraAmmo > 0)
        {
            if (extraAmmo + currentAmmo > clipSize)
            {

                int leftOverAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftOverAmmo;
                currentAmmo = clipSize;

            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }

}
