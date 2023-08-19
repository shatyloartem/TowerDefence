using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using TD.Interfaces;
using Unity.Mathematics;
using UnityEngine.Jobs;

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
                Debug.Log(_target);
        }

/*        private Transform GetFireTarget()
        {
            
            handle.Complete();

            Transform target = activeEnemies[jobResult[0]];

            enemiesPositions.Dispose();
            jobResult.Dispose();

            return target;
        }*/

        public JobHandle StartCalculateTargetJob(NativeArray<float3> enemiesPositions, NativeArray<int> jobResult, int jobIndex)
        {
            CalculateIfFireJob job = new CalculateIfFireJob()
            {
                positions = enemiesPositions,
                jobResult = jobResult,
                fireRange = _stats.fireRange,
                turret = _turret.position,
                turretIndex = jobIndex
            };

            JobHandle handle = job.Schedule(enemiesPositions.Length, enemiesPositions.Length);

            return handle;
        }

        public void SetFireTarget(Transform target) => _target = target;
    }

    [BurstCompile]
    public struct CalculateIfFireJob : IJobParallelFor
    {
        public NativeArray<float3> positions;
        public NativeArray<int> jobResult;

        public float fireRange;
        public float3 turret;

        public int turretIndex;

        public void Execute(int index)
        {
            if (math.distance(positions[index], turret) < fireRange)
            {
                jobResult[turretIndex] = index;
                return;
            }
        }
    }
}
