using UnityEngine;

public class DashCharacterState : CharacterState
{
    private ICharacterState nextState;
    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        if (player.isHurting == false)
        {
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
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
        else
        {
            // hurt state
            return nextState = new HurtCharacterState();
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.audioManager.Play("FireDash");
        // dash effect
        player.DashEffect();
        // dash move
        if (player.isFlipLeft == true)
        {
            // rb.AddForce(new Vector2(dashForce * -1, 0f));
            player.rb.velocity = Vector2.left * player.dashForce;

        }
        else if (player.isFlipLeft == false)
        {
            // rb.AddForce(new Vector2(dashForce, 0f));
            player.rb.velocity = Vector2.right * player.dashForce;
        }
        // dash animation
        player.animator.Play("DashMove");
    }

    public override void OnExit(MovePlayer player)
    {
        // cooldown
        player.nextTimeDash = Time.time + MovePlayer.dashCooldownTime;
    }

    public override void PerformingInput(string action)
    {

    }
}
