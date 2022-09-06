namespace Vacations.Utilities.Settings
{
    public class AppSettings
    {
        public Application Application { get; set; }
        public Author Author { get; set; }
        public Company Company { get; set; }
    }

    public class Application
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool IsTest { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Company
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
