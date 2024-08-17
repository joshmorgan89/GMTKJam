using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomEventPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject _popUpWindow;
    [SerializeField]
    private TMP_Text _popUpInfo;

    public void ShowEventPopUp(string text)
    {
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
