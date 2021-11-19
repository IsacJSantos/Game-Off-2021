using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] int waveCount = 1;
    [SerializeField] ManualWave[] manualWaves;
    [SerializeField] Wave actualWave;
    [SerializeField] Transform[] spawnPoints;

    int spawnDelay = 1000;
 
 
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
            StartManualWave();
        }
    }
    async void StartManualWave()
    {

        tokenSource.Token.ThrowIfCancellationRequested();
        for (int i = 0; i < manualWaves.Length; i++)
        {
            actualWave = manualWaves[i].wave;
            print($"Wave number: {actualWave.waveNumber}");
            await StartWaveTurn(actualWave.waveTurns);
            waveCount++;
        }

    }
    async Task<bool> StartWaveTurn(WaveTurn[] waveTurns)
    {
        for (int i = 0; i < waveTurns.Length; i++)
        {
            print("Waiting for next wave turn");

            await Task.Delay(actualWave.waveTurnsDelay);

            await SpawnEnemys(waveTurns[i].Enemyreferences);
        }
        print("End of wave turns");
        return true;
    }
    async Task<bool> SpawnEnemys(string[] references)
    {
        for (int i = 0; i < references.Length; i++)
        {
            print(references[i]);
            int randomPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(Resources.Load(references[i]), spawnPoints[randomPoint].position, Quaternion.identity);
            await Task.Delay(spawnDelay);
        }
        return true;
    }
}
