using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [SerializeField] int waveCount;
    [SerializeField] float _delaToStartFirstWave;
    [SerializeField] ManualWave[] manualWaves;
    [SerializeField] Wave actualWave;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] EnemysContainer enemysContainer;
    [SerializeField] int _enemysCount;

    [SerializeField] TextMeshProUGUI _HUDwaveText;
    [SerializeField] TextMeshProUGUI _HUDTurnTimeText;

    float spawnDelay = 0.85f;
    bool _isFirst;
    bool _checkRemaningEnemys;
    private void Awake()
    {
        Events.OnStartWaves += StartWaves;
        Events.OnNextWave += NextWave;
        Events.OnEnemyDie += OnEnemyDie;
    }
    private void OnDestroy()
    {
        Events.OnStartWaves -= StartWaves;
        Events.OnNextWave -= NextWave;
        Events.OnEnemyDie -= OnEnemyDie;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartWaves();
        }

        CheckRemaningEnemys();
    }

    void StartWaves()
    {
        if (manualWaves != null && manualWaves.Length > 0)
        {
            _isFirst = true;
            StartCoroutine(StartManualWave());
        }
    }
    void NextWave()
    {
        _isFirst = false;
        _enemysCount = 0;
        StartCoroutine(StartManualWave());
    }

    void OnEnemyDie()
    {
        _enemysCount--;
    }
    IEnumerator StartManualWave()
    {
        _HUDwaveText.text = $"{waveCount + 1}/{manualWaves.Length}";
        actualWave = manualWaves[waveCount].wave;
        print($"Wave number: {actualWave.waveNumber}");
        yield return StartCoroutine(StartWaveTurn(actualWave.waveTurns));
        waveCount++;
       
    }

    IEnumerator StartWaveTurn(WaveTurn[] waveTurns)
    {
        for (int i = 0; i < waveTurns.Length; i++)
        {
            print("Waiting for next wave turn");

            yield return StartCoroutine(CountDownTurnTime(i == 0 ? _delaToStartFirstWave : actualWave.waveTurnsDelay));

            yield return StartCoroutine(SpawnEnemys(waveTurns[i].EnemyTypes));
        }
        print("End of wave turns");
        _checkRemaningEnemys = true;
    }
    IEnumerator SpawnEnemys(EnemySpawn[] enemys)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            for (int j = 0; j < enemys[i].amout; j++)
            {
                _enemysCount++;
                int randomPoint = Random.Range(0, spawnPoints.Length);
                Instantiate(GetEnemyPrefab((int)enemys[i].enemyType), spawnPoints[randomPoint].position, Quaternion.identity);
                yield return new WaitForSeconds(spawnDelay);
            }

        }
    }
    void CheckRemaningEnemys()
    {
        if (_checkRemaningEnemys)
        {
            if (_enemysCount <= 0)
            {
                _checkRemaningEnemys = false;
                if (waveCount <= manualWaves.Length - 1)
                {
                    Events.OnEndWave?.Invoke();
                    Events.OnShowCards?.Invoke();
                }
                else
                {
                    Debug.LogWarning("Victory");
                    SceneManager.LoadScene(2);
                    //Events.OnWinGame?.Invoke();
                }

            }
        }
    }

    IEnumerator CountDownTurnTime(float turnDelay) 
    {
        float count = turnDelay;
        while (count >= 0)
        {
            _HUDTurnTimeText.text = $"{count}s";
            count--;
            yield return new WaitForSeconds(1);
        }
      
    }
    GameObject GetEnemyPrefab(int index)
    {
        return enemysContainer.enemyList[index];
    }
}
