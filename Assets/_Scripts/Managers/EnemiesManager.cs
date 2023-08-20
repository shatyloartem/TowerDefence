using System.Collections.Generic;
using TD.Singleton;
using TD.Interfaces;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System.Collections;
using Unity.Mathematics;

namespace TD.Managers
{
    public class EnemiesManager : MonoBehaviour, IEnemiesManager
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
        private List<float3> _spawnedEnemiesPositions = new List<float3>();

        private static EnemiesManager Instance;

        private void Awake()
        {
            Instance = this;

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
                IEnemy enemy = _enemyFactory.SpawnEnemy(enemyIndex);

                // Setting enemy move animation
                float animationDuration = _pathDistance / enemy.GetStats().Speed;
                enemy.GetTransform().DOPath(_enemiesPath, animationDuration).
                    SetEase(Ease.Linear).
                    OnWaypointChange((int index) => RotateEnemyToward(enemy, _enemiesPath[index])).
                    OnComplete(() => OnEnemyRichedEnd(enemy));

                _spawnedEnemies.Add(enemy.GetTransform());
                _spawnedEnemiesPositions.Add(enemy.GetTransform().position);

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

        private void RotateEnemyToward(IEnemy enemy, Vector3 target)
        {
            enemy.GetTransform().LookAt(target);
        }

        private void OnEnemyRichedEnd(IEnemy enemy)
        {
            Destroy(enemy.GetGameObject());

            Debug.Log("Enemy reached finish");
        }

        private float GetPathDistance(Vector3[] path)
        {
            float result = 0;

            for (int i = 1; i < path.Length; i++)
                result += Vector3.Distance(path[i - 1], path[i]);

            return result;
        }

        public static List<Transform> GetSpawnedEnemies() => Instance._spawnedEnemies;

        public static List<float3> GetSpawnedEnemiesPositions() => Instance._spawnedEnemiesPositions;

        private WaveScriptableObject[] LoadWaves => Resources.LoadAll<WaveScriptableObject>("Waves Data");
    }
}
