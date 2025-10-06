using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public void Embark(Vector3 velocity)
   {
      GetComponent<Rigidbody2D>().linearVelocity =  velocity;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      
   }
}
