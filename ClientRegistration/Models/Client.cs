using SQLite;

namespace ClientRegistration.Models
{
    [Table("clients")]
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Column("name"), NotNull]
        public string Name { get; set; }
        
        [Column("lastname"), NotNull]
        public string Lastname { get; set; }
        
        [Column("age")]
        public int Age { get; set; }
        
        [Column("address"), NotNull]
        public string Address { get; set; }

        public Client()
        {
            Name = string.Empty;
            Lastname = string.Empty;
            Address = string.Empty;
        }

        [Ignore]
        public string FullName => $"{Name} {Lastname}";
    }
}