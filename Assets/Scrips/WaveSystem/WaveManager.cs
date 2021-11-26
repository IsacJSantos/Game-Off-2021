using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] int waveCount = 1;
    [SerializeField] ManualWave[] manualWaves;
    [SerializeField] Wave actualWave;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] EnemysContainer enemysContainer;

    float spawnDelay = 0.3f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetWave();
        }
    }

    void GetWave()
    {
        if (manualWaves != null && manualWaves.Length > 0)
        {
            StartCoroutine(StartManualWave());
        }
    }
    IEnumerator StartManualWave()
    {
        for (int i = 0; i < manualWaves.Length; i++)
        {
            actualWave = manualWaves[i].wave;
            print($"Wave number: {actualWave.waveNumber}");
            yield return StartCoroutine(StartWaveTurn(actualWave.waveTurns));
            waveCount++;
        }

    }
    IEnumerator StartWaveTurn(WaveTurn[] waveTurns)
    {
        for (int i = 0; i < waveTurns.Length; i++)
        {
            print("Waiting for next wave turn");

            yield return new WaitForSeconds(actualWave.waveTurnsDelay);

            yield return StartCoroutine(SpawnEnemys(waveTurns[i].EnemyTypes));
        }
        print("End of wave turns");
    }
    IEnumerator SpawnEnemys(EnemySpawn[] enemys)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            for (int j = 0; j < enemys[i].amout; j++)
            {
                int randomPoint = Random.Range(0, spawnPoints.Length);
                Instantiate(GetEnemyPrefab((int)enemys[i].enemyType), spawnPoints[randomPoint].position, Quaternion.identity);
                yield return new WaitForSeconds(spawnDelay);
            }

        }
    }

    GameObject GetEnemyPrefab(int index)
    {
        return enemysContainer.enemyList[index];
    }
}
