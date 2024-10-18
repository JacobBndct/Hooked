using UnityEngine;

public abstract class EntityStateAction : ScriptableObject
{
    public abstract void PerformAction(Entity entityReference);
}
