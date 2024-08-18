using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings: MonoBehaviourSingleton<Settings>
{
    public bool BelovedLeader = false;
    public int GracePeriod = 0;
    
    public bool Energizer = false;
    public int EnergizerRange = 1;

    public bool TradingHub = false;
    public float TradingMultiplier = 1f;

    public bool NanobotComposition = false;

    public bool DroneBuddy = false;

    public bool Harder = false;
    public float DurabilityMultiplier = 1f;

    public bool Fire = false;
    public float FireMultiplier = 1f;

    public bool EfficientThrusters = false;
    public float PlayerSpeedMultiplier = 1f;

    public bool Asylum = false;
    public float RefugeeDropModifier = 1;

    public bool Engineer = false;
    public float RepaireCostModifier = 1;

    public bool Scavenger = false;
    public float RoomDropMultiplier = 1;

    public bool FoundFamily = false;

}
