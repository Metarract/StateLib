using Metarract.StateLib.Models;

namespace Metarract.StateLib.FiniteStateMachine;
public abstract class State<T, TS> : IState where T : class where TS : State<T, TS> {
  public delegate void StateChangeHandler(TS nextState);
  public event StateChangeHandler OnChangeState;

  public T Context { get; private set; }
  public void SetContext(T context) => Context = context;

  public virtual void OnEnter() { }
  public virtual void OnProcess(double delta) { }
  public virtual void OnPhysics(double delta) { }
  public virtual void OnExit() { }

  protected void ChangeState(TS nextState) => OnChangeState?.Invoke(nextState);
}
