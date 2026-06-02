namespace Metarract.StateLib.Models;
public interface IStateMachine<out TS> where TS : IState {
  TS CurrentState { get; }

  void OnProcess(double delta) => CurrentState?.OnProcess(delta);
  void OnPhysics(double delta) => CurrentState?.OnPhysics(delta);
}
