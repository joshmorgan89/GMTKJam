using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void CloseTutorial() {
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
}
