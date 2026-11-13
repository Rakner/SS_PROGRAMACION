using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SearchDetector", story: "Check if [Target] is moving with [Detector] and notifies [PlayerMoved]", category: "Action", id: "ac080533b56a3e3d02d4930bd4bc9378")]
public partial class SearchDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> Detector;
    [SerializeReference] public BlackboardVariable<bool> PlayerMoved;
    protected override Status OnStart()
    {
        Detector.Value.gameObject.SetActive(true);
        return Status.Running;
        
    }

    //COMRPUEBA SI EL JUGADOR SE HA MOVIDO O NO, DE HACERLO, LO NOTIFICA Y CAMBIA DE ESTADO
    protected override Status OnUpdate()
    {
        Debug.Log("EL JUGADOR SE HA MOVIDO?: " + Detector.Value.GetComponent<MoveDetector>().PlayerHasMoved());
        if (Detector.Value.GetComponent<MoveDetector>().PlayerHasMoved())
        {
            PlayerMoved.Value = true;
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
        
    }
}

