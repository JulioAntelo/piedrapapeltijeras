using piedrapapeltijeras.participants;

namespace piedrapapeltijeras.game;

public class Jueguito
{
    private Participant jugadoh1;
    private Participant jugadoh2;
    private readonly int rounds;
    private static readonly object LockObject = new object();
    private string jugadoh1sacar;
    private string jugadoh2sacar;

    public Jueguito(Participant player1, Participant player2, int rounds)
    {
        player1 = player1;
        player2 = player2;
        rounds = rounds;
    }

    
    public void Play()
    {
        int neededVictories = (rounds / 2) + 1;
        int victoriasjugador1 = 0;
        int victoriasjugador2 = 0;

        Console.WriteLine($"{jugadoh1.Name} VS {jugadoh2.Name}");

        Thread thread1 = new Thread(() =>
        {
            while (victoriasjugador1 < neededVictories && victoriasjugador2 < neededVictories)
            {
                lock (LockObject)
                {
                    jugadoh1sacar = jugadoh1.GetRandomOption();
                    Monitor.Pulse(LockObject);
                    Monitor.Wait(LockObject);
                }
            }
        });

        Thread thread2 = new Thread(() =>
        {
            while (victoriasjugador1 < neededVictories && victoriasjugador2 < neededVictories)
            {
                lock (LockObject)
                {
                    jugadoh2sacar = jugadoh2.GetRandomOption();
                    Monitor.Pulse(LockObject);
                    Monitor.Wait(LockObject);
                }
            }
        });

        thread1.Start();
        thread2.Start();
        
        while (victoriasjugador1 < neededVictories && victoriasjugador2 < neededVictories)
        {
            lock (LockObject)
            {
                Monitor.Pulse(LockObject);
                Monitor.Wait(LockObject);
                
                string result = GetRoundWinner(jugadoh1sacar, jugadoh2sacar);
                if (result == jugadoh1.Name)
                {
                    victoriasjugador1++;
                }
                else if (result == jugadoh2.Name)
                {
                    victoriasjugador2++;
                }
                
                Console.WriteLine($"Marcador: {jugadoh1.Name} {victoriasjugador1} - {victoriasjugador2} {jugadoh2.Name}");
            }
        }

        thread1.Interrupt();
        thread2.Interrupt();
        
        Console.WriteLine(victoriasjugador1 > victoriasjugador2
            ? $"{jugadoh1.Name} es el ganador!"
            : $"{jugadoh2.Name} es el ganador!");
        
    }

    
    private string GetRoundWinner(string jugadoh1sacar, string jugadoh2sacar)
    {
        if ((jugadoh1sacar == "rock" && jugadoh2sacar == "scissors") ||
            (jugadoh1sacar == "paper" && jugadoh2sacar == "rock") ||
            (jugadoh1sacar == "scissors" && jugadoh2sacar == "paper"))
        {
            return jugadoh1.Name;
        }
        else if (jugadoh1sacar == jugadoh2sacar)
        {
            return "EMPATE";
        }
        else
        {
            return jugadoh2.Name;
        }
        
    }
    
}