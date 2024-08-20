using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameoverScore : MonoBehaviour
{
    
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().text = "Your Score Is: " + Settings.Instance.GoodwillOverall;
    }

}
