using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CostumeCheck", story: "Chek [Target] costume with [Detector]", category: "Conditions", id: "09d03013351473b173a3f3da4f5721b2")]
public partial class CostumeCheckCondition : Condition
{
    //COMPRUEBA SI EL JUGADOR SE ESTÁ DISFRAZANDO O NO CON SU DETECTOR CORRESPONDIENTE
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<CostumeDetector> Detector;

    public override bool IsTrue()
    {
        return Detector.Value.IsUsingCostume();
    }
}
