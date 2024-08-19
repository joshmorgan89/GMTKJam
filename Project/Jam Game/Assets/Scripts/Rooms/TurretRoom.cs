using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRoom : BaseRoom
{
    public GameObject Turret { get; set; }

    public override void Activate() {
        base.Activate();
        Turret.SetActive(true);
        // Additional generator activation logic
        Debug.Log($"{gameObject.name} is now scanning for enemies.");
    }

    // Override Deactivate to handle generator-specific logic
    public override void Deactivate() {
        base.Deactivate();
        Turret.SetActive(false);
        // Additional generator deactivation logic
        Debug.Log($"{gameObject.name} lost the ability to shoot.");
    }
}
