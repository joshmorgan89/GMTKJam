using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float attackingRange;
    public float attackingCooldown;

    private float timer;

    public GameObject bulletPrefab;
    public GameObject[] Enemies;
    public GameObject turret;

    public Transform ShootingPoint;
    public void Update()
    {
        timer += Time.deltaTime;
    }

    public void FixedUpdate()
    {
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
        if (closestEnemy != null )
        {
            //calculate the direction angle
            Vector2 direction = (closestEnemy.transform.position - ShootingPoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //rotate the turret
            turret.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

            //check if attacking cooldown is over
            if (timer > attackingCooldown) {
                ShootBullet(angle);
                timer -= attackingCooldown;
            }
        }
    }


    /// <summary>
    /// Shoot bullet towards enemy detected
    /// </summary>
    /// <param name="direction">the direction of the bullet</param>
    private void ShootBullet(float angle) {
        
        GameObject bullet = Instantiate(bulletPrefab, ShootingPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
    }
}
