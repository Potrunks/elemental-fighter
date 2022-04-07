public class MediumATK2CharacterState : CharacterState
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
            // Medium ATK 2 Transition
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                return nextState = new MediumATK2TransitionCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        // Check if grounded
        if (player.isGrounding == true)
        {
            player.animator.Play("MediumATK2");
        }
        else
        {
            // if not grounded, air medium atk 2 animation
            player.animator.Play("AirMediumAttack2");
        }
    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
