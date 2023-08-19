using UnityEngine;
using TD.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Zenject;

namespace TD.Managers
{
    public class TurretsManager : MonoBehaviour
    {
        [Inject]
        private IEnemiesManager _enemiesManager;

        private List<ITurretBase> turrets;

        private void Awake()
        {
            UpdateTurrets();
        }

        private void UpdateTurrets()
        {
            turrets = FindObjectsOfType<MonoBehaviour>().OfType<ITurretBase>().ToList();
        }

        private void Update()
        {
            CalculateTargetsJob();
        }

        private void CalculateTargetsJob()
        {
            List<Transform> activeEnemies = _enemiesManager.GetSpawnedEnemies();

            if (activeEnemies.Count == 0) return;

            NativeArray<JobHandle> handles = new NativeArray<JobHandle>(turrets.Count, Allocator.Temp);

            NativeArray<int> jobResults = new NativeArray<int>(turrets.Count, Allocator.TempJob);

            for (int i = 0; i < turrets.Count; i++)
            {
                NativeArray<float3> enemiesPositions = new NativeArray<float3>(activeEnemies.Count, Allocator.TempJob);

                for (int j = 0; j < activeEnemies.Count; j++)
                    enemiesPositions[i] = activeEnemies[i].position;

                handles[i] = turrets[i].StartCalculateTargetJob(enemiesPositions, jobResults, i);
                enemiesPositions.Dispose();
            }

            JobHandle.CompleteAll(handles);
            
            for (int i = 0; i < turrets.Count; i++)
                turrets[i].SetFireTarget(activeEnemies[jobResults[i]]);

            handles.Dispose();
            jobResults.Dispose();
        }
    }
}
