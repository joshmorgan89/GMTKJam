using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoBehaviourSingleton<GameManager> {
    [Header("Game Settings")]
    public int InitialRoomCount = 3;
    public List<BaseRoom> RoomPrefabs;
    public Transform RoomsParent;

    [Header("Managers")]
    public GameObject Ship;
    private ShipGrid _shipGrid;

    private List<BaseRoom> _addedRooms = new List<BaseRoom>();
    private int _currentElectionCycle = 1;

    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;

    private void Start() {
        InitializeGame();
    }

    private void InitializeGame() {
        _shipGrid = Ship.GetComponent<ShipGrid>();
        SetupInitialRooms();

        // Initialize UI.
    }

    private void SetupInitialRooms() {
        int section = -1;

        for (int i = 0; i < InitialRoomCount; i++) {
            BaseRoom newRoom;
            if (i == 0) {
                newRoom = Instantiate(RoomPrefabs.FirstOrDefault(x => x.GetComponent<GeneratorRoom>() != null), RoomsParent);
            } else {
                newRoom = Instantiate(RoomPrefabs.ElementAt(Random.Range(0, RoomPrefabs.Count - 1)), RoomsParent); 
            }
            AddRoomAtPosition(newRoom, new Vector3Int(section++, 0));
        }
    }

    public void AddRoomAtPosition(BaseRoom newRoom, Vector3Int cellPosition) {
        if (_shipGrid.IsPositionValid(cellPosition)) {
            if (_shipGrid.AddRoom(newRoom, cellPosition)) {
                _addedRooms.Add(newRoom);

                // Update UI with room count.
            } else {
                Destroy(newRoom);
            }
        }
    }

    public void HandleRoomDestroyed(BaseRoom room) {
        _addedRooms.Remove(room);
        
        // Update UI with room count.

        // Trigger any other logic for when a room is destroyed, such as spawning enemies or triggering events.
        CheckGameOverCondition();
    }

    public void CheckGameOverCondition() {
        if (_addedRooms.Count == 0) {
            // Show Game Over screen.
        }
    }

    public void ProceedToNextElectionCycle() {
        _currentElectionCycle++;
        
        // Update UI

        // Add more rooms, spawn new challenges, etc.
    }

    public void SetZoomLevelOne() {
        virtualCamera.m_Lens.OrthographicSize = 7.5f;
    }

    public void SetZoomLevelTwo() {
        virtualCamera.m_Lens.OrthographicSize = 20;
    }
}
