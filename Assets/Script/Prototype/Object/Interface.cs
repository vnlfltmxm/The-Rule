public interface IObjectState<T> : IObjectState where T : IObjectState<T> { }

public interface IObjectState
{
    public void StateEnter() { }
    public void StateUpdate() { }
    public void StateExit() { }
}
