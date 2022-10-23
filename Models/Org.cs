namespace CollegeDataExplorer.Models; 

public class Org: BaseInfoObj {
    public int? orgTypeId { get; set; }
    
    public override void Add(SumProgElements elementType, int value) {
        base.Add(elementType, value);
        switch (elementType) {
            case SumProgElements.OrgTypeId:
                    orgTypeId = value;
                    break;
        }
    }
}