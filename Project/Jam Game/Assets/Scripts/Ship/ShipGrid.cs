using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Grid))]
public class ShipGrid : MonoBehaviour {
    public Grid _grid;

    private List<BaseRoom> _addedRooms = new List<BaseRoom>();

    // Add a room to the grid
    public bool AddRoom(BaseRoom room, Vector3Int cellPosition) {
        if (IsPositionValid(cellPosition)) {
            // Convert the cell position to world position
            Vector3 worldPosition = _grid.CellToWorld(cellPosition);

            // Set the room's position in the world
            room.transform.position = worldPosition;

            // Activate the room
            _addedRooms.Add(room);

            if (IsInRangeOfGeneratorRoom(cellPosition)) {
                room.Activate();
            }

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

    public bool IsInRangeOfGeneratorRoom(Vector3Int cellPosition) {
        return _addedRooms
            .Where(x => {
                Vector3Int xCellPosition = _grid.WorldToCell(x.gameObject.transform.position);
                return (xCellPosition.x - cellPosition.x) + (xCellPosition.y - cellPosition.y) <= 3;
            })
            .ToList()
            .Count > 0;
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
}
