namespace ShiftWork.Backend.Services
{

    public interface IRepository<T>
    {
        Task<List<T>> GetAll(string companyId, int[] ids);
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}