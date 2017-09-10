using System;

namespace AUV.PetaPoco5
{
    using Data;
    /// <summary>
    /// 表示工作单元的扩展实例。
    /// </summary>
    public static class UnitOfWorkExtension
    {
        /// <summary>
        /// 将当前工作单元转换成 <see cref="PetaPocoDbContext"/> 实例。
        /// </summary>
        /// <param name="unitOfWork">当前工作单元的扩展实例。</param>
        /// <returns></returns>
        public static PetaPocoDbContext AsPetaPocoDbContext(this IUnitOfWork unitOfWork)
            => unitOfWork as PetaPocoDbContext ?? throw new NotSupportedException("当前 UnitOfWork 不是 PetaPocoDbContext 的实例，更多帮助请查阅 https://github.com/Williamsoft520/AUV-Entityframework6 .");
    }
}
