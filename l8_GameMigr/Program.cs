
using l8_GameMigr;
using l8_GameMigr.Classes;

using (GmContext db = new GmContext())
{
    var data = db.Games.ToList();

    foreach(Game game in data)
    {
        Console.WriteLine(game + "\n");
    }
}