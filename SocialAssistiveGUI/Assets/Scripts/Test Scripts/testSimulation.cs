using System.Collections.Generic;
using System;
using UnityEngine;

public class testSimulation : MonoBehaviour
{
    public Queue q;
    LinkedList<string> activityQueue = new LinkedList<string>(); // Linked list of movements user should perform
    LinkedList<string>.Enumerator node; // Enumerator to parse linked list
    SimulatedUser user; // Simulated user
    ActivityTimer timer = new ActivityTimer(); // Used to determine when to enter Not Playing State

    bool finished = false; // Determines if activity exercise is finished
    bool simulationRunning = false; // Determines if start button on GUI has been pressed
    int consecWrong = 0; // Number of consecutively wrong movements made
    string timestamp; // Timestamp that state is entered
    string message; // Message to be displayed

    public float waitTime = 2.0f; // Threshold time to trigger Not Playing
    public string userName = "Misty"; // Name of simulated user
    public int wellThreshold = 3; // Threshold to trigger Playing Well
    public int poorlyThreshold = 3; // Threshold to trigger Playing Poorly
    public bool userIsPlaying = false; // Flag to help determine if simulated user should peform a movement (toggable in Unity editor)
    
    // Start is called before the first frame update
    public void Start()
    {
        Time.timeScale = 0f; // Used to prevent FixedUpdate() from running before StartSimulation() is executed
    }

    // Start State
    public void StartSimulation()
    {

        if (! q.isEmpty()){
            GetTimeStamp(); // Get timestamp for when start state is entered

            // Initialize linked list
            activityQueue = q.CopyCurrent();
            node = activityQueue.GetEnumerator();
            node.MoveNext();

            // Initialize variables
            user = new SimulatedUser(userName, playing: userIsPlaying);
            user.targetMovement = node.Current;
            Time.timeScale = 1f; // Allows FixedUpdate() to run
            simulationRunning = true;
            message = "";
            finished = false;

            // Start activity timer and have the robot perform the first movement the user should perform
            timer.StartTimer();
            DemonstrateMovement();
        }
        else {
            Debug.Log("Empty Queue!");
        }
    }

    // Called once per frame (used to keep track of how much time has passed since user has performed a movement)
    private void Update()
    {
        if (timer.IsRunning() && simulationRunning)
        {
            timer.elapsedTime += Time.deltaTime;
        }
    }

    // FixedUpdate() will execute once on application startup, and then stop whenever Time.timeScale is 0
    private void FixedUpdate()
    {
        if (simulationRunning)
        {
            user.isPlaying = userIsPlaying; // Update isPlaying in case it's changed during run time by administrator

            if (timer.elapsedTime > waitTime) // Not playing state
            {
                GetTimeStamp();
                message = "Please perform a movement.";
                timer.ResetTimer();
            }
            else // Stable State
            {
                GetTimeStamp();
                GetUserMovement(); // Get user movement
                if (user.chosenMovement != "no movement") // User should perform a movement
                {
                    timer.ResetTimer(); // Reset timer once user has performed a movement
                    if (user.chosenMovement == node.Current) // User performed correct movement
                    {
                        consecWrong = 0; // Reset the number of conescutively wrong movements the user has performed
                        user.IncrementCorrect(); // Increment the number of correct movements the user has performed by 1

                        if (user.GetNumCorrect() % wellThreshold == 0) // Playing Well State
                        {
                            GetTimeStamp();
                            message = "Correct! Keep up the good work!";
                        }
                        else // Correct Movement State
                        {
                            GetTimeStamp();
                            message = "Correct!";
                        }
                        if (!node.MoveNext()) // Move to next node in linked list
                            finished = true; // If no more nodes in linked list, acitivity exercise finished
                        else
                            user.targetMovement = node.Current; // Set user's target movement to the new motion to be performed
                    }
                    else // User performed an incorrect movement
                    {
                        consecWrong++; // Increment the number of consecutively wrong movements the user has performed
                        user.IncrementIncorrect(); // Increment the number of incorrect movements the user has performed by 1

                        if (consecWrong >= poorlyThreshold) // Playing Poorly State
                        {
                            GetTimeStamp();
                            message = "Incorrect! Don't give up!";
                        }
                        else // Incorrect Movement State
                        {
                            GetTimeStamp();
                            message = "Incorrect!";
                        }
                    }
                }
            }

            if (!finished && message != "") // Display State
            {
                GetTimeStamp();
                DisplayMessage(); // Display Message State
                DemonstrateMovement(); // Robot demonstrates next motion to be performed
            }

            if (finished) // Finished State
            {
                GetTimeStamp();
                simulationRunning = false;
                Time.timeScale = 0f;
                timer.StopTimer();
                DisplayMessage();
                CongratulatoryBehavior();

                // Save user performance here

            }
        }
    }

    // Function for robot to detect user movement
    private void GetUserMovement()
    {
        user.GenerateMovement(); // Simulates getting user input
    }

    // Function for robot to demonstrate which motion the user should perform
    private void DemonstrateMovement()
    {
        print("Try to perform this movement: " + node.Current);
    }

    // Function for robot demonstrating congratulatory behavior
    private void CongratulatoryBehavior()
    {
        print("You finished your exercise activity! Good job!");
        print("Number of correct movements: " + user.GetNumCorrect());
        print("Number of incorrect movements: " + user.GetNumIncorrect());
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
