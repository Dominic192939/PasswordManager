namespace PasswordManager.ConApp
{
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(/*string[] args*/)
        {
            string input = string.Empty;
            using Logic.Contracts.IContext context = CreateContext();

            while (!input.Equals("x", StringComparison.CurrentCultureIgnoreCase))
            {
                int index = 1;
                Console.Clear();
                Console.WriteLine("SEPasswordManager");
                Console.WriteLine("==========================================");

                Console.WriteLine($"{nameof(InitDatabase),-25}....{index++}");

                CreateMenu(ref index);

                Console.WriteLine();
                Console.WriteLine($"Exit...............x");
                Console.WriteLine();
                Console.Write("Your choice: ");

                input = Console.ReadLine()!;
                if (Int32.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            InitDatabase();
                            Console.WriteLine();
                            Console.Write("Continue with Enter...");
                            Console.ReadLine();
                            break;
                        case 2:
                            foreach (var item in context.MockVaultEntriesSet)
                            {
                                Console.WriteLine($"GUID: {item.Guid} | UserName: {item.UserName} | PW: {item.Password} | Email: {item.Email}\n");
                            }
                            Console.ReadKey();
                            break;
                        default:
                            ExecuteMenuItem(choice, context);
                            break;
                    }
                }
            }
        }

        private static Logic.Contracts.IContext CreateContext()
        {
            return Logic.DataContext.Factory.CreateContext();
        }

        public static void InitDatabase()
        {
            BeforeInitDatabase();
            Logic.DataContext.Factory.InitDatabase();
            AfterInitDatabase();
        }

        static void AfterInitDatabase()
        {
            ImportData();
        }

        #region partial methods
        static void BeforeInitDatabase() { }

        static void ImportData() { }
        static void CreateMenu(ref int index) { }
        static void ExecuteMenuItem(int choice, Logic.Contracts.IContext context) { }
        #endregion partial methods
    }
}
