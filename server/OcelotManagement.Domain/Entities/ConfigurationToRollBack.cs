namespace OcelotManagement.Domain.Entities
{
    /// <summary>
    /// 回滚的配置实体类
    /// </summary>
    public class ConfigurationToRollBack
    {
        public string Json { get; set; }
        /// <summary>
        /// 当前配置 中心信息
        /// </summary>
        public ConfigCenter ConfigCenter;

    }
}
