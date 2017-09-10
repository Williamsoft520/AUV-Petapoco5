namespace AUV.PetaPoco5
{
    using AUV.Data;
    using PetaPoco;
    using System.Collections.Generic;

    /// <summary>
    /// 提供基于 PetaPoco 框架的领域驱动设计仓储功能。
    /// </summary>
    /// <typeparam name="TEntity">派生自 <see cref="IEntity{TKey}" /> 的实例。</typeparam>
    /// <typeparam name="TKey">表示实体对象的主键标识。</typeparam>
    /// <seealso cref="AUV.Data.IRepository{TEntity, TKey}" />
    public interface IPetaPocoRepository<TEntity, TKey>
        : IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// 获取当前操作的工作单元。
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// 获取指定 <see cref="PetaPoco.Sql"/> 实例查询的结果。
        /// </summary>
        /// <param name="sql"><see cref="PetaPoco.Sql"/> 实例。</param>
        /// <returns>查询结果集。</returns>
        IEnumerable<TEntity> Query(PetaPoco.Sql sql);
    }
}
