using System.Collections;
using UnityEngine;

public class SpawnEnemu : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private GameObject StartButton;

    [Header("Level Settings")]
    private int _currentEnemyIndex;
    private int _currentWaveIndex;

    [Header("Settings Enemy")]
    [SerializeField] private GameObject[] WayPoint;
    [SerializeField] private Waves[] waves;

    public float TimeWaves;

    [SerializeField] private int[] hp;
    void Start()
    {
        StartButton.SetActive(true);
    }
    public void StartGame()
    {
        StartCoroutine(Spawn());
        StartCoroutine(Health());
    }
    private IEnumerator Spawn()
    {
        while (waves.Length != _currentWaveIndex)
        {
            for (int i = 0; i < waves.Length; i++)
            {
                _currentWaveIndex++;
                yield return new WaitForSeconds(TimeWaves);

                for (int j = 0; j < waves[i].WaveSettings.NumberOfEnemy; j++)
                {
                    int random = Random.Range(0, waves[i].WaveSettings.Enemy.Length);
                    GameObject enemy = Instantiate(waves[i].WaveSettings.Enemy[random],
                        transform.position,
                        Quaternion.identity);
                    enemy.GetComponent<Enemu>().wayPoint = WayPoint;
                    enemy.GetComponent<Enemu>().NumberCount = 0;
                    enemy.GetComponent<Enemu>().Health = hp[random];
                    yield return new WaitForSeconds(waves[i].WaveSettings.TimeBetwenSpawnEnemy);
                }
            }

        }
    }
    private IEnumerator Health()
    {
        while (true)
        {
            yield return new WaitForSeconds(70);
            TimeWaves -= 0.5f;
            hp[0] += 1;
            hp[1] += 2;
            hp[2] += 5;
            hp[3] += 5;
            hp[4] += 5;
        }
    }
}
[System.Serializable]
public class Waves
{
    [SerializeField] private SettingsWaves SettingsWaves;
    public SettingsWaves WaveSettings { get => SettingsWaves; }
}

[System.Serializable]
public class SettingsWaves
{
    [SerializeField] private GameObject[] _enemy;
    public GameObject[] Enemy { get => _enemy; }

    [SerializeField] private int _numberOfEnemy;
    public int NumberOfEnemy { get => _numberOfEnemy; }

    [SerializeField] private float _timeBetwenSpawnEnemy;
    public float TimeBetwenSpawnEnemy { get => _timeBetwenSpawnEnemy; }
}

