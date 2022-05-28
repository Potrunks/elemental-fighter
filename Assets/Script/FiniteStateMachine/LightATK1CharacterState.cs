using System;

public class LightATK1CharacterState : CharacterState
{
    private ICharacterState nextState;
    private Random random = new Random();

    public override ICharacterState CheckingStateModification(MovePlayer player)
    {
        // hurt state
        if (player.isHurting == true)
        {
            return nextState = new HurtCharacterState();
        }
        else
        {
            // Light ATK 1 Transition
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                return nextState = new LightATK1TransitionCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.audioManager.Play("SwordAttack1");
        player.audioManager.PlaySoundByIndexInListOfSound(player.audioManager.lightATKSounds, random.Next(0, player.audioManager.lightATKSounds.Length));
        // Check if grounded
        if (player.isGrounding == true)
        {
            // if grounded, Disable Player Velocity
            player.moveSpeed = player.stopMoveSpeed;
            player.animator.Play("LightATK1");
        }
        else
        {
            // if not grounded, air light atk 1 animation
            player.animator.Play("AirAttack1");
        }
    }

    public override void OnExit(MovePlayer player)
    {
        
    }

    public override void PerformingInput(string action)
    {

    }
}