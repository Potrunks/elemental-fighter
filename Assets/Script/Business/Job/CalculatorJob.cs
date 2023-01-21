using System.Numerics;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;
using static Pathfinding.SimpleSmoothModifier;

namespace Assets.Script.Business.Job
{
    [BurstCompile]
    public struct MultiplicatorJob : IJob
    {
        public NativeArray<float> floatListToMultiplicate;
        public NativeArray<float> resultMultiplicator;

        [BurstCompile]
        public void Execute()
        {
            resultMultiplicator[0] = 1;
            foreach(float value in floatListToMultiplicate)
            {
                resultMultiplicator[0] *= value;
            }
        }
    }
}
