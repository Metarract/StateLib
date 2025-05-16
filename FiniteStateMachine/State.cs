using Godot;

namespace StateLib.FiniteStateMachine;
public abstract class State<T> where T : class {
  public delegate void StateChangeHandler(State<T> nextState);
  public event StateChangeHandler OnChangeState;

  public T Context { get; private set; }
  public void SetContext(T context) => Context = context;

  public virtual void EnterState() { }
  public virtual void OnProcess(double delta) { }
  public virtual void OnPhysics(double delta) { }
  public virtual void OnInput(InputEvent inputEvent) { }
  public virtual void ExitState() { }

  protected void ChangeState(State<T> nextState) {
    OnChangeState?.Invoke(nextState);
  }
}
