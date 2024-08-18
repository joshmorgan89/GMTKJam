using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
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
}
