namespace FullStack.API.Model
{
    public class Player
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public long Salary { get; set; }
        public string Club { get; set; }
    }
}
