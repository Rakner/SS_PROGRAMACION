using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "LineOfSightCheck", story: "Check [Target] with Line Of Sight [detector]", category: "Conditions", id: "ee486ae43247dec4a91a4a952986ab05")]
public partial class LineOfSightCheckCondition : Condition
{
    //COMPRUEBA SI EL JUGADOR SE ENCUENTRA EN EL RANGO DE VISIÓN O NO CON SU DETECTOR CORRESPONDIENTE. 
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<LineOfSightDetector> Detector;

    public override bool IsTrue()
    {
        Debug.Log("LineOfSightCheckCondition");
        return Detector.Value.PerformDetection(Target.Value) !=  null;
    }
}
