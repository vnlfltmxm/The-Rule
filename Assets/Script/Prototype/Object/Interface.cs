using UnityEngine;

public interface IObjectState<T> : IObjectState where T : IObjectState<T> { }

public interface IObjectState
{
    public void StateEnter() { }
    public void StateUpdate() { }
    public void StateFixedUpdate() { }
    public void StateExit() { }
}

public interface ITrace { }

public interface ISoundTrace : ITrace
{
    public void OnHearSound(Vector3 position);
}

public interface ISystemTrace : ITrace
{

}
