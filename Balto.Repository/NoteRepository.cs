using Balto.Domain;
using Balto.Repository.Context;

namespace Balto.Repository
{
    class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(BaltoDbContext context) : base(context)
        {
        }
    }
}
