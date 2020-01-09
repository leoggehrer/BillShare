namespace BillShare.Contracts
{
	/// <summary>
	/// Defines a generalized copying method for all properties defined by the Type T.
	/// </summary>
	/// <typeparam name="T">The type of object to copy.</typeparam>
	public partial interface ICopyable<T>
	{
		void CopyProperties(T other);
	}
}
