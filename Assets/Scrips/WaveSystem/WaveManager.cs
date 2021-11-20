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

    float spawnDelay = 1.5f;


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

            yield return StartCoroutine(SpawnEnemys(waveTurns[i].Enemyreferences));
        }
        print("End of wave turns");
    }
    IEnumerator SpawnEnemys(string[] references)
    {
        for (int i = 0; i < references.Length; i++)
        {
            print(references[i]);
            int randomPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(Resources.Load(references[i]), spawnPoints[randomPoint].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
