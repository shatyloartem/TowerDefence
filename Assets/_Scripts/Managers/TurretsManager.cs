using UnityEngine;
using TD.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Jobs;

namespace TD.Managers
{
    public class TurretsManager : MonoBehaviour
    {
        private List<ITurretBase> turrets;

        private void Awake()
        {
            UpdateTurrets();
        }

        private void UpdateTurrets()
        {
            turrets = FindObjectsOfType<MonoBehaviour>().OfType<ITurretBase>().ToList();
        }

        private void LateUpdate()
        {
            CalculateTargetsJob();
        }

        private void CalculateTargetsJob()
        {
            List<Transform> activeEnemies = EnemiesManager.GetSpawnedEnemies();

            if (activeEnemies.Count == 0) return;

            NativeArray<JobHandle> handles = new NativeArray<JobHandle>(turrets.Count, Allocator.Temp);

            NativeArray<int> jobResults = new NativeArray<int>(turrets.Count, Allocator.TempJob);

            for (int i = 0; i < turrets.Count; i++)
            {
                handles[i] = turrets[i].StartCalculateTargetJob(activeEnemies.Count, jobResults, i, i > 0 ? handles[i-1] : default);
            }

            JobHandle.CompleteAll(handles);

            for (int i = 0; i < turrets.Count; i++)
                turrets[i].SetFireTarget(jobResults[i] == 0 ? null : activeEnemies[jobResults[i]-1]);

            handles.Dispose();
            jobResults.Dispose();
        }
    }
}
