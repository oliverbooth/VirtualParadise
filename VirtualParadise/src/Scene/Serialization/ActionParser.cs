namespace VirtualParadise.Scene.Serialization
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using Commands;
    using Commands.Parsing;
    using Triggers;

    #endregion

    internal static class ActionParser
    {
        #region Fields

        /// <summary>
        /// Backing field for <see cref="RegisteredCommands"/>.
        /// </summary>
        private static readonly Dictionary<string, Type> registeredCommands = new Dictionary<string, Type>();

        /// <summary>
        /// Backing field for <see cref="RegisteredCommandParsers"/>.
        /// </summary>
        private static readonly Dictionary<Type, Type> registeredCommandParsers = new Dictionary<Type, Type>();

        /// <summary>
        /// Backing field for <see cref="RegisteredTriggers"/>.
        /// </summary>
        private static readonly Dictionary<string, Type> registeredTriggers = new Dictionary<string, Type>();

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor for <see cref="ActionParser"/>.
        /// </summary>
        static ActionParser()
        {
            RegisterTrigger<ActivateTrigger>();
            RegisterTrigger<AdoneTrigger>();
            RegisterTrigger<BumpTrigger>();
            RegisterTrigger<BumpEndTrigger>();
            RegisterTrigger<CreateTrigger>();
            RegisterCommand<AmbientCommand>();
            RegisterCommand<AnimateCommand>();
            RegisterCommand<AstartCommand>();
            RegisterCommand<AstopCommand>();
            RegisterCommand<CameraCommand>();
            RegisterCommand<ColorCommand>();
            RegisterCommand<DiffuseCommand>();
            RegisterCommand<FrameRateCommand>();
            RegisterCommand<LightCommand>();
            RegisterCommand<MoveCommand>();
            RegisterCommand<NameCommand>();
            RegisterCommand<NoiseCommand>();
            RegisterCommand<NormalMapCommand>();
            RegisterCommand<OpacityCommand>();
            RegisterCommand<RotateCommand>();
            RegisterCommand<ScaleCommand>();
            RegisterCommand<SignCommand, SignCommandParser>();
            RegisterCommand<SolidCommand>();
            RegisterCommand<SoundCommand>();
            RegisterCommand<SpecularCommand>();
            RegisterCommand<SpecularMapCommand>();
            RegisterCommand<TeleportCommand>();
            RegisterCommand<TeleportXyzCommand>();
            RegisterCommand<TextureCommand>();
            RegisterCommand<VisibleCommand>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the commands registered to the parser.
        /// </summary>
        public static IEnumerable<Type> RegisteredCommands =>
            registeredCommands.Values;

        /// <summary>
        /// Gets the command parsers registered to the parser.
        /// </summary>
        public static IEnumerable<Type> RegisteredCommandParsers =>
            registeredCommandParsers.Values;

        /// <summary>
        /// Gets the triggers registered to the parser.
        /// </summary>
        public static IEnumerable<Type> RegisteredTriggers =>
            registeredTriggers.Values;

        #endregion

        #region Methods

        /// <summary>
        /// Registers a command that is recognized by the parser.
        /// </summary>
        /// <typeparam name="TCommand">A <see cref="CommandBase"/> derived type.</typeparam>
        private static void RegisterCommand<TCommand>()
            where TCommand : CommandBase
        {
            RegisterCommand<TCommand, CommandParser<TCommand>>();
        }

        /// <summary>
        /// Registers a command that is recognized by the parser.
        /// </summary>
        /// <typeparam name="TCommand">A <see cref="CommandBase"/> derived type.</typeparam>
        /// <typeparam name="TParser">A <see cref="CommandParser{TCommand}"/> derived type.</typeparam>
        private static void RegisterCommand<TCommand, TParser>()
            where TCommand : CommandBase
            where TParser : CommandParser<TCommand>
        {
            if (!(typeof(TCommand).GetCustomAttribute<CommandAttribute>() is { } command))
            {
                return;
            }

            registeredCommands?.Add(command.Name.ToUpperInvariant(), typeof(TCommand));
            registeredCommandParsers?.Add(typeof(TCommand), typeof(TParser));
        }

        /// <summary>
        /// Registers a trigger that is recognized by the parser.
        /// </summary>
        /// <typeparam name="TTrigger">A <see cref="TriggerBase"/> derived type.</typeparam>
        private static void RegisterTrigger<TTrigger>() where TTrigger : TriggerBase
        {
            if (typeof(TTrigger).GetCustomAttribute<TriggerAttribute>() is { } trigger)
            {
                registeredTriggers?.Add(trigger.Name.ToUpperInvariant(), typeof(TTrigger));
            }
        }

        /// <summary>
        /// Parses a raw action value into serialized triggers and commands.
        /// </summary>
        /// <param name="text">The action text.</param>
        /// <returns>Returns a new instance of <see cref="System.Action"/>.</returns>
        public static Action Parse(string text)
        {
            ActionBuilder builder       = new ActionBuilder();
            StringBuilder triggerBuffer = new StringBuilder();
            List<string>  triggers      = new List<string>();
            bool          triggerQuote  = false;

            foreach (char c in text ?? String.Empty)
            {
                switch (c)
                {
                    case '"':
                        triggerBuffer.Append(c);
                        triggerQuote = !triggerQuote;
                        break;

                    case ';':
                        if (!triggerQuote)
                        {
                            triggers.Add(triggerBuffer.ToString().Trim());
                            triggerBuffer.Clear();
                        }
                        else
                        {
                            triggerBuffer.Append(c);
                        }

                        break;

                    default:
                        triggerBuffer.Append(c);
                        break;
                }
            }

            if (!String.IsNullOrWhiteSpace(triggerBuffer.ToString()))
            {
                triggers.Add(triggerBuffer.ToString().Trim());
                triggerBuffer.Clear();
            }

            foreach (string trigger in triggers)
            {
                string triggerWord = Array.Find(Regex.Split(trigger, "\\s+"),
                    s => !String.IsNullOrWhiteSpace(s));

                if (!registeredTriggers.ContainsKey(triggerWord.ToUpperInvariant()))
                {
                    continue;
                }

                if (!(Activator.CreateInstance(registeredTriggers[triggerWord.ToUpperInvariant()])
                    is TriggerBase currentTrigger))
                {
                    continue;
                }

                builder.AddTrigger(currentTrigger);

                StringBuilder commandBuffer = new StringBuilder();
                List<string>  commands      = new List<string>();
                bool          commandQuote  = false;

                foreach (char c in trigger.Substring(triggerWord.Length))
                {
                    switch (c)
                    {
                        case '"':
                            commandBuffer.Append(c);
                            commandQuote = !commandQuote;
                            break;

                        case ',':
                            if (!commandQuote)
                            {
                                commands.Add(commandBuffer.ToString().Trim());
                                commandBuffer.Clear();
                            }
                            else
                            {
                                commandBuffer.Append(c);
                            }

                            break;

                        default:
                            commandBuffer.Append(c);
                            break;
                    }
                }

                if (!String.IsNullOrWhiteSpace(commandBuffer.ToString()))
                {
                    commands.Add(commandBuffer.ToString().Trim());
                    commandBuffer.Clear();
                }

                // split commands with ','
                foreach (string command in commands)
                {
                    string[]                   commandWords = Regex.Split(command, "\\s+");
                    string                     commandWord  = commandWords.FirstOrDefault() ?? String.Empty;
                    Dictionary<string, object> properties   = GetPropertiesFromArgs(commandWords.Skip(1));
                    IEnumerable<string>        args         = GetArgsWithoutProperties(commandWords.Skip(1));

                    if (!registeredCommands.ContainsKey(commandWord.ToUpperInvariant()))
                    {
                        continue;
                    }

                    if (!(Activator.CreateInstance(registeredCommands[commandWord.ToUpperInvariant()],
                            new object[] {args.ToArray(), properties})
                        is CommandBase currentCommand))
                    {
                        continue;
                    }

                    if (!(Activator.CreateInstance(registeredCommandParsers[currentCommand.GetType()])
                        is CommandParser parser))
                    {
                        continue;
                    }

                    currentCommand = parser.Parse(String.Join(" ", commandWords.Skip(1)));
                    builder.AddCommand(currentCommand);
                }
            }

            return builder.Build();
        }

        private static IEnumerable<string> GetArgsWithoutProperties(IEnumerable<string> args)
        {
            Regex regex = new Regex("([^\\s]+)=([^\\s]+)");
            return args.Where(a => !regex.Match(a).Success && !String.IsNullOrWhiteSpace(a)).ToArray();
        }

        private static Dictionary<string, object> GetPropertiesFromArgs(IEnumerable<string> args)
        {
            Regex  regex = new Regex("([^\\s]+)=([^\\s]+)");
            string str   = String.Join(" ", args);
            return regex.Matches(str)
                        .Cast<Match>()
                        .ToDictionary(m => m.Groups[1].Value,
                             m => (object) m.Groups[2].Value);
        }

        #endregion
    }
}
