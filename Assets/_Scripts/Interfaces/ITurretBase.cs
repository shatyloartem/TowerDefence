using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace TD.Interfaces
{
    public interface ITurretBase
    {
        public void SetFireTarget(Transform target);

        public JobHandle StartCalculateTargetJob(int enemiesCount, NativeArray<int> jobResult, int jobIndex, JobHandle dependentJob);
    }
}
