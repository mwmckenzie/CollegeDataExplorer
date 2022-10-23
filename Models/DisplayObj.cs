namespace CollegeDataExplorer.Models; 

public abstract class DisplayObj {
    
    public bool isValid { get; set; }
    public string? id { get; set; }
    public string? name { get; set; }
    public string? displayName { get; set; }
    public string? url { get; set; }
    
    public List<string> tagList { get; set; } = new();
    public List<string> aliasList { get; set; } = new();
    public List<string> urlList { get; set; } = new();
    public List<string> noteList { get; set; } = new();
    
}