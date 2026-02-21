using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject robotPrefab;
    [SerializeField] Transform enemyListTransform;
    [SerializeField] float spawnDelay = 5f;

    PlayerHealth player;
    GameManager gameManager;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        gameManager = FindFirstObjectByType<GameManager>();
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (!player.IsDie)
        {
            GameObject enemy = Instantiate(robotPrefab, transform.position, transform.rotation, enemyListTransform);
            enemy.GetComponent<EnemyHealth>().Init(gameManager);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
