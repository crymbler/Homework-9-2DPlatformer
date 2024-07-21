using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected IStateable _stateable;

    private void Awake()
    {
        OnAwake();
    }

    public void Initialize(IStateable stateable)
    {
        _stateable = stateable;
    }

    public virtual void Enter() =>
        enabled = true;

    public virtual void Exit() =>
        enabled = false;

    protected virtual void OnAwake() { }
}