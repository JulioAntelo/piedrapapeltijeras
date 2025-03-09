namespace piedrapapeltijeras.participants;

public class Participant
{
    
    public string Name { get; set; }
    private List<string> options = new List<string>{"rock", "paper", "scissors"};


    public Participant(string name)
    {
        Name = name;
    }
    

    public string GetRandomOption()
    {
        Random random = new Random();
        int index = random.Next(options.Count);
        return options[index];
    }
    
}