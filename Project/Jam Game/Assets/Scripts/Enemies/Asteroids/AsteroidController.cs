using Scripts.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : BaseEnemyController
{

    private void OnDestroy()
    {
        SoundManager.Instance.PlaySound(SoundName.AsteroidBreaks);
    }
}
