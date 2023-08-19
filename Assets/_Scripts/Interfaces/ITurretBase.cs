using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

namespace TD.Interfaces
{
    public interface ITurretBase
    {
        public void SetFireTarget(Transform target);

        public JobHandle StartCalculateTargetJob(NativeArray<float3> enemiesPositions, NativeArray<int> jobResult, int jobIndex);
    }
}
