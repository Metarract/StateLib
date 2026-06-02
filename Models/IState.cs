namespace Metarract.StateLib.Models;
public interface IState {
  void OnEnter();
  void OnExit();

  void OnProcess(double delta);
  void OnPhysics(double delta);
}
