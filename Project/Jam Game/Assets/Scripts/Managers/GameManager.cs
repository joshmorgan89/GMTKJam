using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviourSingleton<GameManager> {
    [Header("Game Settings")]
    public List<BaseRoom> InitialRoomsPrefabs;
    public List<BaseRoom> RoomPrefabs;
    public Transform ShipTransform;
    public int MaxPopulationCount = 10;
    public int CrewQuartersMod = 5;
    public int GeneratorChargeRange = 3;

    private float _sessionTimer = 0.0f;
    public float SessionTimer => _sessionTimer;

    [Header("Managers")]
    public InteractionHandler PlayerPodInteraction;
    public GameObject Ship;
    private ShipGrid _shipGrid;

    [Header("Camera")]
    public CinemachineVirtualCamera virtualCamera;

    private int _currentElectionCycle = 1;
    private int _currentPopulationCount = 0;

    private void Start() {
        _shipGrid = Ship.GetComponent<ShipGrid>();
        InitializeGame();
    }

    private void Update() {
        _sessionTimer += Time.deltaTime;
    }

    private void InitializeGame() {
        _sessionTimer = 0;
        SetupInitialRooms();

        // Initialize UI.
    }

    public void ToggleGamePause() {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }
    public void StopGameTimeScale()
    {
        Time.timeScale = 0;
    }
    public void StartGameTimeScale()
    {
        Time.timeScale = 1;
    }

    private void SetupInitialRooms() {
        int section = -1;

        for (int i = 0; i < InitialRoomsPrefabs.Count; i++) {
            BaseRoom newRoom = Instantiate(InitialRoomsPrefabs[i], ShipTransform);
            AddRoomAtPosition(new Vector3Int(section++, 0), newRoom);
        }
    }

    public void AddRoomAtPosition(Vector3Int cellPosition, BaseRoom newRoom) {
        if (_shipGrid.AddRoom(cellPosition, newRoom)) {
            // Update UI with room count.
        } else {
            Destroy(newRoom);
        }
    }

    public bool GenerateRoomClusterAtPosition(Vector3 worldPosition, int numberOfRooms) {
        Vector3Int cellPosition = _shipGrid.Grid.WorldToCell(worldPosition);

        if (_shipGrid.IsCloseEnough(cellPosition)) {
            List<BaseRoom> roomsToAdd = new List<BaseRoom>();

            for (int i = 0; i < numberOfRooms; i++) {
                roomsToAdd.Add(Instantiate(RoomPrefabs[Random.Range(0,RoomPrefabs.Count - 1)], ShipTransform));
            }

            _shipGrid.SpawnRoomsAroundCellPosition(cellPosition, roomsToAdd);

            return true;
        }

        return false;
    }

    public void HandleRoomDestroyed(Vector3Int cellPosition) {
        _shipGrid.RemoveRoom(cellPosition);
        
        // Update UI with room count.

        // Trigger any other logic for when a room is destroyed, such as spawning enemies or triggering events.
        CheckGameOverCondition();
    }

    public bool IsPodOutsideCircle(Vector3 circleCenter, float radius) {
        return PlayerPodInteraction.IsPodOutsideCircle(circleCenter, radius);
    }

    public void MovePodOutsideShipBounds(Vector3 circleCenter, float radius) {
        PlayerPodInteraction.MovePodOutsideShipBounds(circleCenter, radius);
    }

    public Transform GetPodTransform() {
        return PlayerPodInteraction.PodTransform;
    }

    public Vector3Int GetRoomPosition(BaseRoom room) {
        return _shipGrid.GetRoomPosition(room);
    }

    public void RemoveRoom(Vector3Int cellPosition) {
        _shipGrid.RemoveRoom(cellPosition);   
    }

    public void CheckGameOverCondition() {
        if (_shipGrid.RoomCount == 0) {
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

    public void CrewQuartersActivated() {
        MaxPopulationCount += CrewQuartersMod;
    }

    public void CrewQuartersDeactivated() {
        MaxPopulationCount -= CrewQuartersMod;
    }

    public void ActivatedGeneratorRoom(GeneratorRoom generatorRoom) {
        _shipGrid.ActivatedGeneratorRoom(generatorRoom);
    }

    public void DeactivatedGeneratorRoom(GeneratorRoom generatorRoom) {
        _shipGrid.DeactivatedGeneratorRoom(generatorRoom);
    }
}
