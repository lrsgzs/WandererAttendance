using CommunityToolkit.Mvvm.ComponentModel;

namespace WandererAttendance.Abstraction;

public abstract class BaseConfig : ObservableRecipient
{
    public abstract string ConfigFilePath { get; }
}