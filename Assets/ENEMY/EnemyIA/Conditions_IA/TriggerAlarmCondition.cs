using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TriggerAlarm", story: "Check Alarm Trigger with [Detector]", category: "Conditions", id: "f49abf448fd7e014721191f76c071d93")]
public partial class TriggerAlarmCondition : Condition
{
    //COMPRUEBA SI SE HA ACTIVADO O NO LA ALARMA
    [SerializeReference] public BlackboardVariable<AlarmDetector> Detector;

    public override bool IsTrue()
    {
        return Detector.Value.IsAlarmTriggered();
    }
}
