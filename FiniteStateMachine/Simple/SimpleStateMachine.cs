namespace Metarract.StateLib.FiniteStateMachine.Simple;
/// <summary>
/// The same as the standard FiniteStateMachine, minus the required Context
/// </summary>
public abstract class StateMachine<T> where T : State<T> {
  public T CurrentState { get; private set; }
  
  #region lifecycle
  public virtual void OnProcess(double delta) => CurrentState?.OnProcess(delta);
  public virtual void OnPhysics(double delta) => CurrentState?.OnPhysics(delta);
  #endregion
  
  public void ChangeState(T newState) {
    if (CurrentState is not null) {
      CurrentState.OnExit();
      CurrentState.OnChangeState -= ChangeState;
    }

    CurrentState = newState;
    CurrentState.OnChangeState += ChangeState;
    CurrentState.OnEnter();
  }
}
