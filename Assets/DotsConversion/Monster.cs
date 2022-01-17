using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   public float health = 100;
   public float maxHealth = 100;
   public float healthRegenRate = 1; // per second
 
   public float stamina = 100;
   public float maxStamina = 100;
   public float staminaRegenRate = 1;

   public void Update()
   {
      if (health > 0 && health < maxHealth)
      {
         health += healthRegenRate * Time.deltaTime;
         if (health > maxHealth)
            health = maxHealth;
      }
      if (stamina > 0 && stamina < maxStamina)
      {
         stamina += staminaRegenRate * Time.deltaTime;
         if (stamina > maxStamina)
            stamina = maxStamina;
      }
   }

   public void Poll()
   {
      if (health > 0 && health < maxHealth)
      {
         health += healthRegenRate * Time.deltaTime;
         if (health > maxHealth)
            health = maxHealth;
      }
      if (stamina > 0 && stamina < maxStamina)
      {
         stamina += staminaRegenRate * Time.deltaTime;
         if (stamina > maxStamina)
            stamina = maxStamina;
      }
   }
}
