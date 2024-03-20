using RpgTkoolMvSaveEditor.Domain.GameDatas;

namespace RpgTkoolMvSaveEditor.Application
{
    public class Actor(ActorData actorData)
    {
        public string Name { get; } = actorData.Name;
        public int HP { get; } = actorData.HP;
        public int MP { get; } = actorData.MP;
        public int TP { get; } = actorData.TP;
        public int Exp { get; } = actorData.Exp;
    }
}