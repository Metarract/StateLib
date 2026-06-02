# StateLib

Just a simple pack of stateful models that I use for various projects. Currently tuned to Godot, but should be straightforward enough to use wherever.

## Finite State Machine

Stateful setup characterized by a `StateMachine`, and several `States`. The `StateMachine` manages changing states, and acts as an abstraction for the current state, such that the consumer will pass along any update methods through the `StateMachine` to the current `State`

The `StateMachine` and `States` must have a matching `Context`, which must be a `class` containing any relevant data, references, or delegates that each `State` may need access to to operate.

The `IStateMachine` interface is exposed as well as an abstract `StateMachine` that implements it.

### SimpleStateMachine

`SimpleStateMachine` is an implementation that does not require an associated `Context`. Everything else remains the same.

## Pushdown Automata

### WIP

A special kind of state machine where States are added to a FILO/LIFO Stack, and as States complete they pop themselves off of the stack to allow the next state to act.