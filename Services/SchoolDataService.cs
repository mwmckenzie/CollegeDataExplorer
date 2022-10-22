using System.Net.Http.Json;
using CollegeDataBrowser.Models;
using CollegeDataExplorer.Helpers;

namespace CollegeDataExplorer.Services;

public class SchoolDataService {
    
    private HttpClient? _http;
    private bool _isInitialized;
    private int _pagesLoaded;
    private readonly SchoolLookUps _schoolLookUps = new();
    private readonly Dictionary<string, List<School>> _schoolsByState = new();
    private readonly Dictionary<string, string> _stateAbbrevs = new();
    public List<string> states { get; private set; } = new();
    public int schoolCount { get; private set; }
    public string loadProgress => $"{(int) (100f * _pagesLoaded / 26f)}%";
    public bool loadComplete { get; private set; }

    public event Action? LoadProgressOccured;
    public event Action? LoadCompleted;

    public async Task Init(HttpClient http) {
        _http = http;
        if (_isInitialized) return;

        for (var i = 0; i < 27; i++) {
            _pagesLoaded = i;
            var schools
                = await _http.GetFromJsonAsync<School[]>($"college-data/CollegeData_{i:0000}.json");

            if (schools is null) continue;

            foreach (var school in schools) {
                var state = school.State ?? "Unknown";
                _schoolsByState.TryAdd(state, new List<School>());
                _schoolsByState[state].Add(school);
                _stateAbbrevs.TryAdd(state, _schoolLookUps.GetStateFullNameByFip(school.StateFips));
                schoolCount++;
            }
            OnInitializationUpdated();
        }

        if (_schoolsByState.Count < 1) return;

        foreach (var key in _schoolsByState.Keys) {
            var list = _schoolsByState[key];
            _schoolsByState[key] = list.OrderBy(x => x.Name).ToList();
        }

        states = _schoolsByState.Keys.OrderBy(x => x).ToList();

        OnInitializationComplete();
    }

    private void OnInitializationUpdated() {
        LoadProgressOccured?.Invoke();
    }

    private void OnInitializationComplete() {
        _isInitialized = true;
        loadComplete = true;
        LoadCompleted?.Invoke();
    }

    public List<School> GetSchoolsByState(string state) {
        return!_schoolsByState.ContainsKey(state) ? new List<School>() : _schoolsByState[state];
    }
    
    public string GetStateFullName(string abbrev) {
        return!_stateAbbrevs.ContainsKey(abbrev) ? "Unknown" : _stateAbbrevs[abbrev];
    }
    
    public SchoolDisplay BuildSchoolDisplay(School school) {
        var display = new SchoolDisplay();

        display.name = school.Name ?? "";
        display.city = school.City ?? "";
        display.state = school.State ?? "";
        display.zip = school.Zip ?? "";
        display.accreditor = school.Accreditor ?? "";

        if (school.SchoolUrl != null)
            display.schoolUrl = school.SchoolUrl.Contains("https://")
                ? school.SchoolUrl
                : $"https://{school.SchoolUrl}";

        if (school.PriceCalculatorUrl != null)
            display.priceCalculatorUrl = school.PriceCalculatorUrl.Contains("https://")
                ? school.PriceCalculatorUrl
                : $"https://{school.PriceCalculatorUrl}";

        //DegreesAwardedPredominantRecoded = degreesAwardedPredominantRecoded;
        //UnderInvestigation = underInvestigation;

        display.mainCampus = _schoolLookUps.GetMainCampus(school.MainCampus);
        display.branches = school.Branches ?? 0;

        //DegreesAwardedPredominant = degreesAwardedPredominant;
        display.degreesAwardedHighest = _schoolLookUps.GetHighestDegree(school.DegreesAwardedHighest);
        display.ownership = _schoolLookUps.GetOwnership(school.Ownership);
        display.stateFips = _schoolLookUps.GetStateFullNameByFip(school.StateFips);
        display.regionId = _schoolLookUps.GetRegion(school.RegionId);
        display.locale = _schoolLookUps.GetLocale(school.Locale);

        display.carnegieBasic = _schoolLookUps.GetCarnegieBasic(school.CarnegieBasic);
        display.carnegieUndergrad = _schoolLookUps.GetCarnegieUndergrad(school.CarnegieUndergrad);
        display.carnegieSizeSetting = _schoolLookUps.GetCarnegieSizeSetting(school.CarnegieSizeSetting);

        display.menOnly = school.MenOnly == 1;
        display.womenOnly = school.WomenOnly == 1;

        display.genderRestriction = display.menOnly ? "Men Only" : "Coed";
        display.genderRestriction = display.womenOnly ? "Women Only" : display.genderRestriction;

        //ReligiousAffiliation = religiousAffiliation;

        display.onlineOnly = school.OnlineOnly == 1 ? "Yes" : "No";
        display.operating = school.Operating == 1 ? "Yes" : "No";

        display.tuitionRevenuePerFte = school.TuitionRevenuePerFte ?? -1;
        display.instructionalExpenditurePerFte = school.InstructionalExpenditurePerFte ?? -1;
        display.facultySalary = school.FacultySalary ?? -1;
        display.schoolFtFacultyRate = school.SchoolFtFacultyRate ?? -1;

        display.alias = school.Alias ?? "";
        display.aliasList = new List<string> {display.alias};

        var splitters = new List<char> {',', '/', '\t', ';'};

        foreach (var splitter in splitters) display.alias = display.alias.Replace(splitter, '|');
        display.aliasList = display.alias.Split('|').ToList();

        //InstitutionalCharacteristicsLevel = institutionalCharacteristicsLevel;
        //OpenAdmissionsPolicy = openAdmissionsPolicy;
        //AccreditorCode = accreditorCode;
        //TitleIvApprovalDate = titleIvApprovalDate;
        //OwnershipPeps = ownershipPeps;
        //TitleIvEligibilityType = titleIvEligibilityType;

        display.id = school.id ?? -1;
        display.ope6Id = school.ope6Id ?? string.Empty;
        display.ope8Id = school.ope8Id ?? string.Empty;

        return display;
    }
}