using Platformer.Core;
using Platformer.Mechanics;

namespace Mechanics
{
    public class MyCharacterJump : Simulation.Event<MyCharacterJump>
    {
        public KinematicObject _myCharacterController;

        private float characterJumpVelocity;

        public override void Execute()
        {

            if (_myCharacterController.GetType() == typeof(PlayerController))
            {
                PlayerJump();
            }
            else
            {
                CharacterJump();
            }
        }

        private void CharacterJump()
        {
            if (characterJumpVelocity < 0)
            {
                ((MyCharacterController)_myCharacterController).Jump();
            }
            else
            {
                ((MyCharacterController)_myCharacterController).Jump(characterJumpVelocity);
            }
        }

        private void PlayerJump()
        {
            if (characterJumpVelocity < 0)
            {
                ((PlayerController)_myCharacterController).Jump();
            }
            else
            {
                ((PlayerController)_myCharacterController).Jump(characterJumpVelocity);
            }
        }
    }
}
