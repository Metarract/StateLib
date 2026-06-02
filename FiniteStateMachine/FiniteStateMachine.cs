using Metarract.StateLib.Models;

namespace Metarract.StateLib.FiniteStateMachine;
/// <summary>
/// Abstract Finite State Machine
/// <br />
/// Passes engine lifecycle methods down to an internal state which handles logic unique to the state.
/// Generally, only states should handle transitioning from one state to the next.
/// Utilizes the Curiously Recurring Generic Pattern for strict type safety -
/// hence, concrete implementations must define a Context type, a State with that Context, and finally the State Machine
/// that specifies both. 
/// </summary>
/// <typeparam name="T">
/// Context that contains relevant references to anything the state might need to read/modify during execution, setup,
/// or teardown. Must match the concrete State implementation's type.
/// </typeparam>
/// <typeparam name="TS">
/// The State&lt;T&gt; that must be used with this implementation. This State must accept the same Context type in order
/// to be considered valid
/// </typeparam>
public abstract class FiniteStateMachine<T, TS>(T context) : IStateMachine<TS> where T : class where TS : State<T, TS> {
  public TS CurrentState { get; private set; }

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
  #endregion

  public void ChangeState(TS newState) {
    if (CurrentState is not null) {
      CurrentState.OnExit();
      CurrentState.OnChangeState -= ChangeState;
    }

    CurrentState = newState;
    CurrentState.OnChangeState += ChangeState;
    CurrentState.SetContext(Context);
    CurrentState.OnEnter();
  }

  public void SetContext(T context) {
    Context = context;
    CurrentState?.SetContext(context);
  }
}
