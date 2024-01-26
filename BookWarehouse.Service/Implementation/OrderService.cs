using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.Enums;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.Csv;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;
using CsvHelper;
using System.Globalization;

namespace BookWarehouse.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, AppDbContext context)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _context = context;
        }

        public OrderAddDTO Add(OrderAddDTO orderDTO)
        {
            var MemberCheckReference = _context.Members.Where(x => x.Id == orderDTO.MemberId).FirstOrDefault();
            var LibrarianCheckReference = _context.Librarians.Where(x => x.Id == orderDTO.LibrarianId).FirstOrDefault();
            var BookCheckReference = _context.Books.Where(x => x.Id == orderDTO.BookId).FirstOrDefault();
            if (MemberCheckReference == null || LibrarianCheckReference == null || BookCheckReference == null)
            {
                return orderDTO;
            }
            else
            {
                _orderRepository.Add(_mapper.Map<OrderAddDTO, Order>(orderDTO));
                _orderRepository.Commit();
                return orderDTO;
            }
        }

        public void Delete(int id)
        {
            _orderRepository.Remove(id);
            _orderRepository.Commit();
        }

        public List<OrderDTO> GetAll(OrderFilter filter)
        {
            var query = _orderRepository.GetAllByViewSql();
            query = query.Where(x => string.IsNullOrEmpty(filter.MemberName) || x.MemberName == filter.MemberName)
                         .Where(x => string.IsNullOrEmpty(filter.LibrarianName) || x.LibratianName == filter.LibrarianName)
                         .Where(x => x.OrderStatus != null || x.OrderStatus == filter.StatusAble)
                         .Where(x => x.DateCreated != null || x.DateCreated == filter.CreatedDate)
                         .Where(x => x.ActualReturnDate != null || x.ActualReturnDate == filter.DateGiveCurrent);
            return query.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public OrderDTO GetById(int id)
        {
            return _mapper.Map<Order, OrderDTO>(_orderRepository.GetById(id).FirstOrDefault());
        }

        public OrderDTO GetListBookProgressOfMember(int id)
        {
            return _mapper.Map<Order, OrderDTO>(_orderRepository.GetListBookProgressOfMember(id).FirstOrDefault());
        }

        public List<StatisticsDTO> GetStatistics(DateTime dateStart, DateTime dateEnd)
        {
            var datas = _mapper.Map<List<Order>, List<StatisticsDTO>>(_orderRepository.GetStatistics(dateStart, dateEnd).ToList());
            WriteCsvFile(datas);
            return datas;
        }

        public void WriteCsvFile(List<StatisticsDTO> filtered)
        {
            if (filtered == null || !filtered.Any())
            {
                Console.WriteLine("No data to export.");
                return;
            }
            string directoryPath = Path.Combine("D:\\Developer\\SiliconStack\\Project2\\BookWarehouse\\BookWarehouse", "Statistics");
            Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, "statistics.csv");

            using (var writer = new StreamWriter(filePath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CsvFileMap>();

                    csv.WriteHeader<StatisticsDTO>();
                    csv.NextRecord();
                    csv.WriteRecords(filtered);
                }
            }
        }

        public void Update(OrderUpdateDTO orderDTO)
        {
            var exist = _orderRepository.FindById(orderDTO.Id, x => x.orderDetails);
            if (exist != null)
            {
                exist.Status = orderDTO.Status;
                exist.orderDetails.ForEach(x => x.DateGiveCurrent = orderDTO.DateGiveCurent);
                _orderRepository.Updated(exist);
                _orderRepository.Commit();
            }
        }
    }
}