using BookWarehouse.Service.EntityDTOs;
using CsvHelper.Configuration;

namespace BookWarehouse.Service.Csv
{
    public class CsvFileMap : ClassMap<StatisticsDTO>
    {
        public CsvFileMap()
        {
            Map(m => m.BorrowerName).Name("BorrowerName");
            Map(m => m.LibrarianName).Name("LibrarianName");
            Map(m => m.BookName).Name("BookName");
            Map(m => m.ExpectedReturnDate).Name("ExpectedReturnDate");
            Map(m => m.ActualReturnDate).Name("ActualReturnDate");
            Map(m => m.Status).Name("Status");
        }
    }
}