using UnityEngine;
using UnityEngine.InputSystem;

namespace MidtermTuringTest
{
    [DefaultExecutionOrder(-100)]
    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance { get; private set; }

        public float horizontalInput { get; private set; }
        public float verticalInput { get; private set; }
        public float mouseX { get; private set; }
        public float mouseY { get; private set; }

        public bool sprintHeld { get; private set; }
        public bool jumpPressed { get; private set; }
        public bool activatePressed { get; private set; }
        public bool primaryShootPressed { get; private set; }
        public bool secondaryShootPressed { get; private set; }

        // - STRATEGY PATTERN - allows for different types of shooting
        public bool alpha1Pressed { get; private set; }
        public bool alpha2Pressed { get; private set; }

        // - COMMAND PATTERN - allows for queuing up commands for the player to execute
        public bool commandPressed { get; private set; }


        [Header("References")]
        [SerializeField] InputActionReference moveAction;
        [SerializeField] InputActionReference lookAction;
        [SerializeField] InputActionReference sprintAction;
        [SerializeField] InputActionReference jumpAction;
        [SerializeField] InputActionReference activateAction;
        [SerializeField] InputActionReference primaryShootAction;
        [SerializeField] InputActionReference secondaryShootAction;

        // - STRATEGY PATTERN - allows for different types of shooting
        [SerializeField] InputActionReference alpha1Action;
        [SerializeField] InputActionReference alpha2Action;

        // - COMMAND PATTERN - allows for queuing up commands for the player to execute
        [SerializeField] InputActionReference commandAction;
        

        private bool clear;

        private void Awake()
        {
            // - SINGLETON PATTERN - Set the singleton static reference if it has not been set yet.
            if (Instance == null)
            {
                Instance = this;
            }

            // - SINGLETON PATTERN - Destroy any other copies of our singleton class reference
            else if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            
        }

        /// <summary>
        /// When player presses button on the keyboard/controller/etc. the game is listening for that input
        /// </summary>
        private void OnEnable()
        {
            moveAction.action.Enable();
            lookAction.action.Enable();

            sprintAction.action.Enable();
            jumpAction.action.Enable();
            activateAction.action.Enable();

            primaryShootAction.action.Enable();
            secondaryShootAction.action.Enable();

            // - STRATEGY PATTERN - enables the input actions for alpha1 and alpha2
            alpha1Action.action.Enable();
            alpha2Action.action.Enable();

            // - COMMAND PATTERN - allows for queuing up commands for the player to execute
            commandAction.action.Enable();
        }

        private void OnDisable()
        {
            moveAction.action.Disable();
            lookAction.action.Disable();

            sprintAction.action.Disable();
            jumpAction.action.Disable();
            activateAction.action.Disable();

            primaryShootAction.action.Disable();
            secondaryShootAction.action.Disable();

            // - STRATEGY PATTERN - disables the input actions for alpha1 and alpha2
            alpha1Action.action.Disable();
            alpha2Action.action.Disable();

            // - COMMAND PATTERN - allows for queuing up commands for the player to execute
            commandAction.action.Disable();
        }

        private void Update()
        {
            ClearInputs();
            ProcessInputs();

        }

        //clearing inputs at the end of the frame
        private void LateUpdate()
        {
            clear = true;
        }

        private void ProcessInputs()
        {
            Vector2 move = moveAction.action.ReadValue<Vector2>();
            Vector2 look = lookAction.action.ReadValue<Vector2>();

            horizontalInput = move.x;
            verticalInput = move.y;

            mouseX = look.x;
            mouseY = look.y;

            sprintHeld = sprintAction.action.IsPressed();
            jumpPressed |= jumpAction.action.WasPressedThisFrame(); // | is "Or" operator
            activatePressed |=activateAction.action.WasPressedThisFrame();

            primaryShootPressed |= primaryShootAction.action.WasPressedThisFrame();
            secondaryShootPressed |= secondaryShootAction.action.WasPressedThisFrame();


            // - STRATEGY PATTERN - checks if alpha1 or alpha2 actions were pressed this frame and updates the corresponding boolean values
            alpha1Pressed |= alpha1Action.action.WasPressedThisFrame();
            alpha2Pressed |= alpha2Action.action.WasPressedThisFrame();

            // - COMMAND PATTERN - allows for queuing up commands for the player to execute
            commandPressed |= commandAction.action.WasPressedThisFrame();

        }


        private void ClearInputs()
        {
            if (!clear)
            {
                return;
            }
            horizontalInput = 0;
            verticalInput = 0;
            mouseX = 0;
            mouseY = 0;

            sprintHeld = false;
            jumpPressed = false;
            activatePressed = false;

            primaryShootPressed = false;
            secondaryShootPressed = false;

            // - STRATEGY PATTERN - clears the inputs for alpha1 and alpha2
            alpha1Pressed = false;
            alpha2Pressed = false;
            
            // - COMMAND PATTERN - allows for queuing up commands for the player to execute
            commandPressed = false;


            clear = false;
        }


    }
}
