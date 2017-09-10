using AUV.Data;

namespace AUV.PetaPoco5
{
    /// <summary>
    /// 表示使用 PetaPoco 作为数据源并且实体派生自 <see cref="IIdentityEntity" /> 的仓储适配。
    /// </summary>
    /// <typeparam name="TEntity">派生自 <see cref="IIdentityEntity" /> 的实体类型。</typeparam>
    /// <seealso cref="AUV.Data.IIdentityRepository{TEntity}" />
    public class PocoPocoIdentityRepository<TEntity>
        : PetaPocoRepository<TEntity, int>,
        IIdentityRepository<TEntity>
        where TEntity : IIdentityEntity
    {
        /// <summary>
        /// 使用指定的 <see cref="IUnitOfWork"/> 实例初始化 <see cref="PocoPocoIdentityRepository{TEntity}"/> 类的新实例。
        /// </summary>
        /// <param name="context">表示一个工作单元。</param>
        public PocoPocoIdentityRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
