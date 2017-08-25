using PetaPoco;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace AUV.PetaPoco5
{
    /// <summary>
    /// 表示可以对 PetaPoco 进行上下文管理实例。
    /// </summary>
    /// <seealso cref="AUV.PetaPoco5.IDbContext" />
    /// <seealso cref="DisposableHandler" />
    public class PetaPocoDbContext : DisposableHandler,  IDbContext
    {
        private readonly Database _database;

        /// <summary>
        /// 使用 <see cref="PetaPoco.Database"/> 初始化 <see cref="PetaPocoDbContext"/> 类的新实例。
        /// </summary>
        /// <param name="database"><see cref="PetaPoco.Database"/> 实例。</param>
        public PetaPocoDbContext(Database database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _database?.BeginTransaction();
        }

        /// <summary>
        /// 获取当前的 <see cref="PetaPoco.Database" /> 实例。
        /// </summary>
        public virtual Database Database => _database;

        /// <summary>
        /// 表示需要记录 SQL 日志的委托。
        /// </summary>
        /// <example>
        ///   <code>
        /// SqlLog = line =&gt; Debug.Write(line);
        /// </code>
        /// </example>
        public virtual Action<string> SqlLog { get; set; }

        /// <summary>
        /// 表示使用异步的方式将当前的工作单元完结。
        /// </summary>
        /// <returns></returns>
        public virtual Task CompleteAsync() => Task.Run(() => _database.CompleteTransaction());
        
        /// <summary>
        /// 异步执行非查询的命令。
        /// </summary>
        /// <param name="commandText">命令文本。文本可以使用符合 PetaPoco 所规定的查询命令。</param>
        /// <param name="parameters">符合命令文本的参数列表。</param>
        /// <returns>影响行数。</returns>
        public virtual Task<int> ExecuteAsync(string commandText, params object[] parameters)
        => Task.Run(()=>
        {
            var rows = _database.Execute(commandText, parameters);
            this.SqlLog?.Invoke(_database.FormatCommand(commandText, parameters));
            return rows;
        });
        
        /// <summary>
        /// 异步查询指定命令并返回指定结果的集合。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="commandText">命令文本。文本可以使用符合 PetaPoco 所规定的查询命令。</param>
        /// <param name="parameters">符合命令文本的参数列表。</param>
        /// <returns>集合迭代器。</returns>
        public virtual Task<IEnumerable<T>> QueryAsync<T>(string commandText, params object[] parameters)
                => Task.Run(()=>
                {
                    var query = _database.Query<T>(commandText, parameters);
                    this.SqlLog?.Invoke(_database.FormatCommand(commandText, parameters));
                    return query;
                });

        /// <summary>
        /// 由派生类执行与释放或重置非托管资源相关的应用程序定义的任务处理。
        /// </summary>
        protected override void DisposeHandler() => _database.Dispose();
        
    }
}
