using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorRoom : BaseRoom {
    // Power output for this generator room
    public int MaxPowerOutput = 50;

    public int CurrentPowerOutput { get; private set; }

    // Override Activate to include generator-specific logic
    public override void Activate() {
        base.Activate();
        CurrentPowerOutput = MaxPowerOutput;
        // Additional generator activation logic
        Debug.Log($"{gameObject.name} is now generating {CurrentPowerOutput} power.");
    }

    // Override Deactivate to handle generator-specific logic
    public override void Deactivate() {
        base.Deactivate();
        // Additional generator deactivation logic
        CurrentPowerOutput = 0;
        Debug.Log($"{gameObject.name} stopped generating power.");
    }

    // Optional: Place generator on grid (if specific placement logic is needed)
    public override void PlaceOnGrid(Vector3Int position) {
        base.PlaceOnGrid(position);
        // Additional placement logic, if any
    }
}
