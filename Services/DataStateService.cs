using CollegeDataExplorer.Models;

namespace CollegeDataExplorer.Services; 

public class DataStateService {

    public event Action? OnValueChange;

    private string? _selectedState;
    private SchoolDisplay _selectedSchool;

    private string? _selectedFilterType;
    private string? _selectedFilter;

    private string? _selectedSubject;
    private string? _selectedTopic;
    private string? _selectedTag;
    private string? _selectedProgramType;
    
    private SummerProgramObj _selectedProgram;

    public DataStateService() {
        _selectedSchool = new SchoolDisplay();
        _selectedProgram = new SummerProgramObj();
    }

    public string selectedState {
        get => _selectedState ?? string.Empty;
        set {
            if (_selectedState == value) {
                return;
            }
            _selectedState = value;
            NotifyDataStateChanged();
        }
    }
    
    public string selectedFilterType {
        get => _selectedFilterType ?? string.Empty;
        set {
            if (_selectedFilterType == value) {
                return;
            }
            _selectedFilterType = value;
            NotifyDataStateChanged();
        }
    }
    
    public string selectedFilter {
        get => _selectedFilter ?? string.Empty;
        set {
            if (_selectedFilter == value) {
                return;
            }
            _selectedFilter = value;
            NotifyDataStateChanged();
        }
    }
    
    public string selectedSubject {
        get => _selectedSubject ?? string.Empty;
        set {
            if (_selectedSubject == value) {
                return;
            }
            _selectedSubject = value;
            NotifyDataStateChanged();
        }
    }
    public string selectedTopic {
        get => _selectedTopic ?? string.Empty;
        set {
            if (_selectedTopic == value) {
                return;
            }
            _selectedTopic = value;
            NotifyDataStateChanged();
        }
    }
    public string selectedTag {
        get => _selectedTag ?? string.Empty;
        set {
            if (_selectedTag == value) {
                return;
            }
            _selectedTag = value;
            NotifyDataStateChanged();
        }
    }
    public string selectedProgramType {
        get => _selectedProgramType ?? string.Empty;
        set {
            if (_selectedProgramType == value) {
                return;
            }
            _selectedProgramType = value;
            NotifyDataStateChanged();
        }
    }
    
    public SchoolDisplay selectedSchool {
        get => _selectedSchool;
        set {
            _selectedSchool = value;
            NotifyDataStateChanged();
        }
    }
    
    public SummerProgramObj selectedProgram {
        get => _selectedProgram;
        set {
            _selectedProgram = value;
            NotifyDataStateChanged();
        }
    }

    private void NotifyDataStateChanged() {
        OnValueChange?.Invoke();
    }
}