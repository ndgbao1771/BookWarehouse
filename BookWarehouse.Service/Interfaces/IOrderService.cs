using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface IOrderService
    {
        List<OrderDTO> GetAll(OrderFilter filter);
        /// <summary>
        /// Get Order By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OrderDTO GetById(int id);

        OrderDTO GetListBookProgressOfMember(int id);

        List<StatisticsDTO> GetStatistics(DateTime dateStart, DateTime dateEnd);

        OrderAddDTO Add(OrderAddDTO orderDTO);

        void Update(OrderUpdateDTO orderDTO);

        void Delete(int id);
    }
}