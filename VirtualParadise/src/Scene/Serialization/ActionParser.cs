namespace VirtualParadise.Scene.Serialization
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Commands;
    using Commands.Parsing;
    using Triggers;

    #endregion

    public static class ActionParser
    {
        #region Fields

        private const char CommandChar = ',';

        private const char TriggerChar = ';';

        /// <summary>
        /// Backing field for <see cref="RegisteredCommands"/>.
        /// </summary>
        private static readonly Dictionary<string, Type> registeredCommands = new Dictionary<string, Type>();

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
            RegisterCommand<PictureCommand>();
            RegisterCommand<RotateCommand>();
            RegisterCommand<ScaleCommand>();
            RegisterCommand<ShearCommand>();
            RegisterCommand<SignCommand>();
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
        /// Gets the triggers registered to the parser.
        /// </summary>
        public static IEnumerable<Type> RegisteredTriggers =>
            registeredTriggers.Values;

        #endregion

        #region Methods

        /// <summary>
        /// Registers a command that is recognized by the parser.
        /// </summary>
        /// <typeparam name="TCommand">A <see cref="Command"/> derived type.</typeparam>
        public static void RegisterCommand<TCommand>() where TCommand : Command
        {
            RegisterCommand(typeof(TCommand));
        }

        /// <summary>
        /// Registers a command that is recognized by the parser.
        /// </summary>
        /// <param name="type">The command type.</param>
        public static void RegisterCommand(Type type)
        {
            if (!type.IsSubclassOf(typeof(Command)) && !typeof(Command).IsAssignableFrom(type))
            {
                return;
            }

            if (!(type.GetCustomAttribute<CommandAttribute>() is { } command))
            {
                return;
            }

            registeredCommands?.Add(command.Name.ToUpperInvariant(), type);
        }

        /// <summary>
        /// Registers a trigger that is recognized by the parser.
        /// </summary>
        /// <typeparam name="TTrigger">A <see cref="TriggerBase"/> derived type.</typeparam>
        public static void RegisterTrigger<TTrigger>() where TTrigger : TriggerBase
        {
            RegisterTrigger(typeof(TTrigger));
        }

        /// <summary>
        /// Registers a command that is recognized by the parser.
        /// </summary>
        /// <param name="type">The command type.</param>
        public static void RegisterTrigger(Type type)
        {
            if (!type.IsSubclassOf(typeof(TriggerBase)) && !typeof(TriggerBase).IsAssignableFrom(type)) {
                return;
            }

            if (!(type.GetCustomAttribute<TriggerAttribute>() is { } trigger)) {
                return;
            }

            registeredTriggers?.Add(trigger.Name.ToUpperInvariant(), type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="throwOnError"></param>
        /// <returns></returns>
        public static Action Parse(string input, bool throwOnError) // TODO add optional, remove overload
        {
            return ParseAsync(input, throwOnError).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronously parses an action string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="throwOnError">Optional. Whether or not the parser should throw an exception if an operation
        /// failed. Defaults to <see langword="false"/>.</param>
        /// <returns>Returns an instance of <see cref="Action"/>.</returns>
        public static async Task<Action> ParseAsync(string input, bool throwOnError = false)
        {
            if (String.IsNullOrWhiteSpace(input)) {
                return Action.Empty;
            }

            // TODO delegate this beauty to X10D - ensure to divide by needle length
            // discovered at https://stackoverflow.com/questions/541954/how-would-you-count-occurrences-of-a-string-actually-a-char-within-a-string
            int potentialTriggerCount = input.Length - input.Replace(TriggerChar.ToString(), "").Length;

            ActionBuilder builder       = new ActionBuilder();
            StringBuilder triggerBuffer = new StringBuilder();
            List<string>  triggers      = new List<string>(potentialTriggerCount + 1); // guaranteed to not exceed this
            bool          triggerQuote  = false;

            foreach (char ch in input) {
                // split triggers into a List<string>

                await ParseTriggerCharAsync(ch, triggerBuffer, triggers, ref triggerQuote)
                   .ConfigureAwait(false);
            }

            string tmp = triggerBuffer.ToString().Trim();
            if (!String.IsNullOrWhiteSpace(tmp)) {
                // add remaining buffer to the known triggers
                triggers.Add(tmp);
            }

            triggerBuffer.Clear();

            foreach (string triggerStr in triggers) {
                string[] triggerWords =
                    triggerStr.Split(Array.Empty<char>(), 2, StringSplitOptions.RemoveEmptyEntries);

                if (triggerWords.Length == 0) {
                    continue;
                }

                string triggerWord      = triggerWords[0].ToUpperInvariant();
                string triggerRemainder = triggerWords.Length > 1 ? triggerWords[1] : String.Empty;

                if (!registeredTriggers.ContainsKey(triggerWord)) {
                    // unrecognized trigger - ignore it
                    continue;
                }

                Type        triggerType = registeredTriggers[triggerWord];
                TriggerBase trigger;
                try {
                    // earlier type-checking "guarantees" this to pass.
                    // it this should not throw an exception but it's good to be robust I suppose
                    trigger = Activator.CreateInstance(triggerType) as TriggerBase;

                    if (trigger is null) {
                        throw new NullReferenceException("Instantiated command " + triggerType.Name + " is null");
                    }
                } catch {
                    if (throwOnError) {
                        throw;
                    }

                    continue;
                }

                builder.AddTrigger(trigger);

                int potentialCommandCount = triggerRemainder.Length -
                                            triggerRemainder.Replace(CommandChar.ToString(), "").Length;

                // use previously declared chars to avoid duplicate memory
                StringBuilder commandBuffer = new StringBuilder();
                List<string>  commands      = new List<string>(potentialCommandCount);
                bool          commandQuote  = false;

                foreach (char ch in triggerRemainder) {
                    await ParseCommandCharAsync(ch, commandBuffer, commands, ref commandQuote)
                       .ConfigureAwait(false);
                }

                tmp = commandBuffer.ToString().Trim();
                if (!String.IsNullOrWhiteSpace(tmp)) {
                    // add remaining buffer to the known commands
                    commands.Add(tmp);
                }

                commandBuffer.Clear();

                foreach (string commandStr in commands) {
                    string[] commandWords =
                        commandStr.Split(Array.Empty<char>(), 2, StringSplitOptions.RemoveEmptyEntries);

                    if (commandWords.Length == 0) {
                        continue;
                    }

                    string commandWord      = commandWords[0].ToUpperInvariant();
                    string commandRemainder = commandWords.Length > 1 ? commandWords[1] : String.Empty;

                    if (!registeredCommands.ContainsKey(commandWord)) {
                        if (throwOnError) {
                            throw new Exception("Command " + commandWord + " not recognized");
                        }

                        continue;
                    }

                    Type        commandType = registeredCommands[commandWord];
                    Command command;
                    try {
                        // again, earlier type-checking "guarantees" this to pass... but here we are
                        command = Activator.CreateInstance(commandType) as Command;
                        if (command is null) {
                            throw new NullReferenceException("Instantiated command " + commandType.Name + " is null");
                        }
                    } catch {
                        if (throwOnError) {
                            throw;
                        }

                        continue;
                    }

                    CommandAttribute attribute = commandType.GetCustomAttribute<CommandAttribute>();
                    if (Activator.CreateInstance(attribute.Parser) is CommandParser parser) {
                        command = await parser.ParseAsync(command.GetType(), commandRemainder)
                                              .ConfigureAwait(true);
                    }

                    builder.AddCommand(command);
                }
            }

            return builder.Build();
        }

        private static Task ParseCommandCharAsync(char     ch, StringBuilder builder, List<string> commands,
                                                  ref bool quote)
        {
            switch (ch) {
                case '"':
                    quote = !quote;
                    goto default;

                case CommandChar:
                    if (!quote) {
                        string buffer = builder.ToString().Trim();
                        builder.Clear();
                        commands.Add(buffer);
                        break;
                    }

                    goto default;

                default:
                    builder.Append(ch);
                    break;
            }

            return Task.CompletedTask;
        }

        private static Task ParseTriggerCharAsync(char     ch, StringBuilder builder, List<string> triggers,
                                                  ref bool quote)
        {
            switch (ch) {
                case '"':
                    quote = !quote;
                    goto default;

                case TriggerChar:
                    if (!quote) {
                        string buffer = builder.ToString().Trim();
                        builder.Clear();
                        triggers.Add(buffer);
                        break;
                    }

                    goto default;

                default:
                    builder.Append(ch);
                    break;
            }

            return Task.CompletedTask;
        }

        /*
         /// <summary>
        /// Parses a raw action value into serialized triggers and commands.
        /// </summary>
        /// <param name="text">The action text.</param>
        /// <returns>Returns a new instance of <see cref="System.Action"/>.</returns>
        public static async Task<Action> Parse(string text)
        {
            ActionBuilder builder       = new ActionBuilder();
            StringBuilder triggerBuffer = new StringBuilder();
            List<string>  triggers      = new List<string>();
            bool          triggerQuote  = false;

            foreach (char c in text ?? String.Empty) {
                switch (c) {
                    case '"':
                        triggerBuffer.Append(c);
                        triggerQuote = !triggerQuote;
                        break;

                    case ';':
                        if (!triggerQuote) {
                            triggers.Add(triggerBuffer.ToString().Trim());
                            triggerBuffer.Clear();
                        } else {
                            triggerBuffer.Append(c);
                        }

                        break;

                    default:
                        triggerBuffer.Append(c);
                        break;
                }
            }

            if (!String.IsNullOrWhiteSpace(triggerBuffer.ToString())) {
                triggers.Add(triggerBuffer.ToString().Trim());
                triggerBuffer.Clear();
            }

            foreach (string trigger in triggers) {
                string triggerWord = Array.Find(Regex.Split(trigger, "\\s+"),
                    s => !String.IsNullOrWhiteSpace(s));

                if (!registeredTriggers.ContainsKey(triggerWord.ToUpperInvariant())) {
                    continue;
                }

                if (!(Activator.CreateInstance(registeredTriggers[triggerWord.ToUpperInvariant()])
                    is TriggerBase currentTrigger)) {
                    continue;
                }

                builder.AddTrigger(currentTrigger);

                StringBuilder commandBuffer = new StringBuilder();
                List<string>  commands      = new List<string>();
                bool          commandQuote  = false;

                foreach (char c in trigger.Substring(triggerWord.Length)) {
                    switch (c) {
                        case '"':
                            commandBuffer.Append(c);
                            commandQuote = !commandQuote;
                            break;

                        case ',':
                            if (!commandQuote) {
                                commands.Add(commandBuffer.ToString().Trim());
                                commandBuffer.Clear();
                            } else {
                                commandBuffer.Append(c);
                            }

                            break;

                        default:
                            commandBuffer.Append(c);
                            break;
                    }
                }

                if (!String.IsNullOrWhiteSpace(commandBuffer.ToString())) {
                    commands.Add(commandBuffer.ToString().Trim());
                    commandBuffer.Clear();
                }

                // split commands with ','
                foreach (string command in commands) {
                    string[] commandWords = Regex.Split(command, "\\s+");
                    string   commandWord  = commandWords.FirstOrDefault() ?? String.Empty;

                    if (!registeredCommands.ContainsKey(commandWord.ToUpperInvariant())) {
                        continue;
                    }

                    Type commandType = registeredCommands[commandWord.ToUpperInvariant()];

                    if (!(Activator.CreateInstance(commandType) is CommandBase currentCommand)) {
                        continue;
                    }

                    CommandAttribute commandAttribute = commandType.GetCustomAttribute<CommandAttribute>();
                    if (Activator.CreateInstance(commandAttribute.Parser) is CommandParser parser) {
                        currentCommand = await parser.ParseAsync(currentCommand.GetType(),
                            String.Join(" ", commandWords.Skip(1)));
                    }

                    builder.AddCommand(currentCommand);
                }
            }

            return builder.Build();
        }*/

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
