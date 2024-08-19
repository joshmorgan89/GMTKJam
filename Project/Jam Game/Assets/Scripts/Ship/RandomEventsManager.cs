using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using static Unity.VisualScripting.FlowStateWidget;

public class RandomEventsManager : MonoBehaviourSingleton<RandomEventsManager>
{
    private delegate void EventMethod();
    private List<EventMethod> eventMethods;


    private float eventChance = 1.0f;
    public int popUpCount = 2;
    private int _popUpRemain;

    private float totalGameSecond;

    [Header("Event Spawn Enemies")]
    public float distanceFormShip;
    public GameObject asteroid;
    public int numberOfAsteroid;

    public GameObject alien;
    public int numberOfAlien;

    void Start()
    {
        //list of random event
        eventMethods = new List<EventMethod>
        {
            ShopOutpostEvent,
            EmbassyOutpostEvent,
            AlienEvent,
            AsteroidEvent
        };
    }
    public void InitRandomEvent() {
        totalGameSecond = 1 / UIManager.Instance.gameProgressSpeed;
        _popUpRemain = popUpCount;
        Debug.Log("event trigger time:"+ totalGameSecond / (UIManager.Instance.electionEventNum + 1) / (popUpCount+1));
        StartCoroutine(CheckForRandomEvent());
    }

    private IEnumerator CheckForRandomEvent()
    {
        
        _popUpRemain -= 1;
        yield return new WaitForSeconds(totalGameSecond / (UIManager.Instance.electionEventNum + 1) / (popUpCount + 1));

        //only call random event if there is pop up remain
        if (_popUpRemain > -1)
        {
            if (Random.value < eventChance)
            {
                TriggerRandomEvent();
            }
        }
        else {
            Reset();
        }

        StartCoroutine(CheckForRandomEvent());
    }

    private void TriggerRandomEvent()
    {
        Debug.Log("A random event has been triggered!");
        int randomIndex = Random.Range(0, eventMethods.Count);
        eventMethods[randomIndex]?.Invoke();
    }
    public void ShopOutpostEvent() {
        UIManager.Instance.ShowShopOutpost();
        UpgradeStore.Instance.AssignRandomPerks();
        RoomStore.Instance.AssignRandomRooms();
    }

    public void EmbassyOutpostEvent() {
        UIManager.Instance.ShowEmbassyOutpost();
        UpgradeStore.Instance.AssignRandomPerks();
        Embassy.Instance.ShowTradeRefugees();
    }

    public void AsteroidEvent() {
        SpawnPrefabs(asteroid, numberOfAsteroid);
        RandomEventPopup.Instance.ShowEventPopUp("Asteroid detected! Enemies are emerging!");
    }

    public void AlienEvent() {
        SpawnPrefabs(alien, numberOfAlien);
        RandomEventPopup.Instance.ShowEventPopUp("Alien Ship detected! Enemies are emerging!");
    }

    public void SpawnPrefabs(GameObject enemy, int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = CalculatePosition(i, enemyCount);

            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 CalculatePosition(int index, int enemyCount)
    {
        float angle = index * Mathf.PI * 2 / enemyCount;

        float x = GameManager.Instance.Ship.transform.position.x + Mathf.Cos(angle) * distanceFormShip;
        float z = GameManager.Instance.Ship.transform.position.z + Mathf.Sin(angle) * distanceFormShip;

        return new Vector3(x, GameManager.Instance.Ship.transform.position.y, z);
    }

    public void Reset()
    {
        _popUpRemain = popUpCount;
    }

}
