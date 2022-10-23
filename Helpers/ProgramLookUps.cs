using CollegeDataExplorer.Models;

namespace CollegeDataExplorer.Helpers; 

public class ProgramLookUps {
    
    public SubjectList subjectList { get; set; } = new();
    public TopicList topicList { get; set; } = new();
    public OrgTypeList orgTypeList { get; set; } = new();
    public ProgramTypeList programTypeList { get; set; } = new();
    public TagList tagList { get; set; } = new();
    public CitizenshipList citizenshipList { get; set; } = new();
    public ResidenceList residenceList { get; set; } = new();

    public IEnumerable<string> Subjects() {
        return from value in subjectList.list where value.value != null select value.value;
    }
    public IEnumerable<string> Topics() {
        return from value in topicList.list where value.value != null select value.value;
    }
    public IEnumerable<string> Tags() {
        return from value in tagList.list where value.value != null select value.value;
    }
    public IEnumerable<string> ProgramTypes() {
        return from value in programTypeList.list where value.value != null select value.value;
    }

    public string GetSubject(int id) {
        if (!subjectList.TryGetValue(id, out var text)) return string.Empty;
        return !string.IsNullOrWhiteSpace(text) ? text : string.Empty;
    }
    
    public string GetTopic(int id) {
        if (!topicList.TryGetValue(id, out var text)) return string.Empty;
        return !string.IsNullOrWhiteSpace(text) ? text : string.Empty;
    }
    
    public string GetOrgType(int id) {
        if (!orgTypeList.TryGetValue(id, out var text)) return string.Empty;
        return !string.IsNullOrWhiteSpace(text) ? text : string.Empty;
    }
    
    public string GetProgramType(int id) {
        if (!programTypeList.TryGetValue(id, out var text)) return string.Empty;
        return !string.IsNullOrWhiteSpace(text) ? text : string.Empty;
    }
    
    public string GetTag(int id) {
        if (!tagList.TryGetValue(id, out var text)) return string.Empty;
        return !string.IsNullOrWhiteSpace(text) ? text : string.Empty;
    }
    
    public string GetCitizenship(int id) {
        if (!citizenshipList.TryGetValue(id, out var text)) return string.Empty;
        return !string.IsNullOrWhiteSpace(text) ? text : string.Empty;
    }
    
    public string GetResidence(int id) {
        if (!residenceList.TryGetValue(id, out var text)) return string.Empty;
        return !string.IsNullOrWhiteSpace(text) ? text : string.Empty;
    }
}