public abstract class CharacterState : ICharacterState
{
    public abstract ICharacterState CheckingStateModification(MovePlayer player);

    public abstract void OnEnter(MovePlayer player);

    public abstract void OnExit(MovePlayer player);

    public abstract void PerformingInput(string action);
}
