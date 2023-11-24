using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_domain.Entities.Base;
using myfinance_web_dotnet_infra.Interfaces.Base;
using System.Collections.Generic;

namespace myfinance_web_dotnet_infra
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        protected Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Cadastrar(TEntity Entidade)
        {
            if (Entidade.Id == null)
            {
                _dbSet.Add(Entidade);
            }
            else
            {
                _dbSet.Attach(Entidade);
                _dbContext.Entry(Entidade).State = EntityState.Modified;
            }

            _dbContext.SaveChanges();
        }

        public void Excluir(int Id)
        {
            {
                var entidade = new TEntity() { Id = Id };
                _dbContext.Attach(entidade);
                _dbContext.Remove(entidade);
                _dbContext.SaveChanges();
            }
        }

        public List<TEntity> ListarRegistros()
        {
            return _dbSet.ToList();
        }

        public TEntity RetornarRegistro(int Id)
        {
            return _dbSet.Where(x => x.Id == Id).First();
        }
    }
}
