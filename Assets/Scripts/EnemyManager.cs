using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  public List<int> waveSize;
  public static EnemyManager instance { get; private set; }
  public List<GameObject> prefabs;
  public List<Transform> spawnPoints;
  public Transform lastSpawnPoint = null;
  public Transform player;
  public List<GameObject> enemies = new List<GameObject>();

  public float enemyInterval;
  public int currentWaveIndex;
  public int currentWaveKids;

  private void Awake()
  {
    instance = this;
    StartCoroutine(StartWaveAfterSeconds(0));
  }

  private IEnumerator StartWaveAfterSeconds(float seconds)
  {
    currentWaveKids = waveSize[currentWaveIndex];
    yield return new WaitForSeconds(seconds);
    UIManager.instance.SetWaveSize(currentWaveKids);

    while (currentWaveKids != 0)
    {
      InstantiateEnemy();
      currentWaveKids--;
      yield return new WaitForSeconds(enemyInterval);
    }
  }

  public void Add(GameObject enemy)
  {
    enemies.Add(enemy);
  }

  public void Remove(GameObject enemy)
  {
    enemies.Remove(enemy);
  }

  public void InstantiateEnemy()
  {
    List<Transform> allowedSpawnPoints = spawnPoints.GetRange(0, spawnPoints.Count);
    if (lastSpawnPoint != null) { allowedSpawnPoints.Remove(lastSpawnPoint); }

    int randomSpawnPointIndex = Random.Range(0, allowedSpawnPoints.Count);
    Transform spawnPoint = allowedSpawnPoints[randomSpawnPointIndex];

    lastSpawnPoint = spawnPoint;
    GameObject enemyPrefab = prefabs[0];

    GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    Enemy enemyRef = enemy.GetComponent<Enemy>();
    enemyRef.SetTaget(player);
    enemyRef.SetSpawnPoint(spawnPoint);
  }

  private void FixedUpdate()
  {
    if (currentWaveKids == 0 && enemies.Count == 0)
    {
      if (currentWaveIndex >= waveSize.Count)
      {
        Debug.Log("You win!");
        return;
      }

      currentWaveIndex++;
      StartCoroutine(StartWaveAfterSeconds(5));
    }
  }
}
