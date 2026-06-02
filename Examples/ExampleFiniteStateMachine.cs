using Metarract.StateLib.FiniteStateMachine;
using System;


namespace StateLib.Examples;
#region the top-level stuff
/// <summary>
/// Example implementation with all the fixin's. All the plumbing is already handled, outside of any unique methods that are desired.
/// </summary>
/// <param name="context"></param>
internal sealed class ExampleFiniteStateMachine(ExampleContext context) : FiniteStateMachine<ExampleContext, ExampleState>(context);

/// <summary>
/// We establish a base abstract class that all of our States are derived from. Once again, all the plumbing is already handled.
/// </summary>
internal abstract class ExampleState : State<ExampleContext, ExampleState>;

/// <summary>
/// A simple context with a single mutable property to illustrate the mutating context as the states move from one to the other
/// </summary>
internal sealed class ExampleContext {
  public double Value { get; set; }
}
#endregion

#region states
internal sealed class ExampleStateIdle : ExampleState {
  private const int COUNTER_MAX = 100;
  private int _counter;

  public override void OnEnter() => Console.WriteLine("Entering Idle state");

  public override void OnProcess(double delta) {
    // wait for 100 ticks
    if (++_counter < COUNTER_MAX) return;
    // and then we call ChangeState with a new Active state
    ChangeState(new ExampleStateActive());
  }

  public override void OnExit() => Console.WriteLine("Exiting Idle state");
}

internal sealed class ExampleStateActive : ExampleState {
  private static readonly (int Min, int Max) CountdownRange = (100, 500);
  private readonly Random _rng = new();
  private int _countdown;
  private double _initialContextValue;

  public override void OnEnter() {
    // here we will override our EnterState, in order to randomize how many ticks we process for
    _countdown = _rng.Next(CountdownRange.Min, CountdownRange.Max);
    Console.WriteLine($"Entering Active state, will process for {_countdown} ticks");
    _initialContextValue = Context.Value;
  }

  public override void OnProcess(double delta) {
    Context.Value += delta;
    _countdown--;
    if (_countdown > 0) return;
    ChangeState(new ExampleStateIdle());
  }

  public override void OnExit() {
    Console.WriteLine($"Exiting Active state, our context value is now at {Context.Value}, which means it changed by {Context.Value - _initialContextValue}");
  }
}
#endregion

/// <summary>
/// Instantiate this and run the _Process method to see this in action
/// </summary>
public class ExampleConsumer {
  private readonly ExampleFiniteStateMachine _finiteStateMachine;

  public ExampleConsumer() {
    // here we set the context via the constructor, but obviously you may choose to set the context later or via <FSM>.SetContext
    _finiteStateMachine = new ExampleFiniteStateMachine(new ExampleContext());
    // here we set the state manually, but your concrete implementation can specify the initial state in its constructor as well
    _finiteStateMachine.ChangeState(new ExampleStateIdle());
  }

  public void _Process(double delta) {
    _finiteStateMachine.OnProcess(delta);
  }
}
