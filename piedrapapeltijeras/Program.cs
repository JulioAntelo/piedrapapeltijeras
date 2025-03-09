using piedrapapeltijeras.game;
using piedrapapeltijeras.participants;

class Program
{
    static void Main()
    {
        Participant player1 = new Participant("Player 1");
        Participant player2 = new Participant("Player 2");
        
        int rounds = 5;
        
        Jueguito jueguito = new Jueguito(player1, player2, rounds);

        jueguito.Play();
        
        Console.WriteLine("La partida ha finalizado");
    }
}