public class HeavyATK1CharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        // Hurt state
        if (player.isHurting == true)
        {
            return nextState = new HurtCharacterState();
        }
        else
        {
            // Heavy ATK 1 Transition
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                return nextState = new HeavyATK1TransitionCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        if (player.isGrounding == true)
        {
            // Disable Velocity Player
            player.moveSpeed = player.stopMoveSpeed;
            // Heavy ATK 1 Action
            player.animator.Play("HeavyATK1");
        }
        else
        {
            // Air Heavy ATK 1 animation
            player.animator.Play("AirHeavyAttack1");
        }

    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
