// Simulates a User
public class SimulatedUser
{
    private string _name;
    private int _numCorrect; // Number of correct movements
    private int _numIncorrect; // Number of incorrect movements
    private string[] _movements = new string[] { "LookLeft", "LookUp", "LookDown", "LookRight", "LArmOut",
                                                 "LArmAcross", "RArmAcross", "RArmOut", "LArmForward", "RArmForward", 
                                                 "LArmUp", "RArmUp", "LArmBend", "RArmBend", "LLegMoveStanding", "RLegMoveStanding", 
                                                 "LLegMoveSitting", "RLegMoveSitting" }; // Possible movements user can make

    public string targetMovement; // Movement user should perform
    public string chosenMovement; // Movement the user did perform
    public double incorrectChance = 0.5; // Chance of generating a wrong answer (user tunable)

    public SimulatedUser(string name)
    {
        _name = name;
        _numCorrect = 0;
        _numIncorrect = 0;
    }

    // Randomly generates correct/incorrect user movement input
    public void GenerateMovement()
    {
        System.Random rnd = new System.Random();
        double chance = rnd.NextDouble();

        if (chance < incorrectChance)
        {
            do
                chosenMovement = _movements[rnd.Next(_movements.Length)]; // Generate random wrong movement
            while (chosenMovement == targetMovement);
            _numIncorrect++;
        }
        else
        { 
            chosenMovement = targetMovement; // Generate the correct movement
            _numCorrect++;
        }
    }

    // Getters
    public int GetNumCorrect() { return _numCorrect; }
    public int GetNumIncorrect() { return _numIncorrect; }
    public string GetName() { return _name; }

}
