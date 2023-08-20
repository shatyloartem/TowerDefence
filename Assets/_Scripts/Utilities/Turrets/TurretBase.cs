using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using TD.Interfaces;
using Unity.Mathematics;
using UnityEngine.Jobs;
using System.Collections.Generic;
using TD.Managers;

namespace TD.Turrets
{
    public class TurretBase : MonoBehaviour, ITurretBase
    {
        [SerializeField]
        private TurretStatsScriptableObject _stats;

        private Transform _turret;

        private Transform _target;

        private void Awake()
        {
            _turret = transform;
        }

        private void LateUpdate()
        {
            if(_target != null)
                Debug.DrawLine(_target.position, _turret.position);
        }

        public JobHandle StartCalculateTargetJob(int enemiesCount , NativeArray<int> jobResult, int jobIndex, JobHandle dependentJob)
        {
            CalculateIfFireJob job = new CalculateIfFireJob()
            {
                jobResult = jobResult,
                fireRange = _stats.fireRange,
                turret = _turret.position,
                turretIndex = jobIndex
            };

            JobHandle handle = job.Schedule(enemiesCount, enemiesCount, dependentJob);

            return handle;
        }

        public void SetFireTarget(Transform target) => _target = target;
    }

    [BurstCompile]
    public struct CalculateIfFireJob : IJobParallelFor
    {
        public NativeArray<int> jobResult;

        public float fireRange;
        public float3 turret;

        public int turretIndex;

        public void Execute(int index)
        {
            List<float3> activeEnemies = EnemiesManager.GetSpawnedEnemiesPositions();

            if (math.distance(activeEnemies[index], turret) < fireRange)
            {
                jobResult[turretIndex] = index;
                return;
            }
        }
    }
}
