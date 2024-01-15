using BookWarehouse.DTO.EntityDTOs;

namespace BookWarehouse.Service.Interfaces
{
    public interface IOrderService
    {
        List<OrderDTO> GetAll();

        List<OrderDTO> GetByNameMember(string name);

        List<OrderDTO> GetByNameLibrarian(string name);

        List<OrderDTO> GetByStatus(bool status);

        OrderDTO GetById(int id);

        OrderAddDTO Add(OrderAddDTO orderDTO);

        void Update(OrderUpdateDTO orderDTO);

        void Delete(int id);
    }
}