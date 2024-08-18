using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviourSingleton<UIManager>
{
    public int electionEventNum;
    private float _electionHappenValue;
    private float _nextElectionHappenValue;

    public float gameProgressSpeed = 0.01f;
    public GameObject electionPrefab;
    public GameObject electionGroup;

    public Slider gameProgress;

    [Header("Outpost")]
    public GameObject outpostPanel;
    public GameObject shop;
    public GameObject embassy;
    public TMP_Text embassyText;

    private void Start()
    {
        for(int i = 0; i < electionEventNum; i++)
            Instantiate(electionPrefab, electionGroup.transform);
        _electionHappenValue = 1.0f / (electionEventNum+1);
        _nextElectionHappenValue = _electionHappenValue;
    }

    private void Update()
    {
        gameProgress.value += gameProgressSpeed * Time.deltaTime;

        if (gameProgress.value >= _nextElectionHappenValue)
        {
            ElectionPopUp.Instance.ShowElectionPopUp("This is an election");
            _nextElectionHappenValue += _electionHappenValue;
        }
    }

    public void ShowShopOutpost() {
        outpostPanel.SetActive(true);
        shop.SetActive(true);
    }
    public void ShowEmbassyOutpost()
    {
        outpostPanel.SetActive(true);
        embassy.SetActive(true);
    }

    public void ChanegEmbassyTradeText(string tradeText) {

        embassyText.text = tradeText;
    }

    public void CloseOutpost() {
        outpostPanel.SetActive(false);
        embassy.SetActive(false);
        shop.SetActive(false);
    }
}
