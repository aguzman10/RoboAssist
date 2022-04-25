using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class testSimulation : MonoBehaviour
{
    public Queue q;
    LinkedList<string> activityQueue = new LinkedList<string>(); // Linked list of movements user should perform
    LinkedList<string>.Enumerator node; // Enumerator to parse linked list
    SimulatedUser user; // Simulated user
    ActivityTimer timer = new ActivityTimer(); // Used to determine when to enter Not Playing State

    bool finished = false; // Determines if activity exercise is finished
    bool stoppedEarly = false; //Determines if the session should be stopped before the end is reached
    bool simulationRunning = false; // Determines if start button on GUI has been pressed
    int consecWrong = 0; // Number of consecutively wrong movements made
    string timestamp; // Timestamp that state is entered
    string message; // Message to be displayed

    string userPerformance = "Timestamp,Event\n"; //String to be added to for user performance csv file
    string runLog = "Runtime Log \n"; //String to be added to for the runtime log
    string log_path; //string for path of userlog files
    string runLog_path; //string for path of runlog files
    DateTime currentDay; //Current date and time for naming of log files

    public GameObject mainMenu; // points to "TopTools" in the hierarchy
    public GameObject playMenu; // points to "TopPlayTools" in the hierarchy

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

            mainMenu.SetActive(false);
            playMenu.SetActive(true);

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

            //Logging
            userPerformance = "Timestamp,Event\n";
            runLog = "Runtime Log \n";
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
                runLog = runLog + (timestamp + " Not Playing State\n"); //runLog
                message = "Please perform a movement.";
                timer.ResetTimer();
            }
            else // Stable State
            {
                GetTimeStamp();
                runLog = runLog + (timestamp + " Stable State\n"); //runLog
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
                            runLog = runLog + (timestamp + " Playing Well State\n"); //runLog
                            userPerformance = userPerformance + (timestamp + ",Attempt: Correct\n"); //CSV Logging
                            runLog = runLog + (timestamp + " Attempt: Correct\n"); //runLog
                            message = "Correct! Keep up the good work!";
                        }
                        else // Correct Movement State
                        {
                            GetTimeStamp();
                            runLog = runLog + (timestamp + " Correct Movement State\n"); //runLog
                            userPerformance = userPerformance + (timestamp + ",Attempt: Correct\n"); //CSV Logging
                            runLog = runLog + (timestamp + " Attempt: Correct\n"); //runLog
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
                            runLog = runLog + (timestamp + " Playing Poorly State\n"); //runLog
                            userPerformance = userPerformance + (timestamp + ",Attempt: Incorrect\n"); //CSV Logging
                            userPerformance = userPerformance + (timestamp + ",Reprompting\n"); //CSV Logging
                            runLog = runLog + (timestamp + " Attempt: Incorrect\n"); //runLog
                            runLog = runLog + (timestamp + " Reprompting\n"); //runLog
                            message = "Incorrect! Don't give up!";
                        }
                        else // Incorrect Movement State
                        {
                            GetTimeStamp();
                            runLog = runLog + (timestamp + " Incorrect Movement State\n"); //runLog
                            userPerformance = userPerformance + (timestamp + ",Attempt: Incorrect\n"); //CSV Logging
                            userPerformance = userPerformance + (timestamp + ",Reprompting\n"); //CSV Logging
                            runLog = runLog + (timestamp + " Attempt: Incorrect\n"); //runLog
                            runLog = runLog + (timestamp + " Reprompting\n"); //runLog
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
                runLog = runLog + (timestamp + " Finished State\n"); //runLog
                simulationRunning = false;
                Time.timeScale = 0f;
                timer.StopTimer();
                DisplayMessage();
                if (!stoppedEarly)
                    CongratulatoryBehavior();
                else
                    print("Activity finished.");

                print("Number of correct movements: " + user.GetNumCorrect());
                print("Number of incorrect movements: " + user.GetNumIncorrect());
                runLog = runLog + (timestamp + " Correct Movements: " + user.GetNumCorrect() + "\n"); //runLog
                runLog = runLog + (timestamp + " Incorrect Movements: " + user.GetNumIncorrect() + "\n"); //runLog

                // Save user performance here
                userPerformance = userPerformance + ("Total Correct:," + user.GetNumCorrect() + "\n"); //CSV Logging
                userPerformance = userPerformance + ("Total Incorrect:," + user.GetNumIncorrect() + "\n"); //CSV Logging

                log_path = Application.dataPath + "/UserLogs/"; //placing userlogs in a folder
                runLog_path = Application.dataPath + "/RunLogs/"; //placing runlogs in a folder
                currentDay = DateTime.Now; //update date and time for file naming
                System.IO.File.WriteAllText(log_path + userName +" user performance " + currentDay.ToString("MM'-'dd'-'yyyy HH-mm-ss") + ".csv", userPerformance); //CSV Logging
                System.IO.File.WriteAllText(runLog_path + userName + " runlog " + currentDay.ToString("MM'-'dd'-'yyyy HH-mm-ss") + ".txt", runLog); //runLog

                mainMenu.SetActive(true);
                playMenu.SetActive(false);
            }
        }
    }

    // Stops simulation early
    public void stopSimulation()
    {
        Time.timeScale = 1f; // Resumes Activity if it is paused and goes to the finished state
        stoppedEarly = true;
        finished = true;
        simulationRunning = true;

    }

    // Pauses the queue
    public void pauseSimulation()
    {
        Debug.Log("Paused Simulation");

        Time.timeScale = 0f;
        timer.StopTimer();
        simulationRunning = false ;
    }

    // Resumes after a pause
    public void resumeSimulation()
    {
        Time.timeScale = 1f;
        timer.StartTimer();
        simulationRunning = true;

        DemonstrateMovement();
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
        GetTimeStamp();
        userPerformance = userPerformance + (timestamp + "," + "Prompted: " + node.Current + "\n"); //CSV Logging
    }

    // Function for robot demonstrating congratulatory behavior
    private void CongratulatoryBehavior()
    {
        print("You finished your exercise activity! Good job!");
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
