using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Destroy this enemy if it is colliding with the player's projectile
    /// </summary>
    /// <param name="objectCollided"></param>
    /// 
    public GameObject[] itemPrefabs;
    public float dropChance = 0.5f;
    private void Collision(Collider2D objectCollided)
    {
        if (objectCollided.GetComponent<StateController>() != null)
        {
            Destroy(objectCollided.gameObject);
           // Die();
        }
    }
    //private void Die()
    //{
    //    if (Random.value < dropChance)
    //    {
    //        int randomIndex = Random.Range(0, itemPrefabs.Length);
    //        Instantiate(itemPrefabs[randomIndex], transform.position, Quaternion.identity);
    //        Debug.Log("Item dropped!");
    //    }

    //}
    private void OnEnable()
    {
        ProjectilePooler.OnProjectileCollision += Collision;
    }

    private void OnDisable()
    {
        ProjectilePooler.OnProjectileCollision -= Collision;
    }

}
