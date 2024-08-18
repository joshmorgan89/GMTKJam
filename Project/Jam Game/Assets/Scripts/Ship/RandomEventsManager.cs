using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventsManager : MonoBehaviourSingleton<RandomEventsManager>
{
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
}
