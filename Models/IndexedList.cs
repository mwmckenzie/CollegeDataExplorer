using System.Collections;
using System.Text.Json;

namespace CollegeDataExplorer.Models; 

public abstract class IndexedList : IList {

    public List<IndexedValue> list { get; set; } = new();

    private readonly Dictionary<string, int> _valueToIdDict = new();
    private readonly Dictionary<int, string> _idToValueDict = new();

    private int _maxIndex;
    protected string _typeName;

    public int nextIndex => _maxIndex + 1;

    // public bool ContainsValue(string value) {
    //     return _valueToIdDict.ContainsKey(value);
    // }

    public void Add(IndexedValue indexedValue) {
        if (list.Contains(indexedValue) || IsInvalid(indexedValue)) {
            return;
        }
        list.Add(indexedValue);
        _valueToIdDict.TryAdd(indexedValue.value, indexedValue.id);
        _idToValueDict.TryAdd(indexedValue.id, indexedValue.value);
        _maxIndex = Math.Max(_maxIndex, indexedValue.id);
    }

    private bool IsInvalid(IndexedValue indexedValue) {
        return indexedValue.typeName != _typeName || indexedValue.value is null;
    }
    

    public bool TryGetValue(int id, out string? value) {
        if (_idToValueDict.ContainsKey(id)) {
            value = _idToValueDict[id];
            return true;
        }
        value = null;
        return false;
    }
    
    public string? GetValue(int id) {
        return _idToValueDict.ContainsKey(id) ? _idToValueDict[id] : null;
    }

    public async Task TryLoad(string filepath) {
        using var openStream = File.OpenRead(filepath);
        var tempList = await JsonSerializer.DeserializeAsync<List<IndexedValue>>(openStream);
        if (tempList is null)
            return;
        list = tempList;
    }

    public IEnumerator GetEnumerator() {
        return list.GetEnumerator();
    }

    public void CopyTo(Array array, int index) {
        var i = index;
        var arrayCopy = new string[index + list.Count];
        for (int j = 0; j < index; j++) {
            arrayCopy[j] = (string) array.GetValue(j) ?? string.Empty;
        }
        foreach (var indexedValue in list) {
            arrayCopy[i] = indexedValue.value ?? string.Empty;
            i++;
        }
    }

    public int Count => list.Count;
    public bool IsSynchronized { get; }
    public object SyncRoot { get; }
    
    public int Add(object? value) {
        if (value is not IndexedValue indexedValue) return-1;
        Add(indexedValue);
        return IndexOf(indexedValue);
    }

    public void Clear() {
        list.Clear();
    }

    public bool Contains(object? value) {
        return value switch {
            IndexedValue indexedValue => indexedValue.value != null && _valueToIdDict.ContainsKey(indexedValue.value),
            string text => _valueToIdDict.ContainsKey(text),
            int num => _idToValueDict.ContainsKey(num),
            _ => false
        };
    }

    public int IndexOf(object? value) {
        return value switch {
            IndexedValue indexedValue => indexedValue.value == null ? -1 : _valueToIdDict[indexedValue.value],
            string text => _valueToIdDict.ContainsKey(text) ? _valueToIdDict[text] : -1,
            int num => num,
            _ => -1
        };
    }

    public void Insert(int index, object? value) {
        if (value is IndexedValue indexedValue) {
            list.Insert(index, indexedValue);
        }
    }

    public void Remove(object? value) {
        if (value is not IndexedValue indexedValue) return;
        if (list.Contains(indexedValue)) {
            list.Remove(indexedValue);
        }
    }

    public void RemoveAt(int index) {
        if (list.Count >= index) {
            return;
        }
        list.RemoveAt(index);
    }

    public bool IsFixedSize { get; }
    public bool IsReadOnly { get; }

    public object? this[int index] {
        get => list[index];
        set {
            if(value is IndexedValue indexedValue)  
                list[index] = indexedValue;
        }
    }
}