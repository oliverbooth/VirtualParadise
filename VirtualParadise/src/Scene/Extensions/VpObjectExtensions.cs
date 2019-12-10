namespace VirtualParadise.Scene.Extensions
{
    #region Using Directives

    using Serialization;
    using VpNet;

    #endregion

    public static class VpObjectExtensions
    {
        /// <summary>
        /// Gets the parsed action of this object.
        /// </summary>
        /// <param name="obj">The <see cref="VpObject"/>.</param>
        /// <returns>Returns an instance of <see cref="Action"/>.</returns>
        public static Action GetAction(this VpObject obj)
        {
            return Action.Parse(obj.Action);
        }

        /// <summary>
        /// Sets the object's action.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        /// <param name="format">Optional. The action output format. Defaults to <see cref="ActionFormat.None"/>.</param>
        public static void SetAction(this VpObject obj, Action action, ActionFormat format = ActionFormat.None)
        {
            obj.Action = action.ToString(format);
        }
    }
}
