using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_infra;
using myfinance_web_dotnet_service.Interfaces;

namespace myfinance_web_dotnet_service
{
    public class PlanoContaService : IPlanoContaService
    {
        private readonly MyFinanceDbContext _dbContext;

        public PlanoContaService(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Cadastrar(PlanoConta planoConta)
        {
            var dbSet = _dbContext.PlanoConta;

            if(planoConta.Id == null)
            {
                dbSet.Add(planoConta);
            }
            else
            {
                dbSet.Attach(planoConta);
                _dbContext.Entry(planoConta).State = EntityState.Modified;
            }

            _dbContext.SaveChanges();
        }

        public void Excluir(int Id)
        {
            var PlanoConta = new PlanoConta() {Id = Id};
            _dbContext.Attach(PlanoConta);
            _dbContext.Remove(PlanoConta);
            _dbContext.SaveChanges();
        }

        public List<PlanoConta> ListarRegistros()
        {
            var dbSet = _dbContext.PlanoConta;
            return dbSet.ToList();
        }

        public PlanoConta RetornarRegistro(int Id)
        {
            return _dbContext.PlanoConta.Where(x => x.Id == Id).First();
        }
    }
}