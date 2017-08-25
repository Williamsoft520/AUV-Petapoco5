using AUV.Data;
using System;

namespace AUV.PetaPoco5
{
    /// <summary>
    /// 表示使用 PetaPoco 作为数据源并且实体派生自 <see cref="IUniqueIdentifierEntity" /> 的仓储适配。
    /// </summary>
    /// <typeparam name="TEntity">派生自 <see cref="IUniqueIdentifierEntity" /> 的实体类型。</typeparam>    
    /// <seealso cref="PetaPoco5.PetaPocoRepository{TEntity, System.Guid}" />
    public class PetaPocoUniqueIdentifierRepository<TEntity>
        : PetaPocoRepository<TEntity, Guid>,
        IUniqueIdentifierRepository<TEntity>
        where TEntity : IUniqueIdentifierEntity
    {
        /// <summary>
        /// 使用指定的 <see cref="IDbContext"/> 实例初始化 <see cref="PetaPocoUniqueIdentifierRepository{TEntity}"/> 类的新实例。
        /// </summary>
        /// <param name="context">实现了 <see cref="T:AUV.PetaPoco5.IDataBaseContext" /> 的仓储上下文管理实例。</param>
        public PetaPocoUniqueIdentifierRepository(IDbContext context) : base(context)
        {
        }
    }
}
