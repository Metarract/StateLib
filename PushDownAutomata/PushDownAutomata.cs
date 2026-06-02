using System.Collections.Generic;

namespace Metarract.StateLib.PushDownAutomata;
public class PushDownAutomata<T>(T context) where T : class {
  private readonly Stack<State<T>> _stateStack = [];
  public State<T> CurrentState => _stateStack.Count > 0 ? _stateStack.Peek() : null;

  public T Context { get; private set; } = context;

  public void Push(State<T> newState) {
    _stateStack.Push(newState);
    newState.EnterState();
  }

  public void Pop() {
    if (_stateStack.Count == 0) return;
    CurrentState.OnPop -= Pop;
    CurrentState.ExitState();
    _stateStack.Pop();
    CurrentState.OnPop += Pop;
  }

  public void SetContext(T context) => Context = context;

  public void Process(double delta) => CurrentState?.Process(delta);
}
