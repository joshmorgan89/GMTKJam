using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tester : MonoBehaviourSingleton<Tester>
{
    public TMP_Text fuelText;

    public float timer;
    public GameObject asteroid;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2) { 
            Instantiate(asteroid,Vector3.zero, Quaternion.Euler(new Vector3(0,0, Random.Range(0f, 360f))));
            timer -= 2;
        }


        HUD.Instance.UpdateFuel(100, ResourceManager.Instance.fuel);
    }

    public void UpdateResourceUI(float electricity, float fuel) {
        fuelText.text = "Fuel: " + fuel;
    }
}
