namespace OcelotManagement.Application.Dtos
{
    public class ProjDto : ProjInputDto
    {
        /// <summary>
        /// 创建时间 
        ///</summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间 
        ///</summary>
        public DateTime UpdateTime { get; set; }
        public string Id { get; set; }
    }
}
