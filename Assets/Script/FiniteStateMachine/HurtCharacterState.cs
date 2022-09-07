using UnityEngine;

public class HurtCharacterState : CharacterState
{
    private ICharacterState nextState;
    private System.Random random = new System.Random();
    private float heavyATKPushForce = 50f;

    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        // When damage is taken, go to Idle State
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            player.isHurtingByPushAttack = false;
            if (player.GetComponent<DamageCommand>().currentHealth <= 0)
            {
                return nextState = new DieCharacterState();
            }
            else
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
            if (player.isHurting == true)
            {
                return nextState = new HurtCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        // isHurting go to False value
        player.isHurting = false;
        if (player.playerDamageCommand.isInvincible == false)
        {
            player.audioManager.Play("Hurting");
            player.audioManager.PlaySoundByIndexInListOfSound(player.audioManager.hurtSounds, random.Next(0, player.audioManager.hurtSounds.Length));
            // Play animation
            player.moveSpeed = player.stopMoveSpeed;
            player.BloodEffect();
            player.animator.Play("Hurt", -1, 0.0f);
            if (player.isHurtingByPushAttack == true)
            {
                if (player.isFlipLeft == false)
                {
                    if (player.playerDamageCommand.GetIsAttackedFromBehind() == true)
                    {
                        player.rb.velocity = Vector2.right * heavyATKPushForce;
                    }
                    else
                    {
                        player.rb.velocity = Vector2.left * heavyATKPushForce;
                    }
                }
                else
                {
                    if (player.playerDamageCommand.GetIsAttackedFromBehind() == true)
                    {
                        player.rb.velocity = Vector2.left * heavyATKPushForce;
                    }
                    else
                    {
                        player.rb.velocity = Vector2.right * heavyATKPushForce;
                    }
                }
            }
        }
    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
