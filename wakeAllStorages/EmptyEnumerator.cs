using System.Collections;

namespace wakeAllStorages
{
    public sealed partial class StorageWaker
    {
        class EmptyEnumerator : IEnumerator<string>
        {
            public string Current => string.Empty;
            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext() => false;

            public void Reset() { }
        }
    }
}
