using Balto.Domain;
using Balto.Repository.Context;

namespace Balto.Repository
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(BaltoDbContext context) : base(context)
        {
        }
    }
}
