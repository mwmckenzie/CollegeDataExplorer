namespace CollegeDataExplorer.Models; 

public abstract class IndexedValue : IEquatable<IndexedValue> {

    public int id { get; set; }
    public string? value { get; set; }

    protected string _typeName;
    public string? typeName => _typeName;

    public bool Equals(IndexedValue? other) {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(_typeName, other._typeName, StringComparison.OrdinalIgnoreCase) && id == other.id && string.Equals(value, other.value, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((IndexedValue) obj);
    }

    public override int GetHashCode() {
        var hashCode = new HashCode();
        hashCode.Add(_typeName, StringComparer.OrdinalIgnoreCase);
        hashCode.Add(id);
        hashCode.Add(value, StringComparer.OrdinalIgnoreCase);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(IndexedValue? left, IndexedValue? right) {
        return Equals(left, right);
    }

    public static bool operator !=(IndexedValue? left, IndexedValue? right) {
        return!Equals(left, right);
    }
}