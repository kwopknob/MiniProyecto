using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private WeaponAmmo weaponAmmo;
    [SerializeField] private TextMeshProUGUI ammotxt;
    private int enemyCount;
    [SerializeField] int maxEnemyCount;
    [SerializeField] private GameObject winUI;
    [SerializeField] private Button sceneChangeButton;
    [SerializeField] private string sceneName;


    private void Start()
    {
        enemyCount = maxEnemyCount;
        winUI.SetActive(false);

        if (sceneChangeButton != null)
        {
            sceneChangeButton.onClick.AddListener(ChangeScene);
        }
    }

    void Update()
    {
        UiUpdate();
    }

    void UiUpdate()
    {
        if (weaponAmmo == null) weaponAmmo = FindAnyObjectByType<WeaponAmmo>();
        ammotxt.SetText(weaponAmmo.GetAmmo().ToString() + "/" + weaponAmmo.GetMaxAmmo().ToString());
    }

    public void KillCount()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            winUI.SetActive(true);
            
        }
    }

    public void ClearRefernce()
    {
        weaponAmmo = null;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
