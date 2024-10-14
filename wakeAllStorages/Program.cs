namespace wakeAllStorages
{
    internal class Program
    {
        static void Main(string[] _)
        {
            List<StorageWaker> storageWakerList;
            var storageWakerDictionary = new Dictionary<string, StorageWaker>();

            while (true)
            {
                Console.WriteLine($"{DateTime.Now}");

                // 新たなWakerオブジェクトリストを作る。
                var newStorageWakerList = new List<StorageWaker>();
                var newStorageWakerDictionary = new Dictionary<string, StorageWaker>();

                // ドライブを列挙し、古いWakerオブジェクトに既に同じドライブ名のものがあればそれを新しいWakerオブジェクトリストに登録する。
                // なければ新たなWakerオブジェクトを生成し、リストに登録する。
                foreach (var driveInfo in DriveInfo.GetDrives())
                {
                    var driveName = driveInfo.Name;
                    var waker = storageWakerDictionary.GetValueOrDefault(driveName) ?? new StorageWaker(driveName);
                    newStorageWakerList.Add(waker);
                    newStorageWakerDictionary.Add(driveName, waker);
                }

                // 古いリスト、ディクショナリを破棄し、新しいもので置き換える。
                storageWakerList = newStorageWakerList;
                storageWakerDictionary = newStorageWakerDictionary;

                // 順番に、Wakerオブジェクトのwake()メソッドを呼び出す。
                foreach (var waker in storageWakerList)
                {
                    waker.Wake();
                }
                Console.WriteLine("");

                // 1分sleepする。
                Thread.Sleep(60 * 1000);
            }
        }
    }
}
