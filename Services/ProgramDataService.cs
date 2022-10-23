using System.Net.Http.Json;
using System.Text.Json;
using CollegeDataExplorer.Helpers;
using CollegeDataExplorer.Models;

namespace CollegeDataExplorer.Services; 

public class ProgramDataService {
    
    private HttpClient? _http;
    private bool _isInitialized;
    public bool loadComplete => _isInitialized;

    private ProgramLookUps _lookUps = new();

    private Dictionary<string, SummerProgramObj> _sumProgs = new();
    private Dictionary<string, Application> _applications = new();
    private Dictionary<string, Session> _sessions = new();
    private Dictionary<string, StudentInfo> _studentInfos = new();
    private Dictionary<string, Org> _orgs = new();

    public int programCount => _sumProgs.Count;
    public List<SummerProgramObj> programList => _sumProgs.Values.OrderBy(x => x.name).ToList();
    
    public IEnumerable<string> subjects => _lookUps.Subjects().OrderBy(x => x).ToList();
    public IEnumerable<string> topics => _lookUps.Topics();
    public IEnumerable<string> tags => _lookUps.Tags();
    public IEnumerable<string> programTypes => _lookUps.ProgramTypes();

    public async Task Init(HttpClient http) {
        _http = http;
        if (_isInitialized) return;

        await LoadLookUpsAsync();
        await LoadDbObjectsAsync();
        
        OnInitializationComplete();
    }
    
    private void OnInitializationComplete() {
        _isInitialized = true;
    }

    private async Task LoadDbObjectsAsync() {
        
        var programs =
            await _http.GetFromJsonAsync<List<SummerProgramObj>>(
                "summerProgram-data/SummerProgramDbItems.json");
        
        foreach (var program in programs) {
            program.lookUps = _lookUps;
            _sumProgs.TryAdd(program.id, program);
        }
    }

    private async Task LoadLookUpsAsync() {
        await BuildLookUp<Subject>(_lookUps.subjectList, "lookup-data/Subjects.json");
        await BuildLookUp<Topic>(_lookUps.topicList, "lookup-data/Topics.json");
        await BuildLookUp<OrgType>(_lookUps.orgTypeList, "lookup-data/OrgTypeList.json");
        await BuildLookUp<ProgramType>(_lookUps.programTypeList, "lookup-data/ProgramTypes.json");
        await BuildLookUp<Tag>(_lookUps.tagList, "lookup-data/Tags.json");
        await BuildLookUp<Citizenship>(_lookUps.citizenshipList, "lookup-data/Citizenships.json");
        await BuildLookUp<Residence>(_lookUps.residenceList, "lookup-data/Residences.json");
    }
    
    private async Task<T?> DeserializeFile<T>(string fileName) {
        await using var openStream = File.OpenRead(fileName);
        var list = await JsonSerializer.DeserializeAsync<T>(openStream);
        return list;
    }

    private async Task BuildLookUp<T>(IndexedList indexedList, string filename) {
        
        var list = await _http.GetFromJsonAsync<List<T>>(filename);
        
        //var list = await DeserializeFile<List<T>>(filename);
        if (list is null) return;
        
        foreach (var listItem in list) {
                indexedList.Add(listItem);
        }
    }


    public int GetSubjectId(string subject) {
        return _lookUps.subjectList.IndexOf(subject);
    }
    
}