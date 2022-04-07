public interface ICharacterState
{
    void PerformingInput(string action);
    ICharacterState CheckingStateModification(MovePlayer player);
    void OnEnter(MovePlayer player);
    void OnExit(MovePlayer player);
}
