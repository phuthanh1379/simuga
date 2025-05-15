using Fusion;
using UnityEngine;

namespace Model.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        private Vector3 _velocity;
        private bool _jumpPressed;
        private bool _sprintPressed;
        
        private float Horizontal => Input.GetAxis("Horizontal");
        private float Vertical => Input.GetAxis("Vertical");
        private float SpeedMultiplier => _sprintPressed ? sprintMultiplier : 1f;

        [SerializeField] private CharacterController controller;
        [SerializeField] private float playerSpeed = 10f;
        [SerializeField] private float sprintMultiplier = 50f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float gravityValue = -9.81f;
        
        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _jumpPressed = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _sprintPressed = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _sprintPressed = false;
            }
        }

        /// <summary>
        /// FixedUpdateNetwork is only executed on the StateAuthority
        /// </summary>
        public override void FixedUpdateNetwork()
        {
            if (controller.isGrounded)
            {
                _velocity = new Vector3(0, -1, 0);
            }
            
            var move = new Vector3(Horizontal, 0, Vertical) * Runner.DeltaTime * playerSpeed * SpeedMultiplier;

            _velocity.y += gravityValue * Runner.DeltaTime;
            if (_jumpPressed && controller.isGrounded)
            {
                _velocity.y += jumpForce;
            }

            controller.Move(move + _velocity * Runner.DeltaTime);
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            _jumpPressed = false;
        }
    }
}