namespace CollegeDataExplorer.Models; 

public class StudentInfo : SumProgComponent {
    
    public List<string> sessionIdList { get; set; } = new();
    
    public int ageMin { get; set; }
    public int ageMax { get; set; }
    public int classYearMin { get; set; }
    public int classYearMax { get; set; }

    public List<int> citizenshipIdList { get; set; } = new();
    public List<int> residenceIdList { get; set; } = new();

    public override void Add(SumProgElements elementType, int value) {
        base.Add(elementType, value);
        switch (elementType) {
            case SumProgElements.CitizenshipId:
                if (!citizenshipIdList.Contains(value)) {
                    citizenshipIdList.Add(value);
                }
                break;
            case SumProgElements.ResidenceId:
                if (!residenceIdList.Contains(value)) {
                    residenceIdList.Add(value);
                }
                break;
        }
        
    }
    
}