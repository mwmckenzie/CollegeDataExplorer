namespace CollegeDataExplorer.Models; 

public class Session : DateRangeObj {
    public List<string> applicationIdList { get; set; } = new();
}