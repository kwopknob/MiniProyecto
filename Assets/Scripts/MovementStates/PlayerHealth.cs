using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public TextMeshProUGUI healthText;
    public GameObject deathUI;
    private Animator anim;
    private bool isDead = false;
    private CapsuleCollider capsuleCollider;
    AimStateManager aim;
    CharacterController perosnaje;
    
   

    void Start()
    {
        aim = GetComponent<AimStateManager>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        perosnaje = GetComponent<CharacterController>();
        UpdateHealthUI();
        if (deathUI != null)
        {
            deathUI.SetActive(false);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        playerHealth -= damageAmount;
        UpdateHealthUI();

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + playerHealth;
        }
    }

    public void Die()
    {
        isDead = true;
        //Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (deathUI != null)
        {
            deathUI.SetActive(true);
        }
        Debug.Log("Muerte");
        aim.enabled = false;
        perosnaje.enabled = false;
        

    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
