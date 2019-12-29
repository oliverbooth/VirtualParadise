namespace VirtualParadise.Examples
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Scene.Serialization.Commands;
    using Scene.Serialization.Triggers;
    using Action = Scene.Serialization.Action;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            const string input =
                "create scale 2, name dond-rtcam, visible off, solid off, rotate -1 sync";

            Action action = await Action.ParseAsync(input);
            Console.WriteLine($"Input  = {input}");
            Console.WriteLine($"Parsed = {action}\n");

            OutputVerboseAction(action);

            Console.ReadLine();
        }

        private static void OutputVerboseAction(Action action)
        {
            Console.WriteLine($"Trigger count = {action.Triggers.Count()}");
            foreach (TriggerBase trigger in action.Triggers)
            {
                string triggerName = trigger.TriggerName.ToUpperInvariant();
                Console.WriteLine($"  {triggerName} command count: {trigger.Commands.Count()}");
                foreach (CommandBase command in trigger.Commands)
                {
                    string[] ignoredProperties = new[]
                    {
                        nameof(CommandBase.CommandName),
                        nameof(CommandBase.Arguments),
                        nameof(CommandBase.Properties),
                        nameof(CommandBase.Flags)
                    };

                    IEnumerable<PropertyInfo> properties = command.GetType().GetProperties();
                    Console.WriteLine($"      {nameof(CommandBase.CommandName)} = {command.CommandName}");

                    foreach (PropertyInfo property in properties)
                    {
                        if (ignoredProperties.Contains(property.Name, StringComparer.InvariantCultureIgnoreCase))
                        {
                            // don't output properties we want to ignore
                            continue;
                        }

                        object value = property.GetValue(command, null);

                        if ((value?.GetType().IsArray ?? false) && value is IEnumerable enumerable)
                        {
                            // join arrays
                            value = String.Join(", ", enumerable.Cast<object>());
                        }

                        Console.WriteLine($"      {property.Name} = {value}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
