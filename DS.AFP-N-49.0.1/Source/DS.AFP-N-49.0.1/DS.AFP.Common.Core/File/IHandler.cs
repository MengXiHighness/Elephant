
namespace DS.AFP.Common.Core
{
	
    public interface IHandler
	{
		/// <summary>
		/// �رմ������ͷ���Դ
		/// </summary>
		/// <remarks>
        /// </remarks>
		void Close();

		/// <summary>
		/// ִ�иô���
		/// </summary>	
        void ExecuteHandler(PersistentData data);

		
		string Name { get; set; }

        void InitOptions();
	}
}
