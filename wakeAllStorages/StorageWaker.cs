namespace wakeAllStorages
{
    public sealed class StorageWaker(string driveName)
    {
        readonly string _driveName = driveName;
        readonly Stack<IEnumerator<string>> _directoryNameEnumeratorStack = new();

        public void Wake()
        {
            while (_directoryNameEnumeratorStack.Count > 0)
            {
                var en = _directoryNameEnumeratorStack.Peek()!;
                while (en.MoveNext())
                {
                    try
                    {
                        // 現在のディレクトリに、未処理のサブディレクトリがある場合はそれを一つ処理して終了する。
                        var en2 = GetEnumerator(en.Current);
                        _directoryNameEnumeratorStack.Push(en2);
                        return;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("  access denied.");
                    }
                }

                // 現在のディレクトリのサブディレクトリはすべて処理したので、一つ上のディレクトリに戻る。
                _directoryNameEnumeratorStack.Pop();
            }

            // ルートのサブディレクトリもすべて処理したので、ルートのディレクトリを処理する。
            _directoryNameEnumeratorStack.Push(GetEnumerator(_driveName));
        }

        static IEnumerator<string> GetEnumerator(string directoryPath)
        {
            Console.WriteLine($"{directoryPath}");
            return Directory.EnumerateDirectories(directoryPath).GetEnumerator();
        }
    }
}
