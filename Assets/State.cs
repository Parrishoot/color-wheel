public abstract class State
{
    protected StateMachine StateMachine { get; set; }

    public State(StateMachine stateMachine) {
        this.StateMachine = stateMachine;
    }

    public abstract void OnStart();

    public abstract void OnUpdate(float deltaTime);

    public virtual void OnFixedUpdate(float fixedDeltaTime) {

    }

    public abstract void OnEnd();

}