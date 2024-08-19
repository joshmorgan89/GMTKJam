using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Grid))]
public class ShipGrid : MonoBehaviour {
    public Grid Grid;
    public TileBase RoomTile;
    public TileBase GeneratorRoomTile;
    public TileBase CrewQuartersTile;
    public Tilemap RoomTileMap;
    public Tilemap SpecialRoomTileMap;
    public GameObject TurretPrefab;

    private Dictionary<Vector3Int, BaseRoom> _addedRooms = new Dictionary<Vector3Int, BaseRoom>();

    public int RoomCount => _addedRooms.Count;

    // Add a room to the grid
    public bool AddRoom(Vector3Int cellPosition, BaseRoom room) {
        if (IsPositionValid(cellPosition)) {
            // Set the tile on the tilemap
            RoomTileMap.SetTile(cellPosition, RoomTile);

            // Set the tile on the special tilemap if it's one of our special rooms
            if (room is GeneratorRoom) {
                SpecialRoomTileMap.SetTile(cellPosition, GeneratorRoomTile);
            } else if (room is TurretRoom turretRoom) {
                turretRoom.Turret = Instantiate(TurretPrefab, Grid.GetCellCenterWorld(cellPosition), room.transform.rotation);
            } else if (room is CrewQuarters) {
                SpecialRoomTileMap.SetTile(cellPosition, CrewQuartersTile);
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

            // Tell the cell to stop rendering the tile(s)
            RoomTileMap.SetTile(cellPosition, null);

            // Set the tile on the special tilemap if it's one of our special rooms
            if (room is GeneratorRoom || room is CrewQuarters) {
                SpecialRoomTileMap.SetTile(cellPosition, null);
            } else if (room is TurretRoom turretRoom) {
                Destroy(turretRoom.Turret);
            }
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

    public List<BaseRoom> GetRoomsInRangeOfCellPosition(int range, Vector3Int cellPosition) {
        Dictionary<Vector3Int, BaseRoom> roomsInRange = _addedRooms.Where(x => (x.Key.x - cellPosition.x) + (x.Key.y - cellPosition.y) <= range) as Dictionary<Vector3Int, BaseRoom>;
        return roomsInRange.Values.ToList();
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

    public void SpawnRoomsAroundCellPosition(Vector3Int cellPosition, List<BaseRoom> roomsToSpawn) {
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        // Add the initial position to the queue and mark it as visited
        queue.Enqueue(cellPosition);
        visited.Add(cellPosition);

        // Adjacent offsets (up, down, right, left)
        Vector3Int[] adjacentOffsets = new Vector3Int[] {
            new Vector3Int(0, 1, 0),  // Up
            new Vector3Int(0, -1, 0), // Down
            new Vector3Int(1, 0, 0),  // Right
            new Vector3Int(-1, 0, 0)  // Left
        };

        // Process the queue until all rooms are spawned or no more positions available
        while (queue.Count > 0 && roomsToSpawn.Count > 0) {
            Vector3Int currentCell = queue.Dequeue();

            bool roomPlaced = false;

            foreach (var offset in adjacentOffsets) {
                Vector3Int closestAvailablePosition = FindClosestAvailablePosition(currentCell, adjacentOffsets);
                if (closestAvailablePosition != null) {
                    if (AddRoom(closestAvailablePosition, roomsToSpawn[0])) {
                        roomsToSpawn.RemoveAt(0);  // Remove the room from the list once placed
                        visited.Add(closestAvailablePosition);
                        queue.Enqueue(closestAvailablePosition);
                    }
                }

                if (roomsToSpawn.Count == 0) {
                    break;
                }
            }
        }

        // Push the player outside the new ship bounds
        EnsurePodIsOutsideShipBoundingCircle();

        // If roomsToSpawn is not empty, it means not all rooms could be placed, handle accordingly
        if (roomsToSpawn.Count > 0) {
            Debug.LogWarning("Not all rooms could be placed.");
        }
    }

    private Vector3Int FindClosestAvailablePosition(Vector3Int startPosition, Vector3Int[] offsets) {
        Queue<Vector3Int> searchQueue = new Queue<Vector3Int>();
        HashSet<Vector3Int> searched = new HashSet<Vector3Int>();

        searchQueue.Enqueue(startPosition);
        searched.Add(startPosition);

        while (searchQueue.Count > 0) {
            Vector3Int current = searchQueue.Dequeue();

            foreach (var offset in offsets) {
                Vector3Int neighbor = current + offset;

                //// We want to build out and not fill spaces
                //Vector3 directionTowardShip = startPosition - neighbor;
                //float dot = Vector3.Dot(directionTowardShip.normalized, offset);

                //if (dot > 0) {
                //    continue;
                //}

                if (!searched.Contains(neighbor) && IsPositionValid(neighbor)) {
                    return neighbor;
                }

                if (!searched.Contains(neighbor)) {
                    searched.Add(neighbor);
                    searchQueue.Enqueue(neighbor);
                }
            }
        }

        return Vector3Int.zero;
    }

    private (Vector3 center, float radius) CalculateBoundingCircle(List<Vector3Int> roomPositions) {
        if (roomPositions.Count == 0)
            return (Vector3.zero, 0f);

        Vector3 sum = Vector3.zero;
        foreach (var pos in roomPositions) {
            sum += Grid.CellToWorld(pos);
        }
        Vector3 center = sum / roomPositions.Count;

        float radius = 0f;
        foreach (var pos in roomPositions) {
            float distance = Vector3.Distance(center, Grid.CellToWorld(pos));
            if (distance > radius) {
                radius = distance;
            }
        }

        return (center, radius);
    }

    public void EnsurePodIsOutsideShipBoundingCircle() {
        // Calculate bounding circle of the newly spawned rooms
        var (center, radius) = CalculateBoundingCircle(_addedRooms.Keys.ToList());

        // Check if the pod is outside the circle
        Vector3 podPosition = GameManager.Instance.GetPodTransform().position;

        if (!GameManager.Instance.IsPodOutsideCircle(center, radius)) {
            // Adjust pod position if it's inside the circle
            GameManager.Instance.MovePodOutsideShipBounds(center, radius);
        }
    }

    public bool IsPositionInsideOfARoom(Vector3 worldPosition) {
        Vector3Int cellPosition = Grid.WorldToCell(worldPosition);
        return _addedRooms.ContainsKey(cellPosition) && _addedRooms[cellPosition] != null;
    }

    public void OnActivatedGeneratorRoom(GeneratorRoom generatorRoom) {
        Vector3Int cellPosition = _addedRooms.FirstOrDefault(x => x.Value == generatorRoom).Key;

        foreach (var room in GetRoomsInRangeOfCellPosition(3, cellPosition)) {
            room.Activate();
        }
    }

    public void OnDeactivatedGeneratorRoom(GeneratorRoom generatorRoom) {
        Vector3Int cellPosition = _addedRooms.FirstOrDefault(x => x.Value == generatorRoom).Key;

        foreach (var room in GetRoomsInRangeOfCellPosition(3, cellPosition)) {
            room.Deactivate();
        }
    }
}
