# State Machine + Command Pattern

This Unity example combines two patterns with separate responsibilities:

- The **state machine** decides *when* a unit should idle, move, attack, become stunned, or die.
- A **state** describes which actions happen when that mode starts, updates, or ends.
- A **command** wraps one concrete action on a unit, such as moving, stopping, or starting an attack.

Keeping decisions in states and actions in commands makes both sides easier to change and test. A state can compose several commands without knowing their implementation, and the same command can be invoked from a state, input handler, AI, replay, or tutorial.

## Runtime flow

```text
Unit.Update
    -> StateMachine.Tick
        -> evaluate transitions for the current state
        -> Exit old state
        -> Enter new state
        -> Tick current state
            -> execute command(s)
                -> call the Unit API
```

For example, when a target enters attack range:

1. `Unit`'s move-to-attack transition becomes true.
2. `StateMachine` exits `MoveState`, which executes `StopMoveCommand`.
3. It enters `AttackState`, which executes `StartAttackCommand`.
4. When the target leaves range, `AttackState` exits and executes `StopAttackCommand`.

Transitions are evaluated in insertion order. Death and stun transitions are registered first so they have priority over normal movement and attack transitions.

## Folder structure

```text
scripts/
├── Battle/
│   ├── Core/       # BattleManager and DamageableEntity
│   ├── Data/       # Serializable unit configuration
│   └── Units/      # Unit behavior and ally/enemy identities
└── Patterns/
    ├── Commands/   # Small reusable unit actions
    └── StateMachine/
        ├── Core/   # State contracts, transitions, state-machine runner
        └── States/ # Battle-specific state implementations
```

Namespaces mirror these folders and all begin with `StateCommandDemo`.

## Unity setup

1. Add one `BattleManager` component to the scene.
2. Create ally and enemy GameObjects with a `Rigidbody2D`, a collider, and either `AllyUnit` or `EnemyUnit`.
3. Fill in the serialized `UnitData` values on each unit.
4. Ensure at least one ally and one enemy are active. Units register themselves with `BattleManager` in `Start`.

The example needs only standard Unity APIs; the old choice, placement, UI, upgrade, and DOTween dependencies are not required.

## Extending the example

To add a command, implement `ICommand` and keep `Execute` focused on one action. To add a state, implement one or more of `IEnterState`, `IUpdateState`, and `IExitState`, then create and connect the state in `Unit.ConfigureStateMachine`.

For external effects such as a spell, create and execute a `StunCommand`. The unit's `IsStunned` flag then causes the state machine to enter `StunState`, which stops movement until the stun expires.
