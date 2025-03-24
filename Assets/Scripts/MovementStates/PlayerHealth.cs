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
    public AudioClip damageSound; // Sound when hit
    public Renderer playerRenderer; // Assign the player's Renderer in the Inspector
    public float flashDuration = 0.2f; // How long the player flashes red

    private AudioSource audioSource;
    private Animator anim;
    private bool isDead = false;
    private CapsuleCollider capsuleCollider;
    private AimStateManager aim;
    private CharacterController personaje;
    private Color originalColor;

    void Start()
    {
        aim = GetComponent<AimStateManager>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        personaje = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color; // Store original color
        }

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

        // Play damage sound
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Flash red effect
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
        playerRenderer.material.color = Color.red; // Change to red
        yield return new WaitForSeconds(flashDuration);
        playerRenderer.material.color = originalColor; // Revert to original color
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