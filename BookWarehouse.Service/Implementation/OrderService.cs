using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.Interfaces;

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
            return _mapper.Map<Order, OrderDTO>(_orderRepository.FindById(id));
        }

        public List<OrderDTO> GetByNameLibrarian(string name)
        {
            return _mapper.Map<List<Order>, List<OrderDTO>>(_orderRepository.GetByNameLibrarian(name).ToList());
        }

        public List<OrderDTO> GetByNameMember(string name)
        {
            return _mapper.Map<List<Order>, List<OrderDTO>>(_orderRepository.GetByNameMember(name).ToList());
        }

        public List<OrderDTO> GetByStatus(bool status)
        {
            return _mapper.Map<List<Order>, List<OrderDTO>>(_orderRepository.GetByStatus(status).ToList());
        }

        public void Update(OrderUpdateDTO orderDTO)
        {
            _orderRepository.Updated(_mapper.Map<OrderUpdateDTO, Order>(orderDTO));
            _orderRepository.Commit();
        }
    }
}