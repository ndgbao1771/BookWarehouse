using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.DTO.Enums;

namespace BookWarehouse.Service.Interfaces
{
    public interface IOrderService
    {
        List<OrderDTO> GetAll();

        List<OrderDTO> GetByNameMember(string name);

        List<OrderDTO> GetByNameLibrarian(string name);

        List<OrderDTO> GetByStatus(StatusAble status);

        OrderDTO GetById(int id);

        OrderDTO GetListBookProgressOfMember(int id);

        List<StatisticsDTO> GetStatistics(DateTime dateStart, DateTime dateEnd);

        OrderAddDTO Add(OrderAddDTO orderDTO);

        void Update(OrderUpdateDTO orderDTO);

        void Delete(int id);
    }
}