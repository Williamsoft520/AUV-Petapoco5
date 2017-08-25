using AUV.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AUV.PetaPoco5
{
    /// <summary>
    /// 为 <see cref="PetaPocoRepository{TEntity, TKey}"/> 扩展实例。
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// 输出当前操作的日志。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        public static void OutputLog<TEntity, TKey>(this IRepository<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey>
        =>
            repository.AsPetaPoco().DbContext?.SqlLog?.Invoke(repository.AsPetaPoco().DbContext.Database.LastCommand);
        

        /// <summary>
        /// 将当前的 <see cref="IRepository{TEntity, TKey}"/> 对象转换成 <see cref="IPetaPocoRepository{TEntity, TKey}" /> 实例。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        /// <returns>
        /// 实现了 <see cref="IPetaPocoRepository{TEntity, TKey}" /> 的实例。
        /// </returns>
        /// <exception cref="ArgumentNullException">repository 值不能为空。</exception>
        /// <exception cref="NotSupportedException">不支持对 IEFRepository 的转换。</exception>
        public static IPetaPocoRepository<TEntity, TKey> AsPetaPoco<TEntity, TKey>(this IRepository<TEntity, TKey> repository)
                    where TEntity : class, IEntity<TKey>
        {
            repository = repository ?? throw new ArgumentNullException(nameof(repository));

            var result = repository as IPetaPocoRepository<TEntity, TKey>;
            if (result == null)
            {
                throw new NotSupportedException("不支持对 IPocoRepository 的转换。");
            }
            return result;
        }

        /// <summary>
        /// 使用指定的 <see cref="PetaPoco.Sql"/> 实例对当前领域的实体对象进行修改操作。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        /// <returns>
        /// <param name="entity">作为需要修改的实体，赋值的属性表示需要修改的值。</param>
        /// <param name="sql">需要更新的条件，这是一个 <see cref="T:PetaPoco.Sql" /> 的实例。</param>
        public static void Modify<TEntity,TKey>(this IRepository<TEntity,TKey> repository, PetaPoco.Sql sql)
            where TEntity : class, IEntity<TKey>
        {
            repository.AsPetaPoco().DbContext.Database.Update<TEntity>(sql);
            repository.AsPetaPoco().OutputLog();
        }

        /// <summary>
        /// 使用指定的列更新指定实体。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        /// <returns>
        /// <param name="entity">作为需要修改的实体，赋值的属性表示需要修改的值。</param>
        /// <param name="columns">可以指定需要被更新的列。</param>
        public static void Modify<TEntity, TKey>(this IRepository<TEntity, TKey> repository, TEntity entity, IEnumerable<string> columns)
            where TEntity : class, IEntity<TKey>
        {
            repository.AsPetaPoco().DbContext.Database.Update(entity, columns);
            repository.AsPetaPoco().OutputLog();
        }

        /// <summary>
        /// 对指定的主键相关的数据进行移除操作。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        /// <returns>
        /// <param name="id">要删除的数据主键。</param>
        public static void Remove<TEntity,TKey>(this IRepository<TEntity, TKey> repository, TKey id)
             where TEntity : class, IEntity<TKey>
        {
            repository.AsPetaPoco().DbContext.Database.Delete<TEntity>(id);
            repository.AsPetaPoco().OutputLog();
        }

        /// <summary>
        /// 移除指定的 <see cref="T:PetaPoco.Sql" /> 实例的数据。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        /// <returns>
        /// <param name="sql">指定条件的 <see cref="T:PetaPoco.Sql" /> 实例。</param>
        public static void Remove<TEntity, TKey>(this IRepository<TEntity, TKey> repository, PetaPoco.Sql sql)
             where TEntity : class, IEntity<TKey>
        {
            repository.AsPetaPoco().DbContext.Database.Delete<TEntity>(sql);
            repository.OutputLog();
        }

        /// <summary>
        /// 异步获取指定 <see cref="T:PetaPoco.Sql" /> 实例的分页数据。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        /// <returns>
        /// <param name="page">页码。</param>
        /// <param name="size">每页的数据量。</param>
        /// <param name="sql">指定条件的 <see cref="T:PetaPoco.Sql" /> 实例。</param>
        /// <returns></returns>
        public static Task<PetaPoco.Page<TEntity>> FindPagedAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository, int page, int size, PetaPoco.Sql sql)
               where TEntity : class, IEntity<TKey>
        =>
            Task.Run(() =>
            {
                var result= repository.AsPetaPoco().DbContext.Database.Page<TEntity>(page, size, sql);
                repository.OutputLog();
                return result;
            });

        /// <summary>
        /// 异步查询指定 <see cref="PetaPoco.Sql"/> 实例的唯一实体。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        /// <param name="sql"><see cref="PetaPoco.Sql"/> 实例。</param>
        /// <returns></returns>
        public static Task<TEntity> FindAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository, PetaPoco.Sql sql)
                       where TEntity : class, IEntity<TKey>
                    => Task.Run(() =>
                    {
                        var result= repository.AsPetaPoco().DbContext.Database.SingleOrDefault<TEntity>(sql);
                        repository.AsPetaPoco().OutputLog();
                        return result;
                    });

        /// <summary>
        /// 异步保存指定实体。会根据主键进行自动判断是新增还是更新。
        /// </summary>
        /// <typeparam name="TEntity">实体类型。</typeparam>
        /// <typeparam name="TKey">主键类型。</typeparam>
        /// <param name="repository">
        ///   <see cref="IRepository{TEntity, TKey}" /> 实例。</param>
        /// <param name="entity">要保存的实体。</param>
        /// <returns>没有任何返回值的任务对象。</returns>
        public static Task SaveAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository, TEntity entity)
            where TEntity : class, IEntity<TKey>
                                => Task.Run(() =>
                    {
                        repository.AsPetaPoco().DbContext.Database.Save(entity);
                        repository.AsPetaPoco().OutputLog();
                    });

    }
}
