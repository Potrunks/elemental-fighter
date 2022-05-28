using UnityEngine;

public class JumpCharacterState : CharacterState
{
    private ICharacterState nextState;
    private System.Random random = new System.Random();

    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        if (player.isHurting == false)
        {
            if (!(player.rb.velocity.y >= 0.01f))
            {
                // Idle State
                if (player.isGrounding == true)
                {
                    return nextState = new IdleCharacterState();
                }
                else
                {
                    // Fall State
                    if (player.rb.velocity.y <= -0.1f)
                    {
                        return nextState = new FallCharacterState();
                    }
                }
            }
        }
        else
        {
            // hurt state
            return nextState = new HurtCharacterState();
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.audioManager.Play("Jump");
        player.audioManager.PlaySoundByIndexInListOfSound(player.audioManager.jumpSounds, random.Next(0, player.audioManager.jumpSounds.Length));
        player.moveSpeed = player.normalMoveSpeed;
        player.rb.AddForce(new Vector2(0f, player.jumpForce));
        player.animator.Play("Jump");
    }

    public override void OnExit(MovePlayer player)
    {

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
