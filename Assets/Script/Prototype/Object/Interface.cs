using UnityEngine;

public interface IObjectState<T> : IObjectState where T : IObjectState<T> { }

public interface IObjectState
{
    public void StateEnter() { }
    public void StateUpdate() { }
    public void StateFixedUpdate() { }
    public void StateExit() { }
    public void OnTriggerEnter(Collider other) { }
}

public interface ISetPlayer : IObjectState
{
    public void SetPlayer(Transform player);
}

public interface ITrace { }

public interface ISoundTrace : ITrace
{
    public void OnHearSound(Vector3 position, Transform player);
}

public interface ISystemTrace : ITrace
{
    public void OnSystemTracking(Transform player);
}
