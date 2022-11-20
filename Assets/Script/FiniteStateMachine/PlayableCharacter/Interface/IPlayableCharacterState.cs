public interface IPlayableCharacterState
{
    void PerformingInput(string action);
    IPlayableCharacterState CheckingStateModification(MovePlayer player);
    void OnEnter(MovePlayer player);
    void OnExit(MovePlayer player);
}
