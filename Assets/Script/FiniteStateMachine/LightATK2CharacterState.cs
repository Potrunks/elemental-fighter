using System;

public class LightATK2CharacterState : CharacterState
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
            // Light ATK 2 Transition
            if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                return nextState = new LightATK2TransitionCharacterState();
            }
        }
        return nextState;
    }

    public override void OnEnter(MovePlayer player)
    {
        player.audioManager.Play("SwordAttack2");
        player.audioManager.PlaySoundByIndexInListOfSound(player.audioManager.lightATKSounds, random.Next(0, player.audioManager.lightATKSounds.Length));
        // Check if grounded
        if (player.isGrounding == true)
        {
            player.animator.Play("LightATK2");
        }
        else
        {
            // if not grounded, air light atk 2 animation
            player.animator.Play("AirAttack2");
        }
    }

    public override void OnExit(MovePlayer player)
    {

    }

    public override void PerformingInput(string action)
    {

    }
}
