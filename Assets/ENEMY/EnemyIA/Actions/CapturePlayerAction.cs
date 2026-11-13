using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CapturePlayer", story: "Send message that [Target] is captured", category: "Action", id: "af732d42c361ceb892fa5793a0a9fc0b")]
public partial class CapturePlayerAction : Action
{
    //AVISA DE QUE SE HA CAPTURADO AL JUGADOR: 
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    protected override Status OnStart()
    {
        EventManagerSO.Instance.NotifiesPlayerIsCaptured();
        return Status.Success;
    }
}

