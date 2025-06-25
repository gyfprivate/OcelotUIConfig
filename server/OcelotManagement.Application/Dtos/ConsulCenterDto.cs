namespace OcelotManagement.Application.Dtos
{
    public class ConsulCenterDto
    {
        /// <summary>
        /// IP
        ///</summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        ///</summary>
        public int Port { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 所属位置
        /// </summary>
        public string Key { get; set; }
    }
}
