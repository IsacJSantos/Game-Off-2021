using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] int waveCount;
    [SerializeField] ManualWave[] manualWaves;
    [SerializeField] Wave actualWave;

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
        else
        {
            // Create Wave
        }


    }
    async void StartManualWave()
    {
        for (int i = 0; i < manualWaves.Length; i++)
        {
            actualWave = manualWaves[i].wave;
            print($"Wave number: {actualWave.waveNumber}");
            await StartWaveTurn(actualWave.waveTurns);
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
            await Task.Delay(spawnDelay);
        }
        return true;
    }
}
