using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Demo02 : MonoBehaviour
{
   public bool randomAccess = false;
   const int maxCreatures = 200000;

   Monster[] monsters = new Monster[maxCreatures];

   void Start()
   {
      List<Monster> data = new List<Monster>();
      for (int i = 0; i < maxCreatures; ++i)
      {
         GameObject go = new GameObject(i.ToString());
         var m = go.AddComponent<Monster>();
         data.Add(m);
         m.enabled = false; // disable so we don't call update!
      }
      monsters = data.ToArray();
      if (randomAccess)
      {
         var rnd = new System.Random();
         var randomized = data.OrderBy(item => rnd.Next());
         monsters = randomized.ToArray();
      }
      

   }

   void Update()
   {
      for (int i = 0; i < maxCreatures; ++i)
      {
         monsters[i].Poll();
      }
   }
}
