using RpgTkoolMvSaveEditor.Util.Events;

namespace RpgTkoolMvSaveEditor.Model;

public class ApplicationService
{
    public Event<ErrorOccurredEventArgs> ErrorOccurred { get; } = new();
}

public record ErrorOccurredEventArgs(string Message);