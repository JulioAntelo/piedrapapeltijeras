using piedrapapeltijeras.participants;

namespace piedrapapeltijeras.game;

public class Game
{
    private Participant _player1;
    private Participant _player2;
    private readonly int _rounds;
    private static readonly object LockObject = new object();
    private string _player1Option;
    private string _player2Option;

    public Game(Participant player1, Participant player2, int rounds)
    {
        _player1 = player1;
        _player2 = player2;
        _rounds = rounds;
    }

    
    public void Play()
    {
        int neededVictories = (_rounds / 2) + 1;
        int player1Victories = 0;
        int player2Victories = 0;

        Console.WriteLine($"{_player1.Name} VS {_player2.Name}");

        Thread thread1 = new Thread(() =>
        {
            while (player1Victories < neededVictories && player2Victories < neededVictories)
            {
                lock (LockObject)
                {
                    _player1Option = _player1.GetRandomOption();
                    Monitor.Pulse(LockObject);
                    Monitor.Wait(LockObject);
                }
            }
        });

        Thread thread2 = new Thread(() =>
        {
            while (player1Victories < neededVictories && player2Victories < neededVictories)
            {
                lock (LockObject)
                {
                    _player2Option = _player2.GetRandomOption();
                    Monitor.Pulse(LockObject);
                    Monitor.Wait(LockObject);
                }
            }
        });

        thread1.Start();
        thread2.Start();
        
        while (player1Victories < neededVictories && player2Victories < neededVictories)
        {
            lock (LockObject)
            {
                Monitor.Pulse(LockObject);
                Monitor.Wait(LockObject);
                
                string result = GetRoundWinner(_player1Option, _player2Option);
                if (result == _player1.Name)
                {
                    player1Victories++;
                }
                else if (result == _player2.Name)
                {
                    player2Victories++;
                }
                
                Console.WriteLine($"Marcador: {_player1.Name} {player1Victories} - {player2Victories} {_player2.Name}");
            }
        }

        thread1.Interrupt();
        thread2.Interrupt();
        
        Console.WriteLine(player1Victories > player2Victories
            ? $"{_player1.Name} es el ganador!"
            : $"{_player2.Name} es el ganador!");
        
    }

    
    private string GetRoundWinner(string player1Option, string player2Option)
    {
        if ((player1Option == "rock" && player2Option == "scissors") ||
            (player1Option == "paper" && player2Option == "rock") ||
            (player1Option == "scissors" && player2Option == "paper"))
        {
            return _player1.Name;
        }
        else if (player1Option == player2Option)
        {
            return "Draw";
        }
        else
        {
            return _player2.Name;
        }
        
    }
    
}