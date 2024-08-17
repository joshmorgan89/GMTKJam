using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseRoom : MonoBehaviour
{
    public int PowerRequired = 10;
    protected ShipGrid _shipGrid;
    private Scripts.Shared.Health _health;

    public bool IsActive { get; private set; }

    private void Awake() {
        _health = GetComponent<Scripts.Shared.Health>();
    }

    private void Update() {
        if (_health.IsDestroyed) {
            Deactivate();
        }
    }

    // Called when the room is added to the grid and activated
    public virtual void Activate() {
        IsActive = true;
        // Additional activation logic (e.g., turn on lights, enable room functionality)
        Debug.Log($"{gameObject.name} activated.");
    }

    // Called when the room is removed from the grid or deactivated
    public virtual void Deactivate() {
        IsActive = false;
        // Additional deactivation logic (e.g., turn off lights, disable room functionality)
        Debug.Log($"{gameObject.name} deactivated.");
    }

    // Method to handle any room-specific behavior (e.g., could be overridden by derived classes)
    public virtual void RoomUpdate() { }

    // Method to repair a damaged room
    public virtual void Repair() {
        _health.CurrentHealthUpdate(_health.MaxHealth);
        Debug.Log($"{gameObject.name} repaired.");
        // Repair logic (e.g., restore functionality, reset damage counters)
    }

    // Place the room on the grid (optional in base class, to be implemented in derived classes)
    public virtual void PlaceOnGrid(Vector3Int position) {
        if (_shipGrid.IsPositionValid(position)) {
            _shipGrid.AddRoom(this, position);
            Debug.Log($"{gameObject.name} placed on grid at {position}.");
        } else {
            Debug.Log("Invalid grid position.");
        }
    }
}
