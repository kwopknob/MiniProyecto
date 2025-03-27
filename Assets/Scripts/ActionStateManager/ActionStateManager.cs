using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
   [HideInInspector] public ActionBaseState currentState;

    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();
    public SwapState Swap = new SwapState();

    public WeaponManager currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;
     AudioSource audioSource;

    [HideInInspector] public Animator anim;

    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint lHandK;

    public AudioClip footstep;
  


    void Start()
    {
        SwitchState(Default);
        //ammo = currentWeapon.GetComponent<WeaponAmmo>();
        //audioSource = GetComponentInChildren<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        ammo.Reload();
        SwitchState(Default);
    }

    public void ReloadSound()
    {
        audioSource.PlayOneShot(ammo.ReloadSound);
    }
    public void MagSound()
    {
        audioSource.PlayOneShot(ammo.MagReload);
    }

    public void SetWeapon(WeaponManager weapon)
    {
        currentWeapon = weapon;
        audioSource = weapon.audiosource;
        ammo = weapon.ammo;

    }

    public void PlayFootstep()
    {
        audioSource.PlayOneShot(footstep);
    }


}
