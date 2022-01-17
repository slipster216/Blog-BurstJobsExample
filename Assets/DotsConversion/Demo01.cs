using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// the game object way. Each creature is a game object with a component on
// it with the data which updates itself
public class Demo01 : MonoBehaviour
{
   const int maxCreatures = 200000;


   void Start()
   {
      for (int i = 0; i < maxCreatures; ++i)
      {
         GameObject go = new GameObject(i.ToString());
         go.AddComponent<Monster>();
      }

   }

}
