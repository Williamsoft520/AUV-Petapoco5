using System;
namespace AUV.PetaPoco5
{
    /// <summary>
    /// 提供 <see cref="IDbContext"/>的扩展实例。
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        /// 设置可用于跟踪日志的委托。
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="action">一个委托，用于跟踪 SQL 命令的输出。</param>
        /// <returns></returns>
        public static IDbContext SetSqlLog(this IDbContext context, Action<string> action)
        {
            context.SqlLog = action;
            return context;
        }
    }
}
