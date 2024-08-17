using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ElectionPopUp : MonoBehaviourSingleton<ElectionPopUp>
{
    [SerializeField]
    private GameObject _popUpWindow;
    [SerializeField]
    private TMP_Text _popUpInfo;

    public void ShowElectionPopUp(string text) {
        _popUpWindow.SetActive(true);
        _popUpInfo.text = text;
        StartCoroutine(HidePopUpAfterDelay(4f));
    }

    private IEnumerator HidePopUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _popUpWindow.SetActive(false);
    }
}
