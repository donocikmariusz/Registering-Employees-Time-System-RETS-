
namespace RETS
{
    public class User
    {
        public User(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }
        public string Name { get; private set; }
        public string Surname { get; private set; }            
    }
}






