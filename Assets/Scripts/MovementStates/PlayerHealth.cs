using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static int playerHealth = 100;
    public TextMeshProUGUI healthText;
    public GameObject deathUI;
    public AudioClip damageSound; 
    public Renderer playerRenderer;
    public float flashDuration = 0.2f;

    private AudioSource audioSource;
    private Animator anim; 
    private bool isDead = false;
    private CapsuleCollider capsuleCollider;
    private AimStateManager aim;
    private CharacterController personaje;
    private Color originalColor;

    void Start()
    {
        string name = SceneManager.GetActiveScene().name;
        aim = GetComponent<AimStateManager>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        personaje = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        if (name == "Nivel1")
        
        {
            playerHealth = 100;
        }

        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }

        UpdateHealthUI();
        if (deathUI != null)
        {
            deathUI.SetActive(false);
        }
    }
    public void GiveHealth(int healthGiven)
    {

        playerHealth = Mathf.Clamp((playerHealth + healthGiven), 0, 100);
        UpdateHealthUI();

    }
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        playerHealth -= damageAmount;
        UpdateHealthUI();

       
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

       
        if (playerRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

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

    IEnumerator FlashRed()
    {
        playerRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        playerRenderer.material.color = originalColor; 
    }

    public void Die()
    {
        isDead = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (deathUI != null)
        {
            deathUI.SetActive(true);
        }

        Debug.Log("Muerte");
        aim.enabled = false;
        personaje.enabled = false;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}