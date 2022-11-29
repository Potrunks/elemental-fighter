public class MediumATK2TransitionCharacterState : PlayableCharacterState
{
    private IPlayableCharacterState nextState;
    private bool isAirTransition = false;
    public override IPlayableCharacterState CheckingStateModification(MovePlayer player)
    {
        if (player.isHurting == true)
        {
            // hurt state
            return nextState = new HurtCharacterState();
        }
        else
        {
            // attendre fin animation sauf pour hurt state
            if ((isAirTransition == true && player.isGrounding == true) || player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                // idle state
                if (player.isGrounding == true)
                {
                    return nextState = new IdleCharacterState();
                }
                else
                {
                    // jump state
                    if (player.rb.velocity.y >= 0.1f)
                    {
                        return nextState = new JumpCharacterState();
                    }
                    // fall state
                    if (player.rb.velocity.y <= -0.1f)
                    {
                        return nextState = new FallCharacterState();
                    }
                }
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        // check if grounded
        if(player.isGrounding == true){
            // if grounded, transition normal
            player.animator.Play("MediumATK2Transition");
        } else {
            // if not grounded, air transition
            player.animator.Play("AirMediumAttack2Transition");
            isAirTransition = true;
        }
    }

    public override void OnExit(MovePlayer player)
    {
        isAirTransition = false;
    }

    public override void PerformingInput(string action)
    {
        // Heavy ATK 1
        if (action.Equals("HeavyATK"))
        {
            nextState = new HeavyATK1CharacterState();
        }
    }
}
