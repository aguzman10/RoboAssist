using System.Collections.Generic;
using System;
using UnityEngine;

public class testSimulation : MonoBehaviour
{
    System.Random rnd = new System.Random(); // Namespace conflict between UnityEngine and System, specify System.Random
    
    public Queue q;

    LinkedList<string> activityQueue = new LinkedList<string>(); // Linked list of movements user should perform
    LinkedList<string>.Enumerator node; // Enumerator to parse linked list
    SimulatedUser user; // Simulated user
    ActivityTimer timer = new ActivityTimer(); // Used to determine when to enter Not Playing State

    bool finished = false; // Determines if activity exercise is finished
    bool simulationRunning = false; // Determines if start button on GUI has been pressed
    bool notPlayingVisited = false; // To trigger Playing Poorly
    int consecWrong = 0; // Number of consecutively wrong movements made
    double chance; // Used to determine if simulated user should perform a movement or not
    string timestamp; // Timestamp that state is entered
    string message; // Message to be displayed

    public float waitTime = 2.0f; // Threshold to trigger Not Playing
    public string userName = "Misty"; // Name of simulated user
    public double notPlayingChance = 0.25f; // Chance the simulated user does not make a move in time
    public int wellThreshold = 3; // Threshold to trigger Playing Well
    public int poorlyThreshold = 3; // Threshold to trigger Playing Poorly
    public int maxCycles = 5; // Maximum number of times the do while loop will execute (prevents infinite loop in when trying to test Not Playing State)
    
    // Start is called before the first frame update
    // Start State
    public void Start()
    {
        Time.timeScale = 0f;
    }
    public void StartSimulation()
    {
        // Initialize linked list
        activityQueue = q.CopyCurrent();
        node = activityQueue.GetEnumerator();
        node.MoveNext();
        
        user = new SimulatedUser(userName);
        user.targetMovement = node.Current;

        //GetTimeStamp();
        timer.StartTimer();
        Time.timeScale = 1f;
        simulationRunning = true;
        message = "";
        finished = false;
    }

    // Called once per frame
    private void Update()
    {
        if (timer.IsRunning() && simulationRunning)
        {
            timer.elapsedTime += Time.deltaTime;
        }
    }

    // LateUpdate() called after Update() finishes
    private void FixedUpdate()
    {
        
        if (notPlayingVisited == false) // Ensure Not Playing is visited at least once for testing purposes
        {
            print("waiting");
            if (timer.elapsedTime > waitTime) 
            { 
                GetTimeStamp();
                message = "Please perform a movement.";
                notPlayingVisited = true;
                timer.ResetTimer();
            }
        }
        else
        {
            if (timer.elapsedTime > waitTime) // Not playing state
            {
                GetTimeStamp();
                message = "Please perform a movement.";
                notPlayingVisited = true;
                timer.ResetTimer();

            }
            else // Stable State
            {
                GetTimeStamp();
                chance = rnd.NextDouble();
                if (chance > notPlayingChance) // User should perform a movement
                {
                    user.GenerateMovement(); // Perform a movement
                    timer.ResetTimer(); // Reset timer

                    if (user.chosenMovement == user.targetMovement) // User performed correct movement
                    {
                        consecWrong = 0;

                        if (user.GetNumCorrect() % wellThreshold == 0) // Playing Well State
                        {
                            GetTimeStamp();
                            message = "Keep up the good work!";
                        }
                        else // Correct Movement State
                        {
                           GetTimeStamp();
                            message = "Correct!";
                        }
                        if (!node.MoveNext()) // Move to next node in linked list
                            finished = true; // If no more nodes in linked list, acitivity exercise finished
                    }
                    else // User performed an incorrect movement
                    {
                        consecWrong++;

                        if (consecWrong >= poorlyThreshold) // Playing Poorly State
                        {
                            GetTimeStamp();
                            message = "Don't give up!";
                        }
                        else // Incorrect Movement State
                        {
                            GetTimeStamp();
                            message = "Incorrect!";
                        }
                    }
                }
            }

        }
        if (!finished && simulationRunning && message != "") // Display State
        {
            GetTimeStamp();
            DisplayMessage(); // Display Message State
        }

        if (finished) // Finished State
        {
            Time.timeScale = 0f;
            GetTimeStamp();
            timer.StopTimer();
            print("You finished your exercise activity! Good job!");
            print("Number of correct movements: " + user.GetNumCorrect());
            print("Number of incorrect movements: " + user.GetNumIncorrect());

            // Save user performance here

            UnityEditor.EditorApplication.isPlaying = false; // Terminates program execution in Unity Editor
        }
    }

    

    // Grabs timestamp (should be called each time a state is entered)
    private void GetTimeStamp()
    {
        timestamp = DateTime.Now.ToString("HH:mm:ss:f"); // Hours:Minutes:Seconds:Tenths of a second
    }

    // Display message to debug console
    private void DisplayMessage() {
        print(message);
        message = "";
    }


}
