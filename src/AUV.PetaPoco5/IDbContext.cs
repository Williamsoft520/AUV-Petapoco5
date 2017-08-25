using PetaPoco;

namespace AUV.PetaPoco5
{
    using AUV.Data;
    using System;

    /// <summary>
    /// 提供可对 <see cref="PetaPoco" /> 上下文操作的功能。
    /// </summary>
    /// <seealso cref="IUnitOfWork" />
    /// <seealso cref="ISqlCommand" />
    public interface IDbContext : IUnitOfWork,ISqlCommand
    {
        /// <summary>
        /// 获取当前的 <see cref="PetaPoco.Database"/> 实例。
        /// </summary>
        Database Database { get; }

        /// <summary>
        /// 表示需要记录 SQL 日志的委托。
        /// </summary>
        /// <example>
        /// <code>
        /// SqlLog = line => Debug.Write(line);
        /// </code>
        /// </example>
        Action<string> SqlLog { get; set; }
    }
}
