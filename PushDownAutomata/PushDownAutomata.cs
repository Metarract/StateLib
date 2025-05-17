namespace Metarract.States.PushDownAutomata;

public class StateMachine<T> where T : class {
  private readonly Stack<State<T>> StateStack = [];
  public State<T> CurrentState => StateStack.Count > 0 ? StateStack.Peek() : null;

  public T Root { get; private set; }

  public StateMachine(T root) {
    Root = root;
  }

  public void Push(State<T> newState) {
    StateStack.Push(newState);
    newState.EnterState();
  }

  public void Pop() {
    if (StateStack.Count == 0) return;
    CurrentState.ExitState();
    StateStack.Pop();
  }

  public void SetRoot(T root) => Root = root;

  public void Process(double delta) => CurrentState?.Process(delta);
}
