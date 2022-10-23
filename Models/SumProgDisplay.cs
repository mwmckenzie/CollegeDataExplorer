namespace CollegeDataExplorer.Models; 

public class SumProgDisplay : DisplayObj {
    
    public List<string> topicList { get; set; } = new();
    public List<string> subjectList { get; set; } = new();
    public List<string> programTypeList { get; set; } = new();
    
    public List<string> orgIdList { get; set; } = new();
    public List<string> sessionIdList { get; set; } = new();
    public List<string> applicationIdList { get; set; } = new();
    public List<string> studentInfoIdList { get; set; } = new();
}