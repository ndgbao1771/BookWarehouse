using BookWarehouse.DTO.Shared;

namespace BookWarehouse.DTO.EntityViewSQL
{
    public class AuthorViewSQL
    {
        public string AuthorName { get; }

        public DateTime DateCreated { get; }

        public string Creater { get; }

        public DateTime DateUpdated { get; }

        public string Updater { get; }
    }
}