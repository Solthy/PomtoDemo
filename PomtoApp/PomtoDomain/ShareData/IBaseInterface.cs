namespace PomtoDomain.ShareData
{
    public interface IBaseInterface<T> where T : ModelBase
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetListAsync();
        Task<int> SaveAsync(T model);
        Task<int> DisableAsync(int id);
    }
}