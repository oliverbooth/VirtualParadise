namespace VirtualParadise.Scene.Extensions
{
    #region Using Directives

    using System;
    using Serialization.Commands;
    using VpNet;
    using Action = Serialization.Action;
    using Callback = System.Action<object, VpNet.ObjectClickArgsT<VpNet.Avatar, VpNet.VpObject>>;

    #endregion

    public static class SdkExtensions
    {
        public static void AddObjectClickHandler(this Instance instance, string objectName, Callback callback)
        {
            instance.OnObjectClick += (sender, args) => {
                VpObject obj    = args.VpObject;
                Action   action = Action.Parse(obj.Action);
                string   name   = action.Create?.GetCommandOfType<NameCommand>()?.Name ?? String.Empty;

                if (name.Equals(objectName)) {
                    callback(sender, args);
                }
            };
        }

        public static void AddObjectClickHandler<T>(this Instance                                         instance,
                                                    Action<object, ObjectClickArgsT<Avatar, VpObject>, T> callback)
            where T : Command
        {
            instance.OnObjectClick += (sender, args) => {
                VpObject obj     = args.VpObject;
                Action   action  = Action.Parse(obj.Action);
                T        command = action.Activate?.GetCommandOfType<T>();

                if (!(command is null)) {
                    callback(sender, args, command);
                }
            };
        }
    }
}
