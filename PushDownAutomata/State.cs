namespace StateLib.PushDownAutomata;
public abstract class State<T> where T : class {
  protected readonly StateMachine<T> PDA;

  protected State(StateMachine<T> pda) {
    PDA = pda;
  }

  public virtual void EnterState() { }
  public virtual void Process(double delta) { }
  public virtual void ExitState() { }
}
