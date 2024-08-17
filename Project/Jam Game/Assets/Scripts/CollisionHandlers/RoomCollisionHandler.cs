using Scripts.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCollisionHandler : BaseCollisionHandler
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Health health = GetComponent<Health>();
            BaseEnemyController enemy = collision.gameObject.GetComponent<BaseEnemyController>();
            health.CurrentHealthUpdate(-enemy.CollisionDamage);
            Destroy(collision.gameObject);
        }
    }
}
