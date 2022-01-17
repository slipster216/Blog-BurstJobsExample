using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo03 : MonoBehaviour
{
   const int maxCreatures = 200000;

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

      public void Update(float deltaTime)
      {
         if (health > 0 && health < maxHealth)
         {
            health += healthRegenRate * deltaTime;
            if (health > maxHealth)
               health = maxHealth;
         }
         if (stamina > 0 && stamina < maxStamina)
         {
            stamina += staminaRegenRate * deltaTime;
            if (stamina > maxStamina)
               stamina = maxStamina;
         }
      }
   }

   MonsterData[] data = new MonsterData[maxCreatures];

   void Start()
   {
      for (int i = 0; i < maxCreatures; ++i)
      {
         data[i] = new MonsterData(true);
      }
   }

   void Update()
   {
      float dt = Time.deltaTime;
      for (int i = 0; i < maxCreatures; ++i)
      {
         data[i].Update(dt);
      }
   }
}
