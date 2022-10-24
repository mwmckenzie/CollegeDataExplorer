using CollegeDataExplorer.Helpers;

namespace CollegeDataExplorer.Models;

public class SummerProgramObj : BaseInfoObj {
    public List<int> topicIdList { get; set; } = new();
    public List<int> subjectIdList { get; set; } = new();
    public List<int> programTypeIdList { get; set; } = new();
    public List<string> orgIdList { get; set; } = new();
    public List<string> sessionIdList { get; set; } = new();
    public List<string> applicationIdList { get; set; } = new();
    public List<string> studentInfoIdList { get; set; } = new();
    
    
    public IEnumerable<string> Subjects() {
        foreach (var index in subjectIdList) {
            yield return lookUps.GetSubject(index);
        }
    }
    
    public IEnumerable<string> Topics() {
        foreach (var index in topicIdList) {
            yield return lookUps.GetTopic(index);
        }
    }
    
    public IEnumerable<string> ProgramTypes() {
        foreach (var index in programTypeIdList) {
            yield return lookUps.GetProgramType(index);
        }
    }
    
    public override void Add(SumProgElements elementType, string value) {
        base.Add(elementType, value);
        switch (elementType) {
            case SumProgElements.SessionId:
                if (!sessionIdList.Contains(value)) sessionIdList.Add(value);
                break;
            case SumProgElements.ApplicationId:
                if (!applicationIdList.Contains(value)) applicationIdList.Add(value);
                break;
            case SumProgElements.StudentInfoId:
                if (!studentInfoIdList.Contains(value)) studentInfoIdList.Add(value);
                break;
            case SumProgElements.OrgId:
                if (!orgIdList.Contains(value)) orgIdList.Add(value);
                break;
        }
    }

    public override void Add(SumProgElements elementType, int value) {
        base.Add(elementType, value);
        switch (elementType) {
            case SumProgElements.Subject:
                if (!subjectIdList.Contains(value)) subjectIdList.Add(value);
                break;
            case SumProgElements.Topic:
                if (!topicIdList.Contains(value)) topicIdList.Add(value);
                break;
            case SumProgElements.ProgramType:
                if (!programTypeIdList.Contains(value)) programTypeIdList.Add(value);
                break;
        }
    }
}