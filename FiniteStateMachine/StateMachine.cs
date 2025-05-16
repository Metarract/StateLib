using Godot;

namespace StateLib.FiniteStateMachine;
public abstract class StateMachine<T> where T : class {
  public State<T> CurrentState { get; protected set; }
  /// <summary>
  /// Our Context contains references to entities relevant to our states. 
  /// This will oftentimes just be a reference back to our Actor or whatever is
  /// utilizing our StateMachine. Each StateMachine user type should probably
  /// have its own Context.
  /// </summary>
  public T Context { get; protected set; }

  protected StateMachine(T context) {
    Context = context;
  }

  #region lifecycle
  public virtual void OnProcess(double delta) => CurrentState?.OnProcess(delta);
  public virtual void OnPhysics(double delta) => CurrentState?.OnPhysics(delta);
  public virtual void OnInput(InputEvent inputEvent) => CurrentState?.OnInput(inputEvent);
  #endregion

  public void ChangeState(State<T> newState) {
    if (CurrentState is not null) {
      CurrentState.ExitState();
      CurrentState.OnChangeState -= ChangeState;
    }
    CurrentState = newState;
    CurrentState.OnChangeState += ChangeState;
    CurrentState.SetContext(Context);
    CurrentState.EnterState();
  }

  public void SetContext(T context) {
    Context = context;
    CurrentState?.SetContext(context);
  }
}
