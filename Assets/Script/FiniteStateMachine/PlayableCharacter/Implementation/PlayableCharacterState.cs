public abstract class PlayableCharacterState : IPlayableCharacterState
{
    public abstract IPlayableCharacterState CheckingStateModification(MovePlayer player);

    public abstract void OnEnter(MovePlayer player);

    public abstract void OnExit(MovePlayer player);

    public abstract void PerformingInput(string action);
}
