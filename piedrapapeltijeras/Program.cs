using piedrapapeltijeras.game;
using piedrapapeltijeras.participants;

class Program
{
    static void Main()
    {
        Participant player1 = new Participant("Player 1");
        Participant player2 = new Participant("Player 2");
        
        int rounds = 5;
        
        Game game = new Game(player1, player2, rounds);

        game.Play();
        
        Console.WriteLine("La partida ha finalizado");
    }
}