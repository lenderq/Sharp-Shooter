using StarterAssets;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int startingHealth = 5;
    [SerializeField] CinemachineCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverContainer;

    int currentHealth;
    int gameOverVirtualCameraPriority = 20;

    public bool IsDie { get; private set; }

    void Awake()
    {
        currentHealth = startingHealth;
        AdjustShieldUI();
        IsDie = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        AdjustShieldUI();

        if (currentHealth <= 0 && !IsDie)
        {
            Die();
        }
    }

    private void Die()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
        IsDie = true;
        gameOverContainer.SetActive(true);
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);
        Destroy(this.gameObject);
    }

    private void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i > currentHealth - 1)
            {
                shieldBars[i].gameObject.SetActive(false);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(true);
            }
        }
    }
}
