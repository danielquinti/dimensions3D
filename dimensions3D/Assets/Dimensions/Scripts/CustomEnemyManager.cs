using UnityEngine;
using Unity.FPS.Game;
using System.Collections;
using System.Collections.Generic;

namespace Unity.FPS.AI
{
    public class CustomEnemyManager : MonoBehaviour
    {
        public List<CustomEnemyController> Enemies { get; private set; }
        public int NumberOfEnemiesTotal { get; private set; }
        private int NumberOfEnemiesRemaining;
        public int MaxConcurrentEnemies = 1;
        public bool spawning = true;
        public void RegisterEnemy(CustomEnemyController enemy)
        {
            Enemies.Add(enemy);
        }

        public void UnregisterEnemy(CustomEnemyController enemyKilled)
        {
            NumberOfEnemiesRemaining--;
            // removes the enemy from the list, so that we can keep track of how many are left on the map
            Enemies.Remove(enemyKilled);
            enemyTally.SetInfo(NumberOfEnemiesRemaining + "/" + NumberOfEnemiesTotal);

        }

        public enum SpawnState { SPAWNING, WAITING, COUNTING };

        [System.Serializable]
        public class Wave
        {
            public string name;
            public float rate;
            public List<Transform> enemies;
        }

        public Wave[] waves;
        private int nextWave = 0;
        public int NextWave
        {
            get { return nextWave + 1; }
        }

        public List<Transform> spawnPoints;

        public float timeBetweenWaves = 5f;
        private float waveCountdown;
        public float WaveCountdown
        {
            get { return waveCountdown; }
        }

        private float searchCountdown = 1f;

        private SpawnState state = SpawnState.COUNTING;
        public SpawnState State
        {
            get { return state; }
        }
        protected InfoDisplay roundInfo;
        protected InfoDisplay enemyTally;
        void Start()
        {
            roundInfo = GameObject.Find("RoundNameDisplay").GetComponent<InfoDisplay>(); ;
            enemyTally = GameObject.Find("EnemyTallyDisplay").GetComponent<InfoDisplay>();
            Enemies = new List<CustomEnemyController>();
            if (spawnPoints.Count == 0)
            {
                Debug.LogError("No spawn points referenced.");
            }

            waveCountdown = timeBetweenWaves;
        }

        void Update()
        {
            if (!spawning)
            {
                return;
            }
            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive() & spawning)
                {
                    roundInfo.SetInfo("Wave completed!");
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }

        void WaveCompleted()
        {
            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;

            if (nextWave + 1 > waves.Length - 1)
            {
                nextWave = 0;
                roundInfo.SetInfo("ALL WAVES COMPLETE! Looping...");
            }
            else
            {
                nextWave++;
            }
        }

        bool EnemyIsAlive()
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
                if (NumberOfEnemiesRemaining == 0)
                {
                    return false;
                }
            }
            return true;
        }

        IEnumerator SpawnWave(Wave _wave)
        {
            NumberOfEnemiesTotal = _wave.enemies.Count;
            NumberOfEnemiesRemaining = NumberOfEnemiesTotal;
            enemyTally.SetInfo(NumberOfEnemiesRemaining + "/" + NumberOfEnemiesTotal);
            roundInfo.SetInfo(_wave.name);
            state = SpawnState.SPAWNING;
            int i = 0;
            while (i < _wave.enemies.Count & spawning)
            {
                if (Enemies.Count < MaxConcurrentEnemies)
                {
                    SpawnEnemy(_wave.enemies[i]);
                    i++;
                }
                yield return new WaitForSeconds(1f / _wave.rate);
            }


            state = SpawnState.WAITING;

            yield break;
        }

        void SpawnEnemy(Transform _enemy)
        {
            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
        }

        public void AddSpawnPoints(List<Transform> list)
        {
            spawnPoints.AddRange(list);
        }

        private List<Transform> inactive;
        public void ChangeSpawnPoints(List<Transform> updated)
        {
            inactive = new List<Transform>(spawnPoints);
            spawnPoints = updated;
        }

        public void RestoreSpawnPoints()
        {
            spawnPoints.AddRange(inactive);
        }
    }
}
