public class MediumATK1CharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        // hurt state
        if (player.isHurting == true)
        {
            return nextState = new HurtCharacterState();
        }
        else
        {
            // Medium ATK 1 Transition
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                return nextState = new MediumATK1TransitionCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        // Check if grounded
        if (player.isGrounding == true)
        {
            // if grounded, Disable Player Velocity
            player.moveSpeed = player.stopMoveSpeed;
            player.animator.Play("MediumATK1");
        }
        else
        {
            // if not grounded, air medium atk 1 animation
            player.animator.Play("AirMediumAttack1");
        }
    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
