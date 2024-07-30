namespace BookProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }

    }
    namespace BookProject.Models
    {
        public class UserRoleDto
        {
            public string Username { get; set; }
            public string RoleName { get; set; }
        }
    }

}
