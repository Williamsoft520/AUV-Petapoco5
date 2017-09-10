using AUV.Data;
using System;

namespace AUV.PetaPoco5
{
    /// <summary>
    /// 表示使用 PetaPoco 作为数据源并且实体派生自 <see cref="IUniqueIdentifierEntity" /> 的仓储适配。
    /// </summary>
    /// <typeparam name="TEntity">派生自 <see cref="IUniqueIdentifierEntity" /> 的实体类型。</typeparam>    
    public class PetaPocoUniqueIdentifierRepository<TEntity>
        : PetaPocoRepository<TEntity, Guid>,
        IUniqueIdentifierRepository<TEntity>
        where TEntity : IUniqueIdentifierEntity
    {
        /// <summary>
        /// 使用指定的 <see cref="IUnitOfWork"/> 实例初始化 <see cref="PetaPocoUniqueIdentifierRepository{TEntity}"/> 类的新实例。
        /// </summary>
        /// <param name="unitOfWork">表示工作单元。</param>
        public PetaPocoUniqueIdentifierRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
