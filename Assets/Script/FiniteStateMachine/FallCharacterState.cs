public class FallCharacterState : CharacterState
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
            // Idle state
            if (player.isGrounding == true)
            {
                return nextState = new IdleCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.animator.Play("Fall");
    }

    public override void OnExit(MovePlayer player)
    {
        if (player.isGrounding == true)
        {
            player.audioManager.Play("Grounding");
        }
    }

    public override void PerformingInput(string action)
    {
        // Light ATK 1
        if (action.Equals("LightATK"))
        {
            nextState = new LightATK1CharacterState();
        }
        // Medium ATK 1
        if (action.Equals("MediumATK"))
        {
            nextState = new MediumATK1CharacterState();
        }
        // Heavy ATK 1
        if (action.Equals("HeavyATK"))
        {
            nextState = new HeavyATK1CharacterState();
        }
        // Dash
        if (action.Equals("Dash"))
        {
            nextState = new DashCharacterState();
        }
    }
}
