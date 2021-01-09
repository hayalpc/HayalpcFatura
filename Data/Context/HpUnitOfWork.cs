using Hayalpc.Library.Repository;

namespace Hayalpc.Fatura.Data
{
    public class HpUnitOfWork : HpUnitOfWork<HpDbContext>
    {
        public HpUnitOfWork(HpDbContext context) : base(context)
        {
        }
    }
}
