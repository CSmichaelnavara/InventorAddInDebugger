namespace MiNa.InventorAddInDebugger
{
    /// <summary>
    /// Provides information about add-in in assembly
    /// </summary>
    public class AddInInfo
    {
        /// <summary>
        /// Creates new instance of AddInInfo
        /// </summary>
        /// <param name="fullName">Full name of the AddIn type</param>
        /// <param name="clientId">ClientId of the AddIn</param>
        public AddInInfo(string fullName, string clientId)
        {
            FullName = fullName;
            ClientId = clientId;
        }

        /// <summary>
        /// Gets the ClientId of the AddIn
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets the full name of the AddIn type
        /// </summary>
        public string FullName { get; set; }
    }
}