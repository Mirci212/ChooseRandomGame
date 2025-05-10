using System.Diagnostics;

string path = "C:\\Users\\Marco\\Desktop\\Games";
do
{
    Console.Clear();
    Console.WriteLine(".: Random Game Chooser :.");
    string[] games = Directory.GetFiles(path);
    int rand = Random.Shared.Next(0, games.Length);
    Console.WriteLine();
    Console.WriteLine($"Heutiges Game: {games[rand]}");
    Console.Write("Willst du es starten?[y/n] ");
    if (char.ToLower(Console.ReadKey().KeyChar) == 'y')
    {
        string targetName = GetShortcutTarget(games[rand]);
        Process.Start(targetName,$"Start {games[rand]}");
        break;
    }
    Console.Write("\nReroll?[y/n] ");
} while (char.ToLower(Console.ReadKey().KeyChar) != 'n');

static string GetShortcutTarget(string shortcutPath)
{
    string targetPath = string.Empty;

    try
    {
        IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
        IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);

        targetPath = shortcut.TargetPath;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Fehler beim Ermitteln des Verknüpfungsziels: {ex.Message}");
    }

    return targetPath;
}