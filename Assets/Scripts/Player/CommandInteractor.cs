using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

namespace Midterm
{
    public class CommandInteractor : Interactor
    {
        Queue<Command> commands = new Queue<Command>();

        [SerializeField] private FriendController friend;
        [SerializeField] private GameObject pointerPrefab;
        [SerializeField] private Camera cam;

        [SerializeField] private LayerMask commandRaycastLayers;

        private Command currentCommand;

        public override void Interact()
        {
            if (PlayerInput.Instance.commandPressed)
            {
                Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

                if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, commandRaycastLayers))
                {
                    if (hitInfo.transform.CompareTag("Ground"))
                    {
                        GameObject pointer = Instantiate(pointerPrefab);
                        pointer.transform.position = hitInfo.point;

                        commands.Enqueue(new MoveCommand(friend, hitInfo.point, pointer));
                        AudioManager.Instance.PlaySFX(AudioManager.Instance.moveCommand);
                    }
                }
            }

            ProcessCommands();
        }

        void ProcessCommands()
        {
            if (currentCommand != null && !currentCommand.isComplete)
            {
               return; 
            }

            if (commands.Count == 0)
            {
                return;
            }

            Debug.Log("Processing next command");

            // Get rid of command that was just processed
            currentCommand = commands.Dequeue();

            // Execute the command that was just dequeued from the queue of commands
            currentCommand.Execute();
            
        }

    }
}
