using Godot;

namespace Metarract.States.FiniteStateMachine;
/// <summary>
/// Abstract Finite State Machine
/// Passes engine lifecycle methods down to an internal state which handles logic unique to the state.
/// Generally, only states should handle transitioning from one state to the next.
/// Concrete implementations should define a definite type for the generic, and should initialize with a default state 
/// </summary>
/// <typeparam name="T">Context that contains relevant references to anything the state might need to read/modify during execution, setup, or teardown. Must match the concrete State implementation's type.</typeparam>
public abstract class StateMachine<T>(T context)
  where T : class {
  public State<T> CurrentState { get; protected set; }
  /// <summary>
  /// Our Context contains references to entities relevant to our states. 
  /// This will oftentimes just be a reference back to our Actor or whatever is
  /// utilizing our StateMachine. Each StateMachine user type should probably
  /// have its own Context.
  /// </summary>
  public T Context { get; protected set; } = context;

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
