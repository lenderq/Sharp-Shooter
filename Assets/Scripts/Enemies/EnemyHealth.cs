using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    [SerializeField] GameObject destroyVFX;

    NavMeshAgent navMeshAgent;
    Robot robotScript;
    Turret turretScript;

    int currentHealth;

    bool isDie = false;

    GameManager gameManager;

    void Awake()
    {
        currentHealth = startingHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        robotScript = GetComponent<Robot>();
        turretScript = GetComponent<Turret>();
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();

        gameManager.AdjustEnemyLeft(1);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isDie)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        isDie = true;
        gameManager.AdjustEnemyLeft(-1);
        Instantiate(destroyVFX, transform.position, Quaternion.identity);

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        if (robotScript != null)
        {
            robotScript.enabled = false;
            navMeshAgent.enabled = false;
            Destroy(this.gameObject, 1.7f);
        }
        else if (turretScript != null)
        {
            turretScript.enabled = false;
            Destroy(this.gameObject, 1.7f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
