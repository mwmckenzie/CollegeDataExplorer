
namespace CollegeDataExplorer.Models; 

public abstract class BaseInfoObj : IEquatable<BaseInfoObj> {
    
    public bool isValid { get; set; }
    public string? id { get; set; }
    public string? name { get; set; }
    public string? displayName { get; set; }
    public string? url { get; set; }
    public List<int> tagIdList { get; set; } = new();
    public List<string> aliasList { get; set; } = new();
    public List<string> urlList { get; set; } = new();
    public List<string> noteList { get; set; } = new();
    
    public virtual void Add(SumProgElements elementType, string value) {
        if (string.IsNullOrWhiteSpace(value))
                    return;
        switch (elementType) {
            case SumProgElements.Alias:
                if (!aliasList.Contains(value)) {
                    aliasList.Add(value);
                }
                break;
            case SumProgElements.Note:
                if (!noteList.Contains(value)) {
                    noteList.Add(value);
                }
                break;
            case SumProgElements.Url:
                var fixedUrl = BuildValidUrl(value);
                if (!urlList.Contains(fixedUrl)) {
                    if (string.IsNullOrWhiteSpace(url)) {
                        url = fixedUrl;
                    }
                    urlList.Add(fixedUrl);
                }
                break;
        }
    }
    
    public virtual void Add(SumProgElements elementType, int value) {
        switch (elementType) {
            case SumProgElements.TagId:
                if (!tagIdList.Contains(value)) {
                    tagIdList.Add(value);
                }
                break;
        }
    }

     private string BuildValidUrl(string urlText) {
        if (urlText.Contains("https://", StringComparison.OrdinalIgnoreCase)) {
            return urlText;
        }
        urlText = urlText.Replace("http://", string.Empty);
        return $"https://{urlText}";
    }


     public bool Equals(BaseInfoObj? other) {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return id == other.id;
     }

     public override bool Equals(object? obj) {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != this.GetType()) return false;
         return Equals((BaseInfoObj) obj);
     }

     public override int GetHashCode() {
         return(id != null ? id.GetHashCode() : 0);
     }

     public static bool operator ==(BaseInfoObj? left, BaseInfoObj? right) {
         return Equals(left, right);
     }

     public static bool operator !=(BaseInfoObj? left, BaseInfoObj? right) {
         return!Equals(left, right);
     }
     
}