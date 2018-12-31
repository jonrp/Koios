namespace Koios.Core.Model
{
    public class KUser
    {
        public string Name { get; private set; }
        public KAccess Access { get; private set; }

        internal KUser(string name, KAccess access)
        {
            Name = name;
            Access = access;
        }
    }
}
