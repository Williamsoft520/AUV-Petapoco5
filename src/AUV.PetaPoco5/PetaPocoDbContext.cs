using PetaPoco;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace AUV.PetaPoco5
{
    /// <summary>
    /// 表示可以对 PetaPoco 进行上下文管理实例。
    /// </summary>
    /// <seealso cref="DisposableHandler" />
    public class PetaPocoDbContext : DisposableHandler,  Data.IUnitOfWork,IDisposable
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
        /// 由派生类执行与释放或重置非托管资源相关的应用程序定义的任务处理。
        /// </summary>
        protected override void DisposeHandler() => _database.Dispose();
        
    }
}
