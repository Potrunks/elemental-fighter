using Assets.Script.Data;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Script.Business.Job
{
    [BurstCompile]
    public struct MoveJob : IJob
    {
        public float inputXValue;
        public float speed;
        public float time;
        public float3 velocity;
        public float smoothTime;

        public NativeArray<float2> float2Result;

        Vector3 reference;

        [BurstCompile]
        public void Execute()
        {
            reference = Vector3.zero;
            Vector3 vector3 = Vector3.SmoothDamp(velocity,
                                      new Vector2(inputXValue * speed * time, velocity.y),
                                      ref reference,
                                      smoothTime,
                                      Mathf.Infinity,
                                      time);
            float2Result[0] = new float2(vector3.x, vector3.y);
        }
    }

    [BurstCompile]
    public struct FlipJob : IJobParallelForTransform
    {
        public bool isDeviceUsed;
        public float velocityX;
        public float velocityLowThreshold;
        public float velocityHighThreshold;

        public NativeArray<bool> isLeftFlip;

        [BurstCompile]
        public void Execute(int index, TransformAccess transform)
        {
            if (isDeviceUsed)
            {
                if (velocityX < velocityLowThreshold
                    && !isLeftFlip[index])
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    isLeftFlip[index] = true;
                }
                if (velocityX > velocityHighThreshold
                    && isLeftFlip[index])
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    isLeftFlip[index] = false;
                }
            }
        }
    }
}
