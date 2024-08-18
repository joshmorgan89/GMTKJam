using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomStore : MonoBehaviourSingleton<RoomStore>
{
    public List<SO_RoomItem> roomItems; 
    public Button[] roomButtons; 

    public void AssignRandomRooms()
    {
        for (int i = 0; i < roomButtons.Length; i++)
        {
            SO_RoomItem randomRoomItem = roomItems[Random.Range(0, roomItems.Count)];

            roomButtons[i].gameObject.SetActive(true);
            SetButtonData(roomButtons[i], randomRoomItem);
        }
    }

    private void SetButtonData(Button button, SO_RoomItem roomItem)
    {
        button.GetComponentInChildren<TMP_Text>().text = "Cost: " + roomItem.cost; 
        button.GetComponent<Image>().sprite = roomItem.itemIcon;
        button.GetComponent<TooltipInfo>().description = roomItem.description;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => PurchaseRoom(button,roomItem));
    }

    public void PurchaseRoom(Button button, SO_RoomItem roomItem)
    {
        if (ResourceManager.Instance.rareMineral >= roomItem.cost)
        {
            ResourceManager.Instance.rareMineral -= roomItem.cost;
            button.interactable = false;
            Debug.Log(roomItem.itemName + " purchased! Remaining money: " + ResourceManager.Instance.rareMineral);

            AddRoomToPlayer(roomItem);
        }
        else
        {
            Debug.Log("Not enough money to purchase " + roomItem.itemName);
        }
    }

    private void AddRoomToPlayer(SO_RoomItem roomItem)
    {
        // Implement the logic to add the room to the player's ship or inventory
        // This might involve updating the player's ship layout, inventory, etc.
        Debug.Log(roomItem.itemName + " added to the player's ship.");
    }
}
