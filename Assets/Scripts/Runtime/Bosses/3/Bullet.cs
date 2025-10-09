using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private int _damage;

   public void Embark(Vector3 velocity, int damage)
   {
      GetComponent<Rigidbody2D>().linearVelocity =  velocity;
      transform.right = velocity;
      _damage = damage;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.TryGetComponent<PlayerHealth>(out var playerHealth))
      {
         playerHealth.TakeDamage(_damage);
      }
   }
}
