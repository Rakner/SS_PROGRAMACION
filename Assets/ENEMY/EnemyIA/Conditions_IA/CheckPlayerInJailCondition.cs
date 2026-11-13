using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckPlayerInJail", story: "Check [Target] is in Jail with [Detector]", category: "Conditions", id: "677eeda0f543f8e0847847676b4e8598")]
public partial class CheckPlayerInJailCondition : Condition
{
    //COMPRUEBA CON SU DETECTOR SI EL JUGADOR ESTA O NO EN LA CELDA. 
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<PlayerInJailDetector> Detector;

    public override bool IsTrue()
    {
        return Detector.Value.IsInCelda();
    }
}
