using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float attackingRange;
    public float attackingCooldown;

    private float timer;

    public GameObject bulletPrefab;
    public GameObject[] Enemies;


    public void Update()
    {
        timer += Time.deltaTime;
    }

    public void FixedUpdate()
    {
        //check if attacking cooldown is over
        if (timer > attackingCooldown) {
            
            //find enemy in the scene(could use object pool to save performance later) and init the variables needed
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float closestEnemyDistance = float.MaxValue;
            GameObject closestEnemy = null;
            float currentEnemyDistance;

            //iterate through all the enemy to find the closest enemy
            foreach (GameObject enemy in Enemies)
            {
                currentEnemyDistance = Vector2.Distance(enemy.transform.position, transform.position);

                if (currentEnemyDistance < attackingRange)
                {
                    if (currentEnemyDistance < closestEnemyDistance)
                    {
                        closestEnemyDistance = currentEnemyDistance;
                        closestEnemy = enemy;
                    }
                }
            }

            //if there is enemy found in range shoot bullet toward that enemy
            if (closestEnemy != null)
            {
                ShootBullet((closestEnemy.transform.position - transform.position).normalized);
                timer -= attackingCooldown;
            }
        }
        
    }


    /// <summary>
    /// Shoot bullet towards enemy detected
    /// </summary>
    /// <param name="direction">the direction of the bullet</param>
    private void ShootBullet(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
