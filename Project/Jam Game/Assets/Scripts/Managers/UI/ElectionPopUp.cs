using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ElectionPopUp : MonoBehaviourSingleton<ElectionPopUp>
{
    [SerializeField]
    private GameObject _popUpWindow;
    [SerializeField]
    private TMP_Text _popUpInfo;

    public Sprite[] ordinances;
    public Image PopUpBorder;
    public void ShowElectionPopUp(string text) {
        _popUpWindow.SetActive(true);
        _popUpInfo.text = text;
        PopUpBorder.sprite = ordinances[Random.Range(0,ordinances.Length-1)];
        SoundManager.Instance.PlaySound(SoundName.ElectionHasHappened);
        //StartCoroutine(HidePopUpAfterDelay(4f));
    }

    private IEnumerator HidePopUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _popUpWindow.SetActive(false);
    }
}
