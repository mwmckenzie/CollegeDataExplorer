namespace CollegeDataExplorer.Models; 

public class StudentInfoDisplay : ProgramComponentDisplay {
    
    public int ageMin { get; set; }
    public int ageMax { get; set; }
    public int classYearMin { get; set; }
    public int classYearMax { get; set; }

    public List<string> citizenshipList { get; set; } = new();
    public List<string> residenceList { get; set; } = new();
    
}