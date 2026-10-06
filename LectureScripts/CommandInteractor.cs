using MidtermTuringTest;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine.AI;

public class CommandInteractor : Interactor
{
    // - First In, First Out.  Will complete commands as we give it
    Queue<Command> commands = new Queue<Command>();

    // - Serialize fields for the NavMeshAgent, pointer prefab, and camera
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Camera cam;

    // - Current command being executed
    private Command currentCommand;

   
    public override void Interact()
    {
        Debug.Log("CommandInteractor");

        // - Checking to see if the player is pressing the command button
        if (PlayerInput.Instance.commandPressed)
        {
            Debug.Log("Command Pressed");

            // - Raycast from the center of the screen (camera) to the the point that we want to send the command to
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
            
            // - If the raycast hits something
            if (Physics.Raycast(ray, out var hitInfo))
            {
                // - if the raycast hits the ground
                if (hitInfo.transform.CompareTag("Ground"))
                {
                    // - Create a pointer object
                    GameObject pointer = Instantiate(pointerPrefab);
                    pointer.transform.position = hitInfo.point;

                    // - Add a new movement command to the queue of commands for the agent to execute.
                    commands.Enqueue(new MoveCommand(agent, hitInfo.point));                    
                }                 
            }
        }

        ProcessCommands();
    }

    // - Responsible for processing the commands in the queue one at a time
    void ProcessCommands()
    {
        // - If I current have a command and i'm not done it, then don't do anything new until I finish my first command
        if (currentCommand != null && !currentCommand.IsComplete)
        {
            Debug.Log("Current Command Not Complete");
            return;
        } 

        // - If I don't have any commands in my queue, then don't do anything
        if (commands.Count == 0)
        {
            Debug.Log("No Current Commands Provided");
            return;
        }
            

        // - Get rid of command that was just processed.
        currentCommand = commands.Dequeue();

        // - Execute the command that was just dequeued from the queue of commands
        currentCommand.Execute();
    }
}
