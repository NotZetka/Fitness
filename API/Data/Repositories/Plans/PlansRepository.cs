namespace API.Data.Repositories
{
    public class PlansRepository(DataContext context) : AbstractRepository<FitnessPlan>(context), IPlansRepository
    {
    }
}
