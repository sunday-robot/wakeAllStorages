namespace wakeAllStorages
{
    public sealed partial class StorageWaker(string driveName)
    {
        readonly List<IEnumerator<string>> _directoryNameEnumeratorList = [new EmptyEnumerator()];
        readonly List<string> _directoryNameList = [driveName];

        public void Wake()
        {
            do
            {
                var en = _directoryNameEnumeratorList[^1];// _currentDepth];

                while (en.MoveNext())
                {
                    // 次のサブディレクトリの処理を行い、終了する。
                    try
                    {
                        var dn = en.Current;
                        Console.WriteLine($"{dn}");
                        var en2 = Directory.EnumerateDirectories(dn).GetEnumerator();
                        _directoryNameEnumeratorList.Add(en2);
                        _directoryNameList.Add(en.Current);
                        return;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("  access denied.");
                    }
                }

                // 現在のディレクトリでの処理は終わった。

                if (_directoryNameList.Count == 1)
                {
                    // 現在ルートにいる場合、ルートのディレクトリ一覧を取得する。
                    Console.WriteLine($"{_directoryNameList[0]}");

                    var en2 = Directory.EnumerateDirectories(_directoryNameList[0]).GetEnumerator();
                    _directoryNameEnumeratorList[0] = en2;
                    return;
                }

                // ルートではない場合は、一つ上のディレクトリを見に行く。
                _directoryNameEnumeratorList.RemoveAt(_directoryNameEnumeratorList.Count - 1);
                _directoryNameList.RemoveAt(_directoryNameList.Count - 1);
            } while (true);
        }
    }
}
