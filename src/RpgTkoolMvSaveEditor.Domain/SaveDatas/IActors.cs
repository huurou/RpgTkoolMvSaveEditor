using RpgTkoolMvSaveEditor.Domain.GameDatas;

namespace RpgTkoolMvSaveEditor.Domain.SaveDatas;

public interface IActors : IEnumerable<ActorData>
{
    event EventHandler<ActorData>? ValueChanged;
}