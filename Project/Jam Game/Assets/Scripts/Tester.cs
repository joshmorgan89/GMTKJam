using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tester : MonoBehaviourSingleton<Tester>
{
    public TMP_Text fuelText;

    public float timer;
    public float goodwillNum;
    public GameObject asteroid;
    private void Update()
    {
        goodwillNum += Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= 2) { 
            Instantiate(asteroid, gameObject.transform.position, Quaternion.Euler(new Vector3(0,0, Random.Range(0f, 360f))));
            timer -= 2;
        }

        HUD.Instance.UpdateGoodwill(100, goodwillNum);
        HUD.Instance.UpdateRareMineral();
        HUD.Instance.UpdateRefugee();
        //refresh shop
        if (Input.GetKeyDown("o")) {
            RandomEventsManager.Instance.ShopOutpostEvent();
        }
        if (Input.GetKeyDown("p")) {
            RandomEventsManager.Instance.EmbassyOutpostEvent();
        }
        if (Input.GetKeyDown("q")) {
            RandomEventPopup.Instance.ShowEventPopUp("This is a random event");
        }

    }

}
