using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;


    public void StratSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());

    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-6.7f, 6.80f), 6, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }


    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        while (_stopSpawning == false)
        {
            Vector3 posToPowerupSpawn = new Vector3(Random.Range(-6.7f, 6.80f), 6, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], posToPowerupSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDead()
    {
        _stopSpawning = true;
    }
}
