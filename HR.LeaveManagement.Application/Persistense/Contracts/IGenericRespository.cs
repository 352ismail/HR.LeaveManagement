namespace HR.LeaveManagement.Application.Persistense.Contracts
{
    public interface IGenericRespository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task<bool> Exists(int Id);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }
}
