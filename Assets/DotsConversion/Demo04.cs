using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

public class Demo04 : MonoBehaviour
{
   const int maxCreatures = 200000;
   NativeArray<MonsterData> monsterData;
   struct MonsterData
   {
      public MonsterData(bool b)
      {
         health = 100;
         maxHealth = 100;
         healthRegenRate = 1;
         stamina = 100;
         maxStamina = 100;
         staminaRegenRate = 1;
      }
      public float health;
      public float maxHealth;
      public float healthRegenRate; // per second

      public float stamina;
      public float maxStamina;
      public float staminaRegenRate;
   }


   void Start()
   {
      monsterData = new NativeArray<MonsterData>(maxCreatures, Allocator.Persistent);

      for (int i = 0; i < maxCreatures; ++i)
      {
         monsterData[i] = new MonsterData(true);

      }
   }

   [BurstCompile]
   struct RegenJob : IJobParallelFor
   {
      // Jobs declare all data that will be accessed in the job
      // By declaring it as read only, multiple jobs are allowed to access the data in parallel
      public NativeArray<MonsterData> monsterDatas;
      [ReadOnly] public float deltaTime;
      // The code actually running on the job
      public void Execute(int i)
      {
         var m = monsterDatas[i];
         if (m.health > 0 && m.health < m.maxHealth)
         {
            m.health += m.healthRegenRate * deltaTime;
            if (m.health > m.maxHealth)
               m.health = m.maxHealth;
            monsterDatas[i] = m;
         }
         if (m.stamina > 0 && m.stamina < m.maxStamina)
         {
            m.stamina += m.staminaRegenRate * deltaTime;
            if (m.stamina > m.maxStamina)
               m.stamina = m.maxStamina;
            monsterDatas[i] = m;
         }
      }
   }




   void Update()
   {
      var regenJob = new RegenJob()
      {
         monsterDatas = monsterData,
         deltaTime = Time.deltaTime
      };

      var handle0 = regenJob.Schedule(monsterData.Length, 1000);
      handle0.Complete();
   }
}
