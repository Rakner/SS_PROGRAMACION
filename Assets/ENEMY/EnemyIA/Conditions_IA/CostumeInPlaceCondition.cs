using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CostumeInPlace", story: "Check [Target] costume is in place with [Detector]", category: "Conditions", id: "86598697c4cd3ff19aabb46473ee824e")]
public partial class CostumeInPlaceCondition : Condition
{
    //COMPRUEBA QUE EL JUGADOR SE ESTE DISFRAZANDO CORRECATMENTE O NO. 
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<CostumeInPlaceDetector> Detector;

    public override bool IsTrue()
    {
        return Detector.Value.IsInPlace();
    }
}
