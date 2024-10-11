namespace wakeAllStorages
{
    internal class Program
    {
        static void Main(string[] _)
        {
            while (true)
            {
                Console.WriteLine($"{DateTime.Now}");
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (var drive in drives)
                {
                    // エントリーの列挙をすればドライブがスリープしないと思っている。(根拠はない。)
                    var entries = Directory.EnumerateFileSystemEntries(drive.Name).ToArray();
                    Console.WriteLine($"{drive.Name} : number of entries = {entries.Length}");
                }
                Console.WriteLine("");

                Thread.Sleep(60 * 1000);    // 1分間隔で実行する。
            }
        }
    }
}
