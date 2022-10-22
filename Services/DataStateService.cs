using CollegeDataBrowser.Models;

namespace CollegeDataExplorer.Services; 

public class DataStateService {

    public event Action? OnValueChange;

    private string? _selectedState;
    private SchoolDisplay _selectedSchool;

    public DataStateService() {
        _selectedSchool = new SchoolDisplay();
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
    
    public SchoolDisplay selectedSchool {
        get => _selectedSchool;
        set {
            _selectedSchool = value;
            NotifyDataStateChanged();
        }
    }

    private void NotifyDataStateChanged() {
        OnValueChange?.Invoke();
    }
}