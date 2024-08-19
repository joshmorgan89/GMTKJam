using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviourSingleton<HUD>
{
    [Header("Resource Bar")]
    [SerializeField]
    private Image _populationBar;
    [SerializeField]
    private Image _goodwillBar;
    [SerializeField]
    private Image _fuelBar;

    [SerializeField]
    private TMP_Text _rareMineralCount;
    [SerializeField]
    private TMP_Text _refugeeCount;
    public void UpdatePopulation(float maxPopulation, float currentPopulation)
    {
        _populationBar.rectTransform.localScale = new Vector3(currentPopulation / maxPopulation, _populationBar.rectTransform.localScale.y, _populationBar.rectTransform.localScale.z);
    }
    public void UpdateGoodwill(float maxGoodwil, float currentGoodwil)
    {
        _goodwillBar.rectTransform.localScale = new Vector3(currentGoodwil / maxGoodwil, _goodwillBar.rectTransform.localScale.y, _goodwillBar.rectTransform.localScale.z);
    }
    public void UpdateFuel(float maxFuel, float currentFuel)
    {
        _fuelBar.rectTransform.localScale = new Vector3(currentFuel / maxFuel, _fuelBar.rectTransform.localScale.y, _fuelBar.rectTransform.localScale.z);
    }

    public void UpdateRareMineral() {
        _rareMineralCount.text = "Rare Mineral: " + ResourceManager.Instance.rareMineral;
    }
    public void UpdateRefugee()
    {
        _refugeeCount.text = "RefugeeCount: " + ResourceManager.Instance.refugees;
    }
}
