using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum of types of power-ups
public enum PowerUpType
{
    BelovedLeader,
    Energizer,
    TradingHub,
    NanobotComposition,
    DroneBuddy,
    Harder,
    Fire,
    EfficientThrusters,
    Asylum,
    Engineer,
    Scavenger,
    FoundFamily
}

public class PowerUpEffect : MonoBehaviourSingleton<PowerUpEffect>
{
    public void ApplyPowerUpEffect(PowerUpType powerUp)
    {
        switch (powerUp)
        {
            case PowerUpType.BelovedLeader:
                ApplyBelovedLeader();
                break;
            case PowerUpType.Energizer:
                ApplyEnergizer();
                break;
            case PowerUpType.TradingHub:
                ApplyTradingHub();
                break;
            case PowerUpType.NanobotComposition:
                ApplyNanobotComposition();
                break;
            case PowerUpType.DroneBuddy:
                ApplyDroneBuddy();
                break;
            case PowerUpType.Harder:
                ApplyHarder();
                break;
            case PowerUpType.Fire:
                ApplyFire();
                break;
            case PowerUpType.EfficientThrusters:
                ApplyEfficientThrusters();
                break;
            case PowerUpType.Asylum:
                ApplyAsylum();
                break;
            case PowerUpType.Engineer:
                ApplyEngineer();
                break;
            case PowerUpType.Scavenger:
                ApplyScavenger();
                break;
            case PowerUpType.FoundFamily:
                ApplyFoundFamily();
                break;
            default:
                Debug.Log("Unknown power-up: " + powerUp);
                break;
        }
    }

    public void ApplyBelovedLeader() {
        Settings.Instance.BelovedLeader = true;
    }
    public void ApplyEnergizer() {  }
    public void ApplyTradingHub() {  }
    public void ApplyNanobotComposition() {  }
    public void ApplyDroneBuddy() {  }
    public void ApplyHarder() {  }
    public void ApplyFire() { }
    public void ApplyEfficientThrusters() {  }
    public void ApplyAsylum() {  }
    public void ApplyEngineer() { }
    public void ApplyScavenger() {  }
    public void ApplyFoundFamily() {  }
}

