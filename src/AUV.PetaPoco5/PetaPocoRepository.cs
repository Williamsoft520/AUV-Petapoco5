namespace AUV.PetaPoco5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using PetaPoco;
    using AUV.Data;

    /// <summary>
    /// 表示使用 PetaPoco 作为数据源的仓储适配。
    /// </summary>
    /// <typeparam name="TEntity">派生自 <see cref="IEntity{TKey}" /> 的实例。</typeparam>
    /// <typeparam name="TKey">表示实体对象的主键标识。</typeparam>
    /// <seealso cref="AUV.PetaPoco5.IPetaPocoRepository{TEntity, TKey}" />
    public class PetaPocoRepository<TEntity, TKey>
        : IPetaPocoRepository<TEntity,TKey>
        where TEntity : IEntity<TKey>
    {
        private readonly IDbContext _context;

        /// <summary>
        /// 获取当前操作的 <see cref="T:AUV.PetaPoco5.IDataBaseContext" /> 实例。
        /// </summary>
        IDbContext IPetaPocoRepository<TEntity,TKey>.DbContext => _context;

        /// <summary>
        /// 使用指定的 <see cref="IDbContext"/> 实例初始化 <see cref="PetaPocoRepository{TEntity, TKey}"/> 类的新实例。
        /// </summary>
        /// <param name="context">实现了 <see cref="IDbContext"/> 的仓储上下文管理实例。</param>
        public PetaPocoRepository(IDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 可以将指定实体添加到当前仓储。
        /// </summary>
        /// <param name="entity">需要添加的实体。</param>
        public virtual void Add(TEntity entity)
        {
            _context.Database.Insert(entity);
            OutputLog();
        }

        /// <summary>
        /// 输出日志。
        /// </summary>
        void OutputLog() => _context.SqlLog?.Invoke(_context.Database.LastCommand);
                
        /// <summary>
        /// 当前仓储可以对指定实体进行修改。将会对实体中的所有字段进行更新。
        /// </summary>
        /// <param name="entity">需要修改的实体。</param>
        public virtual void Modify(TEntity entity)
        {
            _context.Database.Update(entity);
            OutputLog();
        }



        /// <summary>
        /// 可以将指定实体从当前仓储中移除。
        /// </summary>
        /// <param name="entity">需要移除的实体。</param>
        public virtual void Remove(TEntity entity)
        {
            _context.Database.Delete<TEntity>(entity);
            OutputLog();
        }

        /// <summary>
        /// 异步从当前仓储中查找指定唯一 Id 的实体。
        /// </summary>
        /// <param name="id">要查找的实体唯一 Id 值。</param>
        /// <returns></returns>
        public virtual Task<TEntity> FindAsync(TKey id)
            => Task.Run(() =>
            {
                OutputLog();
                return _context.Database.SingleOrDefault<TEntity>(id);
            });
        

        /// <summary>
        /// 查询指定的 <see cref="Sql"/> 数据。
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Query(Sql sql) => _context.Database.Query<TEntity>(sql);

        /// <summary>
        /// 表示查询集合的标准。
        /// </summary>
        /// <returns>
        /// 可查询的实体对象。
        /// </returns>
        IQueryable<TEntity> IRepository<TEntity, TKey>.Query() => _context.Database.Fetch<TEntity>(Sql.Builder).AsQueryable();

    }
}
