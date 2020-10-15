namespace VirtualParadise.Examples
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Scene.Serialization.Commands;
    using Scene.Serialization.Triggers;
    using Action = Scene.Serialization.Action;

    public static class ParsingExample
    {
        public static void RunExample()
        {
            const string input = "create scale 0.35, visible off, color tint red, ambient 1, diffuse 1, name foobar;" +
                                 "activate astart myAnim global, move 0 -0.01 time=0.1 smooth global, visible on";

            Action action = Action.Parse(input);
            Console.WriteLine($"Input  = {input}");
            Console.WriteLine($"Parsed = {action}\n");

            Console.WriteLine("Parsed action analysis:");
            OutputVerboseAction(action);
        }

        /// <summary>
        /// Output all commands and triggers
        /// </summary>
        /// <param name="action">The action.</param>
        private static void OutputVerboseAction(Action action)
        {
            Console.WriteLine($"Trigger count = {action.Triggers.Count()}");
            foreach (Trigger trigger in action.Triggers) {
                string triggerName = trigger.TriggerName.ToUpperInvariant();
                Console.WriteLine($"  {triggerName} command count: {trigger.Commands.Count()}");
                foreach (Command command in trigger.Commands) {
                    string[] ignoredProperties = new[] {
                        nameof(Command.CommandName),
                        nameof(Command.Arguments),
                        nameof(Command.Properties),
                        nameof(Command.Flags)
                    };

                    IEnumerable<PropertyInfo> properties = command.GetType().GetProperties();
                    Console.WriteLine($"      {nameof(Command.CommandName)} = {command.CommandName}");

                    foreach (PropertyInfo property in properties) {
                        if (ignoredProperties.Contains(property.Name, StringComparer.InvariantCultureIgnoreCase)) {
                            // don't output properties we want to ignore
                            continue;
                        }

                        object value = property.GetValue(command, null);

                        if ((value?.GetType().IsArray ?? false) && value is IEnumerable enumerable) {
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
