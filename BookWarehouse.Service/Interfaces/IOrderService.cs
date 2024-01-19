using BookWarehouse.DTO.Enums;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface IOrderService
    {
        List<OrderDTO> GetAll();

        List<OrderDTO> GetByNameMember(string name);

        List<OrderDTO> GetByNameLibrarian(string name);

        List<OrderDTO> GetByStatus(StatusAble status);

        List<OrderDTO> GetByFilter(OrderFilter filter);

        OrderDTO GetById(int id);

        OrderDTO GetListBookProgressOfMember(int id);

        List<StatisticsDTO> GetStatistics(DateTime dateStart, DateTime dateEnd);

        OrderAddDTO Add(OrderAddDTO orderDTO);

        void Update(OrderUpdateDTO orderDTO);

        void Delete(int id);
    }
}