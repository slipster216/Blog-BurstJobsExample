using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

public class Demo05 : MonoBehaviour
{
   const int maxCreatures = 200000;
   NativeArray<RegenStat> healthStat;
   NativeArray<RegenStat> staminaStat;

   struct RegenStat
   {
      public RegenStat(bool b)
      {
         stat = 100;
         maxValue = 100;
         regenRate = 1;
      }
      public float stat;
      public float maxValue;
      public float regenRate; // per second
   }


   void Start()
   {
      healthStat = new NativeArray<RegenStat>(maxCreatures, Allocator.Persistent);
      staminaStat = new NativeArray<RegenStat>(maxCreatures, Allocator.Persistent);

      for (int i = 0; i < maxCreatures; ++i)
      {
         healthStat[i] = new RegenStat(true);
         staminaStat[i] = new RegenStat(true);
      }
   }

   [BurstCompile]
   struct RegenJob : IJobParallelFor
   {
      // Jobs declare all data that will be accessed in the job
      // By declaring it as read only, multiple jobs are allowed to access the data in parallel
      public NativeArray<RegenStat> datas;
      [ReadOnly] public float deltaTime;
      // The code actually running on the job
      public void Execute(int i)
      {
         var m = datas[i];
         if (m.stat > 0 && m.stat < m.maxValue)
         {
            m.stat += m.regenRate * deltaTime;
            if (m.stat > m.maxValue)
               m.stat = m.maxValue;
            datas[i] = m;
         }
      }
   }




   void Update()
   {
      var healthJob = new RegenJob()
      {
         datas = healthStat,
         deltaTime = Time.deltaTime
      };

      var staminaJob = new RegenJob()
      {
         datas = staminaStat,
         deltaTime = Time.deltaTime
      };

      var handle0 = healthJob.Schedule(healthStat.Length, 1000);
      var handle1 = staminaJob.Schedule(staminaStat.Length, 1000);
      handle0.Complete();
      handle1.Complete();
   }
}
