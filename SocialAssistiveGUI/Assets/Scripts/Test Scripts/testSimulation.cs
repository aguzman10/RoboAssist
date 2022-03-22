using System.Collections.Generic;
using System;
using UnityEngine;

public class testSimulation : MonoBehaviour
{
    System.Random rnd = new System.Random(); // Namespace conflict between UnityEngine and System, specify System.Random

    LinkedList<string> activityQueue = new LinkedList<string>(); // Linked list of movements user should perform
    LinkedList<string>.Enumerator node; // Enumerator to parse linked list
    SimulatedUser user; // Simulated user
    ActivityTimer timer = new ActivityTimer(); // Used to determine when to enter Not Playing State

    bool finished = false; // Determines if activity exercise is finished
    int consecWrong = 0; // Number of consecutively wrong movements made
    double chance; // Used to determine if simulated user should perform a movement or not
    string timestamp; // Timestamp that state is entered
    string message; // Message to be displayed
    bool notPlayingVisited = false; // To trigger Playing Poorly

    public float waitTime = 2.0f; // Threshold to trigger Not Playing
    public string userName = "Misty"; // Name of simulated user
    public double notPlayingChance = 0.25f; // Chance the simulated user does not make a move in time
    public int wellThreshold = 3; // Threshold to trigger Playing Well
    public int poorlyThreshold = 3; // Threshold to trigger Playing Poorly
    public int maxCycles = 5; // Maximum number of times the do while loop will execute (prevents infinite loop in when trying to test Not Playing State)

    // Start is called before the first frame update
    // Start State
    void Start()
    {
        // Initialize linked list
        activityQueue.AddLast("right arm up");
        activityQueue.AddLast("right arm down");
        activityQueue.AddLast("left arm up");
        activityQueue.AddLast("left arm down");
        node = activityQueue.GetEnumerator();
        node.MoveNext();
        
        user = new SimulatedUser(userName);
        user.targetMovement = node.Current;

        GetTimeStamp();
        timer.StartTimer();

        
    }

    // Called once per frame
    private void Update()
    {
        if (timer.IsRunning())
        {
            timer.elapsedTime += Time.deltaTime;
        }
    }

    // LateUpdate() called after Update() finishes
    private void LateUpdate()
    {
        if (notPlayingVisited == false) // Ensure Not Playing is visited
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
                    //timer.StopTimer(); // Stop timer -> May not need to stop the timer until activity is  completely finished (?)
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
        if(!finished)
            DisplayMessage(message); // Display Message State

        if (finished) // Finished State
        {
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
    void GetTimeStamp()
    {
        timestamp = DateTime.Now.ToString("HH:mm:ss:f"); // Hours:Minutes:Seconds:Tenths of a second
    }

    // Display State
    void DisplayMessage(string message) {
        GetTimeStamp();
        print(message);  
    }


}