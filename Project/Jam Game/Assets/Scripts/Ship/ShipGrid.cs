using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[RequireComponent(typeof(Grid))]
public class ShipGrid : MonoBehaviour {
    public Grid Grid;
    public TileBase RoomTile;
    public TileBase GeneratorRoomTile;
    public Tilemap RoomTileMap;
    public Tilemap SpecialRoomTileMap;

    private Dictionary<Vector3Int, BaseRoom> _addedRooms = new Dictionary<Vector3Int, BaseRoom>();

    public int RoomCount => _addedRooms.Count;

    // Add a room to the grid
    public bool AddRoom(Vector3Int cellPosition, BaseRoom room) {
        if (IsPositionValid(cellPosition)) {
            // Set the tile on the tilemap
            RoomTileMap.SetTile(cellPosition, RoomTile);

            // Set the tile on the special tilemap if it's one of our special rooms
            if (room is GeneratorRoom generatorRoom) {
                SpecialRoomTileMap.SetTile(cellPosition, GeneratorRoomTile);
            }

            // Activate the room
            if (IsInRangeOfGeneratorRoom(cellPosition)) {
                room.Activate();
            }
            
            if (GetRoomAtPosition(cellPosition) == null) {
                _addedRooms.Add(cellPosition, room);
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
            _addedRooms.Remove(cellPosition);
        }
    }

    // Check if a position is within the grid bounds
    public bool IsPositionValid(Vector3Int cellPosition) {
        // Convert cell position to world position to compare with room positions
        Vector3 worldPosition = Grid.CellToWorld(cellPosition);

        // Check if the position is already occupied by an active room
        if (GetRoomAtPosition(cellPosition) != null) {
            Debug.Log("Position is already occupied by another room.");
            return false;
        }

        // Check if the position is adjacent to an existing active room (if adjacency rules apply)
        if (RoomCount > 0 && !IsAdjacentToExistingRoom(cellPosition)) {
            Debug.Log("Position is not adjacent to an existing room.");
            return false;
        }

        // If all checks pass, the position is valid
        return true;
    }

    public BaseRoom GetRoomAtPosition(Vector3Int cellPosition) {
        return _addedRooms.FirstOrDefault(x => x.Key == cellPosition).Value;
    }

    public bool IsInRangeOfGeneratorRoom(Vector3Int cellPosition) {
        return !_addedRooms
            .Where(x => (x.Key.x - cellPosition.x) + (x.Key.y - cellPosition.y) <= 3)       // First get cells within charge distance
            .FirstOrDefault(x => x.Value is GeneratorRoom)                                  // Then see if any of those cells are generators
            .Equals(default(KeyValuePair<Vector3Int, BaseRoom>));                                                       // Finally we check if the FirstOrDefault is default
    }

    public bool IsAdjacentToExistingRoom(Vector3 cellPosition) {
        // Offsets to check all four adjacent positions (up, down, left, right)
        Vector3Int[] adjacentOffsets = new Vector3Int[] {
            new Vector3Int(0, 1, 0),  // Up
            new Vector3Int(0, -1, 0), // Down
            new Vector3Int(1, 0, 0),  // Right
            new Vector3Int(-1, 0, 0)  // Left
        };

        // Check each adjacent position
        foreach (var offset in adjacentOffsets) {
            Vector3 adjacentPosition = cellPosition + offset;

            if (_addedRooms.FirstOrDefault(x => x.Key == adjacentPosition).Value != null) {
                return true; // Found an adjacent room
            }
        }

        // No adjacent room found
        return false;
    }
}
