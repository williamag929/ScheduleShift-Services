



public class CompanyRepository : IRepository<Company>
{
    private readonly ShiftWorkContext _context;
    private readonly DbSet<Company> _dbSet;

    public EntityFrameworkRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Company>();
    }    
    public List<Company> GetAll()
    {
        return _dbSet.ToList();
    }

    public Company GetById(string id)
    {
        return _dbSet.Find(id);
    }

    public void Add(Company entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(Company entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(Company entity)
    {
        _dbSet.Remove(entity);
    }
}


public interface IRepository<T>
{
    List<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(int id);
}