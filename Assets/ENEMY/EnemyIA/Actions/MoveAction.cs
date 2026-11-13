using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move", story: "[Self] rotate 360º", category: "Action", id: "1401caf83a04fd12ff32c8c7c14862d3")]
public partial class MoveAction : Action
{
    //GIRA 360º CUANDO ENTRA EN EL ESTADO DE BUSQUEDA. 
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    private float _startAngle;
    private float _time;
    
    protected override Status OnStart()
    {
        _startAngle = Self.Value.transform.eulerAngles.y;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _time += Time.deltaTime;
        Self.Value.transform.Rotate (Vector3.up * 50 * Time.deltaTime, Space.Self);
        //AÑADE UN PEQUEÑO DELAY AL CHECK PARA EVITAR QUE EMPIEZA Y DETECTE QUE ESTA EN EL MISMO ANGULO QUE EMPEZÓ AL INSTANTE. 
        if (Mathf.Round(Self.Value.transform.eulerAngles.y) == Mathf.Round(_startAngle) && _time >= 0.5f)
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }
}

