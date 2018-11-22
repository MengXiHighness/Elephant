
namespace DS.AFP.Common.Core
{
	
    public interface IHandler
	{
		/// <summary>
		/// 关闭处理，并释放资源
		/// </summary>
		/// <remarks>
        /// </remarks>
		void Close();

		/// <summary>
		/// 执行该处理
		/// </summary>	
        void ExecuteHandler(PersistentData data);

		
		string Name { get; set; }

        void InitOptions();
	}
}
