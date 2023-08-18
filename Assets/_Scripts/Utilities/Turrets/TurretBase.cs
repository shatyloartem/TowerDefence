using Zenject;
using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using System.Collections.Generic;
using TD.Interfaces;
using Unity.Mathematics;

namespace TD.Turrets
{
    public abstract class TurretBase : MonoBehaviour
    {
        [SerializeField]
        private TurretStatsScriptableObject _stats;

        [Inject]
        private IEnemiesManager _enemiesManager;

        private Transform _turret;

        private void Awake()
        {
            _turret = transform;

            Debug.Log(GetFireTarget());
        }

        private Transform GetFireTarget()
        {
            List<Transform> activeEnemies = _enemiesManager.GetSpawnedEnemies();

            NativeArray<float3> enemiesPositions = new NativeArray<float3>(activeEnemies.Count, Allocator.Temp);
            
            bool _canFire = false;

            CalculateIfFireJob job = new CalculateIfFireJob()
            {
                fireRange = _stats.fireRange,
                turret = _turret,
                canFire = _canFire
            };

            JobHandle handle = job.Schedule();
            handle.Complete();

            return target;
        }
    }

    [BurstCompile]
    public struct CalculateIfFireJob : IJob
    {
        public float fireRange;
        public Transform target;
        public Transform turret;

        public bool canFire;

        public void Execute()
        {
            canFire = (Vector3.Distance(target.position, turret.position)) < fireRange;
        }
    }
}
