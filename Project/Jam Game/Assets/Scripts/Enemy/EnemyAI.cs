using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float shootingCooldown = 1f;

    private EnemyMovement _enemyMovement;
    private float _timer;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (_timer <= shootingCooldown) {
            _timer += Time.deltaTime;
        }

        if (_enemyMovement.IsInRange() && _timer >= shootingCooldown)
        {
            ShootBullet();
            _timer -= shootingCooldown;
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab != null && shootingPoint != null)
        {
            Vector2 direction = (_enemyMovement.GetTarget().position - shootingPoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.Euler(new Vector3(0,0,angle)));
        }
    }
}
