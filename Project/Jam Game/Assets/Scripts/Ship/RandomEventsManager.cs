using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class RandomEventsManager : MonoBehaviourSingleton<RandomEventsManager>
{
    private float eventChance = 1.0f;
    public int popUpCount = 2;
    private int _popUpRemain;

    public float totalGameSecond;

    public void InitRandomEvent() {
        totalGameSecond = 1 / UIManager.Instance.gameProgressSpeed;
        _popUpRemain = popUpCount;
        Debug.Log(totalGameSecond);
        Debug.Log((UIManager.Instance.electionEventNum) * (popUpCount + 1));
        Debug.Log(totalGameSecond / (UIManager.Instance.electionEventNum) / (popUpCount+1));
        StartCoroutine(CheckForRandomEvent());
    }

    private IEnumerator CheckForRandomEvent()
    {
        _popUpRemain -= 1;
        yield return new WaitForSeconds(totalGameSecond / (UIManager.Instance.electionEventNum + 1) / popUpCount);

        if (_popUpRemain > 0)
        {
            if (Random.value < eventChance)
            {
                TriggerRandomEvent();
            }
            StartCoroutine(CheckForRandomEvent());
        }
    }

    private void TriggerRandomEvent()
    {
        Debug.Log("A random event has been triggered!");
        // Here you would trigger the actual event, e.g., showing a popup or starting a special sequence.
        RandomEventPopup randomEventPopup = FindObjectOfType<RandomEventPopup>();
        if (randomEventPopup != null)
        {
            randomEventPopup.ShowEventPopUp("Pirates en route to the ship's starboard side!");
        }
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

    public void Reset()
    {
        _popUpRemain = popUpCount;
    }

}
