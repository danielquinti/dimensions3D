using UnityEngine;
using Unity.FPS.Game;
using System.Collections;
using System.Collections.Generic;

namespace Unity.FPS.AI
{
    public class CustomEnemyManager : MonoBehaviour
    {
        private List<CustomEnemyController> Enemies;
        private int NumberOfEnemiesTotal;
        private int NumberOfEnemiesRemaining;
        public int MaxConcurrentEnemies = 1;
        public bool active = true;

        // keep track of how many enemies are left from the round
        public void RegisterEnemy(CustomEnemyController enemy)
        {
            Enemies.Add(enemy);
        }

        public void UnregisterEnemy(CustomEnemyController enemyKilled)
        {
            NumberOfEnemiesRemaining--;
            Enemies.Remove(enemyKilled);
            enemyTally.SetInfo(NumberOfEnemiesRemaining + "/" + NumberOfEnemiesTotal);

        }
        // this represents the possible phases of a spawning wave
        // used for implementing a simple state machine
        public enum SpawnState { SPAWNING, WAITING, COUNTING };

        // all wave attributes can be edited from the inspector
        // for each individual wave
        [System.Serializable]
        public class Wave
        {
            public string name;
            public float rate;
            public List<Transform> enemies;
        }

        // iterate through the waves that were assigned from the inspector
        public Wave[] waves;
        private int nextWave = 0;

        // locations where enemies may spawn at a given moment
        public List<Transform> spawnPoints;

        public float timeBetweenWaves = 5f;
        // time left until the next wave
        private float waveCountdown;

        // for efficiency reasons, time to wait between checks for remaining enemies
        private float searchCountdown = 1f;

        // current state of the machine
        private SpawnState state = SpawnState.COUNTING;

        // references to the scripts that update the text renderers in the HUD
        protected InfoDisplay roundInfo;
        protected InfoDisplay enemyTally;
        void Start()
        {
            roundInfo = GameObject.Find("RoundNameDisplay").GetComponent<InfoDisplay>(); ;
            enemyTally = GameObject.Find("EnemyTallyDisplay").GetComponent<InfoDisplay>();
            // enemy list starts empty
            Enemies = new List<CustomEnemyController>();
            if (spawnPoints.Count == 0)
            {
                Debug.LogError("No spawn points referenced.");
            }

            // the countdown is initialized
            waveCountdown = timeBetweenWaves;
        }
        // update is not robust to FPS changes
        // so time synchronization is performed
        void Update()
        {
            if (!active)
            {
                return;
            }
            // waiting for the player to kill all enemies
            if (state == SpawnState.WAITING)
            {
                // all enemies are dead
                if (!EnemyIsAlive() & active)
                {
                    roundInfo.SetInfo("Wave completed!");
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }
            // a new wave must start now
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
            // reset the counter
            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;
            // cyclic wave list traversal
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
            // initialize wave
            NumberOfEnemiesTotal = _wave.enemies.Count;
            NumberOfEnemiesRemaining = NumberOfEnemiesTotal;
            enemyTally.SetInfo(NumberOfEnemiesRemaining + "/" + NumberOfEnemiesTotal);
            roundInfo.SetInfo(_wave.name);
            state = SpawnState.SPAWNING;
            // spawn all enemies
            int i = 0;
            while (i < _wave.enemies.Count & active)
            {
                // avoid surpassing the concurrent enemies threshold
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
            // each enemy appears at a random spawn point
            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Instantiate(_enemy, _sp.position, _sp.rotation);
        }


        /* 
         * spawners can be active, temporarily disabled 
         * and restored from other scripts
         */
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
