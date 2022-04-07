// Simulates a User
public class SimulatedUser
{
    private string _name; // Name  of user performing the exercise activity
    private int _numCorrect; // Number of correct movements
    private int _numIncorrect; // Number of incorrect movements
    private string[] _movements = new string[] { "LookLeft", "LookUp", "LookDown", "LookRight", "LArmOut",
                                                 "LArmAcross", "RArmAcross", "RArmOut", "LArmForward", "RArmForward", 
                                                 "LArmUp", "RArmUp", "LArmBend", "RArmBend", "LLegMoveStanding", "RLegMoveStanding", 
                                                 "LLegMoveSitting", "RLegMoveSitting" }; // Possible movements user can make

    public string targetMovement; // Movement user should perform
    public string chosenMovement; // Movement the user did perform
    public double incorrectChance; // Chance of generating a wrong answer (user tunable)
    public double notPlayingChance; // Chance user doesn't generate any movement input
    public bool isPlaying; // Flag for if simulated user should generate a random movement

    public SimulatedUser(string name, double chanceIncorrect = 0.5f, double chanceNotPlaying = 0.25f, bool playing = true)
    {
        _name = name;
        _numCorrect = 0;
        _numIncorrect = 0;
        chosenMovement = "no movement";
        incorrectChance = chanceIncorrect;
        notPlayingChance = chanceNotPlaying;
        isPlaying = playing;
    }

    // Randomly generates correct/incorrect user movement input
    public void GenerateMovement()
    {
        System.Random rnd = new System.Random();
        double chance = rnd.NextDouble();

        if (!isPlaying || chance < notPlayingChance)
            chosenMovement = "no movement"; 
        else
        {
            chance = rnd.NextDouble();
            if (chance < incorrectChance)
            {
                do
                    chosenMovement = _movements[rnd.Next(_movements.Length)]; // Generate random wrong movement
                while (chosenMovement == targetMovement);
            }
            else
                chosenMovement = targetMovement; // Generate the correct movement
        }
    }

    // Getters
    public int GetNumCorrect() { return _numCorrect; }
    public int GetNumIncorrect() { return _numIncorrect; }
    public string GetName() { return _name; }

    // IncrementCorrect() should be called whenever it is determined that the simulated user performed a correct movement
    public void IncrementCorrect() { _numCorrect++; }

    // IncrementIncorrect() should be called whenever it is determined that the simulated user performed an incorrect movement
    public void IncrementIncorrect() { _numIncorrect++; }

}
