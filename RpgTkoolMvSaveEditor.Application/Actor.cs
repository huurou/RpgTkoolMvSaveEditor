using RpgTkoolMvSaveEditor.Domain.GameDatas;

namespace RpgTkoolMvSaveEditor.Application
{
    public class Actor
    {
        public string Name { get; } = "";
        public int HP { get; }
        public int MP { get; }
        public int TP { get; }
        public int Exp { get; }

        public Actor(ActorData actorData)
        {
            Name = actorData.Name;
            HP = actorData.HP;
            MP = actorData.MP;
            TP = actorData.TP;
            Exp = actorData.Exp;
        }
    }
}