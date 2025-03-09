using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Porperties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShoht;
    AimStateManager aim;


    [SerializeField] AudioClip gunShot;
    [HideInInspector]public AudioSource audiosource;

    [HideInInspector]public WeaponAmmo ammo;

    ActionStateManager actions;

    ParticleSystem muzzlFlash;
    
    public  Transform leftHandTarget, leftHandHint;

    WeaponClassManager weaponClass;

    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponentInParent<AimStateManager>();
        fireRateTimer = fireRate;
        
        
        actions = GetComponentInParent<ActionStateManager>();
        muzzlFlash = GetComponentInChildren<ParticleSystem>();
    }
    private void OnEnable()
    {
        if(weaponClass == null)
        {
            weaponClass = GetComponentInParent<WeaponClassManager>();
            ammo = GetComponent<WeaponAmmo>();
            audiosource = GetComponent<AudioSource>();
        }
        weaponClass.SetCurrentWeapon(this);
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Shouldfire()) Fire();
        Debug.Log(ammo.currentAmmo);
    }

    bool Shouldfire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0 ) return false;
        if (actions.currentState == actions.Reload) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0; 
        audiosource.PlayOneShot(gunShot);
        barrelPos.LookAt(aim.aimPos);
        ammo.currentAmmo--;
        for (int i = 0; i < bulletsPerShoht; i++)
        { 
         GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward*bulletVelocity,ForceMode.Impulse);
            muzzlFlash.Play();
        }
       
    }

  
}
