using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Grid))]
public class ShipGrid : MonoBehaviour {
    public Grid _grid;

    private List<BaseRoom> _addedRooms = new List<BaseRoom>();
    private int _totalPowerGenerated = 0;
    private int _totalPowerRequired = 0;

    // Add a room to the grid
    public bool AddRoom(BaseRoom room, Vector3Int cellPosition) {
        if (IsPositionValid(cellPosition)) {
            // Convert the cell position to world position
            Vector3 worldPosition = _grid.CellToWorld(cellPosition);

            // Set the room's position in the world
            room.transform.position = worldPosition;

            // Activate the room
            _addedRooms.Add(room);
            room.Activate();

            UpdatePowerUsage();
            return true;
        }
        return false;
    }

    // Remove a room from the grid
    public void RemoveRoom(Vector3Int cellPosition) {
        BaseRoom room = GetRoomAtPosition(cellPosition);
        if (room != null) {
            room.Deactivate();
            _addedRooms.Remove(room);
            UpdatePowerUsage();
        }
    }

    // Check if a position is within the grid bounds
    public bool IsPositionValid(Vector3Int cellPosition) {
        // Convert cell position to world position to compare with room positions
        Vector3 worldPosition = _grid.CellToWorld(cellPosition);

        // Check if the position is already occupied by an active room
        if (GetRoomAtPosition(cellPosition) != null) {
            Debug.Log("Position is already occupied by another room.");
            return false;
        }

        // Check if the position is adjacent to an existing active room (if adjacency rules apply)
        if (!IsAdjacentToExistingRoom(worldPosition) && _addedRooms.Count > 0) {
            Debug.Log("Position is not adjacent to an existing room.");
            return false;
        }

        // If all checks pass, the position is valid
        return true;
    }

    private bool IsAdjacentToExistingRoom(Vector3 worldPosition) {
        // Offsets to check all four adjacent positions (up, down, left, right)
        Vector3Int[] adjacentOffsets = new Vector3Int[] {
            new Vector3Int(0, 1, 0),  // Up
            new Vector3Int(0, -1, 0), // Down
            new Vector3Int(1, 0, 0),  // Right
            new Vector3Int(-1, 0, 0)  // Left
        };

        // Check each adjacent position
        foreach (var offset in adjacentOffsets) {
            Vector3 adjacentPosition = _grid.CellToWorld(_grid.WorldToCell(worldPosition) + offset);

            // Iterate through active rooms to see if any are at the adjacent position
            foreach (BaseRoom room in _addedRooms) {
                if (room.transform.position == adjacentPosition) {
                    return true; // Found an adjacent room
                }
            }
        }

        // No adjacent room found
        return false;
    }

    // Get the room at a specific grid position
    public BaseRoom GetRoomAtPosition(Vector3Int cellPosition) {
        Vector3 worldPosition = _grid.CellToWorld(cellPosition);
        foreach (BaseRoom room in _addedRooms) {
            if (room.transform.position == worldPosition) {
                return room;
            }
        }
        return null;
    }

    // Update power usage across all rooms
    private void UpdatePowerUsage() {
        _totalPowerGenerated = 0;
        _totalPowerRequired = 0;

        foreach (BaseRoom room in _addedRooms) {
            if (room is GeneratorRoom generatorRoom) {
                _totalPowerGenerated += generatorRoom.CurrentPowerOutput;
            }
            _totalPowerRequired += room.PowerRequired;
        }

        // Check if the power balance is correct
        if (_totalPowerGenerated >= _totalPowerRequired) {
            Debug.Log("Power is sufficient.");
        } else {
            Debug.Log("Not enough power. Some rooms may be deactivated.");
            // Deactivate rooms in order of priority
            DeactivateRoomsForPowerBalance();
        }
    }

    // Deactivate rooms to balance power
    private void DeactivateRoomsForPowerBalance() {
        int deficit = _totalPowerRequired - _totalPowerGenerated;

        foreach (BaseRoom room in _addedRooms) {
            if (deficit <= 0) { break; }
            if (room is GeneratorRoom) { continue; }
            room.Deactivate();
            deficit -= room.PowerRequired;
        }
    }
}
