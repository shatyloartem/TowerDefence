using System.Collections.Generic;
using TD.Factories;
using TD.Singleton;
using TD.Enemies;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System.Collections;

namespace TD.Managers
{
    public class EnemiesManager : Singleton<EnemiesManager>
    {
        [SerializeField] private float spawnEnemyDelay = .9f;

        [Space(6)]

        [SerializeField] private Transform _enemiesPathParent;
        private Vector3[] _enemiesPath;
        private float _pathDistance;

        [Inject]
        private IEnemyFactory _enemyFactory;

        private WaveScriptableObject[] _waves;

        private List<Transform> _spawnedEnemies = new List<Transform>();

        protected override void Awake()
        {
            _waves = LoadWaves;
            _enemiesPath = LoadEnemiesPath();
            _pathDistance = GetPathDistance(_enemiesPath);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
                SpawnWave(_waves[0]);
        }

        public void SpawnWave(WaveScriptableObject wave) => StartCoroutine(SpawnWaveCoroutine(wave));

        private IEnumerator SpawnWaveCoroutine(WaveScriptableObject wave)
        {
            foreach (int enemyIndex in wave.enemiesAtWave)
            {
                // Spawning enemy
                Enemy enemy = _enemyFactory.SpawnEnemy(enemyIndex);

                // Setting enemy move animation
                float animationDuration = _pathDistance / enemy.Stats.Speed;
                enemy.Transform.DOPath(_enemiesPath, animationDuration).
                    SetEase(Ease.Linear).
                    OnWaypointChange((int index) => RotateEnemyToward(enemy, _enemiesPath[index])).
                    OnComplete(() => OnEnemyRichedEnd(enemy));

                _spawnedEnemies.Add(enemy.Transform);

                yield return new WaitForSeconds(spawnEnemyDelay);
            }
        }

        private Vector3[] LoadEnemiesPath()
        {
            Transform[] path = _enemiesPathParent.GetComponentsInChildren<Transform>();
            Vector3[] result = new Vector3[path.Length - 1];

            // Starting from 1 because first value in path is parent
            for(int i = 1; i < path.Length; i++)
                result[i-1] = path[i].position;

            return result;
        }

        private void RotateEnemyToward(Enemy enemy, Vector3 target)
        {
            enemy.Transform.LookAt(target);
        }

        private void OnEnemyRichedEnd(Enemy enemy)
        {
            Destroy(enemy.GameObject);

            Debug.Log("Enemy reached finish");
        }

        private float GetPathDistance(Vector3[] path)
        {
            float result = 0;

            for (int i = 1; i < path.Length; i++)
                result += Vector3.Distance(path[i - 1], path[i]);

            return result;
        }

        private WaveScriptableObject[] LoadWaves => Resources.LoadAll<WaveScriptableObject>("Waves Data");
    }
}
