namespace RpgTkoolMvSaveEditor.Application
{
    public class Actor
    {
        public string Name { get; } = "";
        public int HP { get; }
        public int MP { get; }
        public int TP { get; }
        public int Exp { get; }

        public Actor(string name, int hp, int mp, int tp, int exp)
        {
            Name = name;
            HP = hp;
            MP = mp;
            TP = tp;
            Exp = exp;
        }
    }
}