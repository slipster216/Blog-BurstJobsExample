using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

public class Demo06 : MonoBehaviour
{
   const int maxCreatures = 200000;
   NativeArray<float> healthStat;
   NativeArray<float> staminaStat;


   void Start()
   {
      healthStat = new NativeArray<float>(maxCreatures, Allocator.Persistent);
      staminaStat = new NativeArray<float>(maxCreatures, Allocator.Persistent);

      for (int i = 0; i < maxCreatures; ++i)
      {
         healthStat[i] = 100;
         staminaStat[i] = 100;
      }
   }

   [BurstCompile]
   struct RegenJob : IJobParallelFor
   {
      // Jobs declare all data that will be accessed in the job
      // By declaring it as read only, multiple jobs are allowed to access the data in parallel
      public NativeArray<float> stats;
      [ReadOnly] public float deltaTime;
      [ReadOnly] public float maxValue;
      [ReadOnly] public float regenRate;
      // The code actually running on the job
      public void Execute(int i)
      {
         var m = stats[i];
         if (m > 0 && m < maxValue)
         {
            m += regenRate * deltaTime;
            if (m > maxValue)
               m = maxValue;
            stats[i] = m;
         }
      }
   }




   void Update()
   {
      var healthJob = new RegenJob()
      {
         stats = healthStat,
         deltaTime = Time.deltaTime
      };

      var staminaJob = new RegenJob()
      {
         stats = staminaStat,
         deltaTime = Time.deltaTime
      };

      var handle0 = healthJob.Schedule(healthStat.Length, 1000);
      var handle1 = staminaJob.Schedule(staminaStat.Length, 1000);
      handle0.Complete();
      handle1.Complete();
   }
}
