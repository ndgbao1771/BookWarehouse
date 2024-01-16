using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.DTO.Enums;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.Csv;
using BookWarehouse.Service.Interfaces;
using CsvHelper;
using System.Globalization;

namespace BookWarehouse.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public OrderAddDTO Add(OrderAddDTO orderDTO)
        {
            _orderRepository.Add(_mapper.Map<OrderAddDTO, Order>(orderDTO));
            _orderRepository.Commit();
            return orderDTO;
        }

        public void Delete(int id)
        {
            _orderRepository.Remove(id);
            _orderRepository.Commit();
        }

        public List<OrderDTO> GetAll()
        {
            return _orderRepository.FindAll().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public OrderDTO GetById(int id)
        {
            return _mapper.Map<Order, OrderDTO>(_orderRepository.GetById(id).FirstOrDefault());
        }

        public List<OrderDTO> GetByNameLibrarian(string name)
        {
            return _mapper.Map<List<Order>, List<OrderDTO>>(_orderRepository.GetByNameLibrarian(name).ToList());
        }

        public List<OrderDTO> GetByNameMember(string name)
        {
            return _mapper.Map<List<Order>, List<OrderDTO>>(_orderRepository.GetByNameMember(name).ToList());
        }

        public List<OrderDTO> GetByStatus(StatusAble status)
        {
            return _mapper.Map<List<Order>, List<OrderDTO>>(_orderRepository.GetByStatus(status).ToList());
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
            _orderRepository.Updated(_mapper.Map<OrderUpdateDTO, Order>(orderDTO));
            _orderRepository.Commit();
        }
    }
}