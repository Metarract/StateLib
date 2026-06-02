using System;

namespace Metarract.StateLib.PushDownAutomata;
public abstract class State<T>(PushDownAutomata<T> pda) where T : class {
  protected readonly PushDownAutomata<T> PDA = pda;
  public event Action OnPop;

  public virtual void EnterState() { }
  public virtual void Process(double delta) { }
  public virtual void ExitState() { }

  protected void InvokePop() => OnPop?.Invoke();
}
