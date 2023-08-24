using Unity.Jobs;
using UnityEngine;
using Unity.Collections;
using TD.Interfaces;
using Unity.Mathematics;
using UnityEngine.Jobs;
using System.Collections.Generic;
using TD.Managers;
using Sirenix.OdinInspector;

namespace TD.Turrets
{
    public abstract class TurretBase : MonoBehaviour, ITurretBase
    {
        [TabGroup("Tower settings")]
        [SerializeField]
        protected TurretStatsScriptableObject _stats;

        [Space(7)]

        [TabGroup("Essentials")]
        [SerializeField]
        protected Transform _tower;

        [TabGroup("Essentials")]
        [SerializeField]
        protected ParticleSystem _particles;


        protected Transform _turret;
        protected Transform _target;

        private float lastTimeFired;

        private void Awake()
        {
            _turret = transform;
        }

        private void Update()
        {
            if (_target == null) return;

            TurnTowerToTarget();

            TryToFire();
        }

        #region Firing
        
        private void TryToFire()
        {
            if (Time.time < lastTimeFired + _stats.fireCooldown) return;

            if(Vector3.Angle(_turret.forward, _target.position - _turret.position) < _stats.fireAngle) return;

            lastTimeFired = Time.time;

            if(_particles != null)
                PlayFireParticles();

            Fire();
        }

        protected virtual void PlayFireParticles()
        {
            
        }

        protected virtual void Fire()
        {

        }
        
        #endregion

        #region Tower turning

        private void TurnTowerToTarget()
        {
            Quaternion rotTarget = Quaternion.LookRotation(_target.position - _tower.position);
            _tower.rotation = Quaternion.RotateTowards(_tower.rotation, rotTarget, _stats.towerRotationSpeed * Time.deltaTime);
        }

        #endregion

        #region Finding enemies

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

    // Commented this attribute because of error when calling methood EnemiesManager.GetSpawnedEnemiesPositions();
    //[BurstCompile]
    public struct CalculateIfFireJob : IJobParallelFor
    {
        [NativeDisableParallelForRestriction]
        public NativeArray<int> jobResult;

        public float fireRange;
        public float3 turret;

        public int turretIndex;

        public void Execute(int index)
        {
            if (jobResult[turretIndex] != 0)
                return;

            List<float3> activeEnemies = EnemiesManager.GetSpawnedEnemiesPositions();

            float dist = math.distance(activeEnemies[index], turret);

            if (dist < fireRange)
            {
                jobResult[turretIndex] = index+1;
                return;
            }
        }
    }

#endregion
}
