namespace Metarract.StateLib.FiniteStateMachine.Simple;
public abstract class State<T> where T : State<T> {
  public delegate void StateChangeHandler(T nextState);
  public event StateChangeHandler OnChangeState;
  
  public virtual void OnEnter() { }
  public virtual void OnProcess(double delta) { }
  public virtual void OnPhysics(double delta) { }
  public virtual void OnExit() { }
  
  protected void ChangeState(T nextState) => OnChangeState?.Invoke(nextState);
}
