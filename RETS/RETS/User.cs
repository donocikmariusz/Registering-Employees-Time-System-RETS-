namespace RETS
{
    public class User
    {
        public User(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }

        public virtual string Name { get; private set; }
        public virtual string Surname { get; private set; }
    }
}
